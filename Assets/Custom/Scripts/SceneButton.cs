using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
  
    public void LoadScene (string sceneName)
    {
        Debug.Log("Load Scene: " + sceneName);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                
        SceneManager.LoadSceneAsync(sceneName);
    }
}
