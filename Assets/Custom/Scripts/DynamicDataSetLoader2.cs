using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;


public class DynamicDataSetLoader2 : MonoBehaviour
{
    // specify these in Unity Inspector
    public GameObject augmentationObject = null;  // you can use teapot or other object
    public static string dataSetName = "MiniRubik2";  //  Assets/StreamingAssets/QCAR/DataSetName

    // Use this for initialization
    public void Start()
    {
        // // Vuforia 5.0 to 6.1
        // VuforiaBehaviour vb = GameObject.FindObjectOfType<VuforiaBehaviour>();
        // vb.RegisterVuforiaStartedCallback(LoadDataSet);
        //
        // Vuforia 6.2+
        Debug.Log("<color=blue> DynamicDataSetLoader2.Start </color>");

        Debug.Log("<color=red>Path : " + dataSetName + "</color>");
        
        this.augmentationObject = GameObject.Find("ModelTarget");
        
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(LoadDataSet);
           
    }
   
   
    public void DeactivateDataSets()
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker.IsActive)
        {
            objectTracker.Stop();
            objectTracker.DestroyAllDataSets(false);
        }
    }

    void LoadDataSet()
    {

        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker.IsActive)
        {
            objectTracker.Stop();
            objectTracker.DestroyAllDataSets(false);
        }

        DataSet dataSet = objectTracker.CreateDataSet();
        if (dataSet.Load(dataSetName))
        {

            objectTracker.Stop();  // stop tracker so that we can add new dataset

            if (!objectTracker.ActivateDataSet(dataSet))
            {
                // Note: ImageTracker cannot have more than 100 total targets activated
                Debug.Log("<color=yellow>Failed to Activate DataSet: " + dataSetName + "</color>");
            }
            else
            {
                Debug.Log("<color=blue>DataSet Activated : " + dataSetName + "</color>");
            }

            if (!objectTracker.Start())
            {
                Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
            }

            int counter = 0;

            IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
            foreach (TrackableBehaviour tb in tbs)
            {
                if (tb.name == "New Game Object")
                {

                    // change generic name to include trackable name
                    tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;

                    // add additional script components for trackable
                    tb.gameObject.AddComponent<DefaultTrackableEventHandler>();
                    tb.gameObject.AddComponent<TurnOffBehaviour>();

                    if (augmentationObject != null)
                    {
                        // instantiate augmentation object and parent to trackable
                        GameObject augmentation = (GameObject)GameObject.Instantiate(augmentationObject);
                        augmentation.transform.parent = tb.gameObject.transform;
                        augmentation.transform.localPosition = new Vector3(0f, 0f, 0f);
                        augmentation.transform.localRotation = Quaternion.identity;
                        augmentation.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                        augmentation.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("<color=yellow>Warning: No augmentation object specified for: " + tb.TrackableName + "</color>");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
        }
    }
}
