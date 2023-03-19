using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerPrincipalMenu : MonoBehaviour
{
    public string loadingScene = "LOADING_TRANSITION";

    public void Deconnexion(string connexionSceneName)
    {
        PlayerPrefs.DeleteAll();
        LoadScene(connexionSceneName);
    }
    
    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetString("next_scene", sceneName);
        SceneManager.LoadScene(loadingScene, LoadSceneMode.Single);
    }
}
