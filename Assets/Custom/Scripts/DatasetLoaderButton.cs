using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class DatasetLoaderButton : MonoBehaviour
{
    
    public GameObject augmentationObject = null;
    public string dataSetName;
    public string modelTargetName;

	private string _datasetName;
	private string _modelTargetName;
	private GameObject _augmentationObject;

	public void Start() {
		_datasetName = dataSetName;
		_modelTargetName = modelTargetName;
		_augmentationObject = augmentationObject;
	}

	public void action() {
		Debug.Log ("action!");
		DatasetLoader.Instance.loadDataset(_datasetName, _modelTargetName, _augmentationObject);
	}
}