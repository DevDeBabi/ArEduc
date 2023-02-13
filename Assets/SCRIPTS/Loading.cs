using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public string sceneToLoad;
    public float delay;

    void Start()
    {
        Invoke("loadScene", delay);
    }

    void loadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
