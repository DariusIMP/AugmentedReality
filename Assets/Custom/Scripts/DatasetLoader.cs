using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// NOTE: esto no debería ser un Behaviour.
public class DatasetLoader : MonoBehaviour {

	private static DatasetLoader _instance;
	// Use this for initialization
	private static object _lock = new object();
	private static bool applicationIsQuitting = false;

	private List<GameObject> currentAugmentations = new List<GameObject>();
	// NOTE: Podría ver si sirve guardar una referencia directa al target.
	private string currentModelTargetName = "";
	private string currentDatasetName = "";

	public DatasetLoader() {
		
	}

	public void destroyAllDataSets()
	{
		Debug.Log ("DestroyAllDatasets 1");
		countDatasets ();
		ObjectTracker objectTracker = 
			TrackerManager.Instance.GetTracker<ObjectTracker>();
		if (objectTracker.IsActive)
		{
			objectTracker.Stop();
			// TODO qué onda este bool?
			objectTracker.DestroyAllDataSets(true);
			Debug.Log("<color=green>DestroyAllDataSets</color>");
		}
		countDatasets ();
		Debug.Log ("DestroyAllDatasets 2");

	}

	private void countDatasets(){
		ObjectTracker otr = TrackerManager.Instance.GetTracker<ObjectTracker>();
		IEnumerable<DataSet> ads = otr.GetActiveDataSets();
		foreach (DataSet d in ads) {
			Debug.Log ("CountDatasets: Active dataset. " + d.ToString ());

			IEnumerable<Trackable> ts = d.GetTrackables ();
			foreach (Trackable t in ts) {
				Debug.Log("CountDatasets: Trackable: name: " + t.Name + " - id: " + t.ID);
			}
		}
		IEnumerable<DataSet> ds = otr.GetDataSets();
		foreach (DataSet d in ds) {
			Debug.Log ("CountDatasets: all dataset. " + d.ToString ());
		}
	}

	// TODO: corregir lógica.

	private void findAugmentationObject()
	{
			//DeactiveGameObject();
			//SetAugmentationObject(this.augmentationObject);

	}


	private void setCurrentAugmentation() {
	}


	public void loadDataset(string datasetName, string modelTargetName, GameObject augmentationObject)
	{
		destroyAllDataSets();

		if (loadTarget(datasetName, modelTargetName)) {
			if (!loadAugmentation (augmentationObject)) {
				Debug.LogError("<color=red>Failed to get augmentation object by " +
					"modelTargetName: " + augmentationObject + "</color>");
			}
		} else {
			Debug.LogError("<color=yellow>Failed to load dataset: '" + datasetName + "'</color>");
		}

	}

	// Este es solo para cargar el dataset pedido. Podría devolver falso si no carga.
	private bool loadTarget(string datasetName, string modelTargetName) {
		ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
		DataSet dataSet = objectTracker.CreateDataSet();

		if (!dataSet.Load(datasetName)) return false;

		objectTracker.Stop();  // stop tracker so that we can add new dataset
		if (!objectTracker.ActivateDataSet(dataSet)) {
			// Note: ImageTracker cannot have more than 100 total targets activated
			Debug.Log("<color=yellow>Failed to Activate DataSet: " + datasetName + "</color>");
		} else {
			Debug.Log("<color=blue>DataSet Activated : " + datasetName + "</color>");
		}

		if (!objectTracker.Start()) {
			Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
		}
		return true;

	}

	private bool loadAugmentation(GameObject augmentationObject) {
		
		Debug.Log("<color=blue> Find augmentation object </color>");
		//this.currentAugmentation = GameObject.Find(augmentationObject);
		//if (this.currentAugmentation == null) return false;
		if (augmentationObject == null) return false;

		Debug.Log("Load Augmentation: #CurrentAugmentations: " + currentAugmentations.Count);
		int counter = 0;
		IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
		int tbsCounter = 0;
		foreach (TrackableBehaviour tb in tbs) {
			tbsCounter++;
			if (tb.name == "New Game Object") {
				Debug.Log ("Load Augmentations: New Game Object");
				// change generic name to include trackable name
				tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;

				// add additional script components for trackable
				tb.gameObject.AddComponent<DefaultTrackableEventHandler>();
				tb.gameObject.AddComponent<TurnOffBehaviour>();

				if (augmentationObject != null) {
					// instantiate augmentation object and parent to trackable
					GameObject augmentation = (GameObject)GameObject.Instantiate(augmentationObject);
					augmentation.transform.parent = tb.gameObject.transform;
					augmentation.transform.localPosition = new Vector3(0f, 0f, 0f);
					augmentation.transform.localRotation = Quaternion.identity;
					augmentation.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
					augmentation.gameObject.SetActive(true);

					currentAugmentations.Add(augmentation);
				} else {
					Debug.Log("<color=yellow>Warning: No augmentation object specified for: " + tb.TrackableName + "</color>");
				}
			} else {
				Debug.Log ("Load Augmentation: Trackable not a New Game Object. It is: " + tb.name + " with Trackable: " + tb.Trackable.Name + " - id " + tb.Trackable.ID);
			}
		}
		Debug.Log ("Load Augmentation: tbsCounter: " + tbsCounter);

		return true;
	}

	private void destroyAugmentations() {
		Debug.Log ("Destroy Augmentations: #currentAugmentations" + currentAugmentations.Count);
		foreach(GameObject aug in currentAugmentations) {
			Destroy (aug);
		}
		currentAugmentations.RemoveAll (x => true);
		Debug.Log ("Destroy Augmentations: #currentAugmentations" + currentAugmentations.Count);
	}

//	private void DeactivecurrentAugmentationObject(){
//		if(currentAugmentation != null){
//			currentAugmentation.SetActive(false);
//		}
//	}
//
//	private void SetAugmentationObject(GameObject newAugmentationObject){
//		this.currentAugmentation= newAugmentationObject; 
//	}

	public static DatasetLoader Instance { 
		get {
			if (applicationIsQuitting) {
				Debug.LogWarning("[Singleton] Instance '"+ typeof(DatasetLoader	) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}

			lock(_lock) {
				if (DatasetLoader._instance == null) {
					DatasetLoader._instance = (DatasetLoader) FindObjectOfType(typeof(DatasetLoader));

					if ( FindObjectsOfType(typeof(DatasetLoader)).Length > 1 ){
					Debug.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton!" +
							" Reopening the scene might fix it.");
						return DatasetLoader._instance;
					}

					if (_instance == null){
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<DatasetLoader>();

						if (_instance == null) {
							Debug.Log ("sigue siendo null, no sé qué onda");
						}
						singleton.name = "(singleton) "+ typeof(DatasetLoader).ToString();

						DontDestroyOnLoad(singleton);

						Debug.Log("[Singleton] An instance of " + typeof(DatasetLoader) + 
							" is needed in the scene, so '"  + singleton.name + 
							"' was created with DontDestroyOnLoad.");
					} else {
						// TODO: chequear.
						Debug.Log("[Singleton] Using instance already created: ");
					}
				}

				return DatasetLoader._instance;
			}
		}
	}

	public void OnDestroy () {
		applicationIsQuitting = true;
	}

}
