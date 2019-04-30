using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick) { 
        button.onClick.AddListener(delegate
        {
            OnClick(param);
        });
    }
}

public class AssetBundleScene : MonoBehaviour {

    private static string dlcFolder; 
   
    [Header("UI Stuff")]
    public Transform rootContainer;
    public Button prefab;
    public Text labelText;

    static List<AssetBundle> assetBundles = new List<AssetBundle>();
    static List<string> sceneNames = new List<string>();

    IEnumerator Start()
    {
        dlcFolder = "jar:file://" + Application.dataPath + "!/assets/AssetBundles/";

        Debug.Log("AssetBundleScenee - DLCFOLDER: " + dlcFolder);
        string[] urls = { dlcFolder + "downloadablecontent.dlc" };

        //urls = System.IO.Directory.GetFiles(dlcFolder, "*.dlc");
        
        Debug.Log("AssetBundleScenee - urls: " + urls);
        Debug.Log("AssetBundleScenee - LALALALALAA");

        if (assetBundles.Count == 0)
        {
            Debug.Log("AssetBundleScenee - if: entro");

            int i = 0;
            while (i < urls.Length)
            {
                using (WWW www = new WWW(urls[i]))
                {
                    yield return www;
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        Debug.LogError(www.error);
                        yield break;
                    }
                    
                    assetBundles.Add(www.assetBundle);
                    sceneNames.AddRange(www.assetBundle.GetAllScenePaths());
                }
                i++;
            }
        }

        Debug.Log("AssetBundleScenee - foreach: antes");

        foreach (string sceneName in sceneNames)
        {

            Debug.Log("AssetBundleScenee - foreach: adentro");

            labelText.text = Path.GetFileNameWithoutExtension(sceneName);

            var clone = Instantiate(prefab.gameObject) as GameObject;

            clone.GetComponent<Button>().AddEventListener(labelText.text, LoadAssetBundleScene);

            clone.SetActive(true);
            clone.transform.SetParent(rootContainer, false);
        }

        Debug.Log("AssetBundleScenee - foreach: despues");

    }

    public void LoadAssetBundleScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
