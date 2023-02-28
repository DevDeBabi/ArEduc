using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogManager : MonoBehaviour
{
   
    void Start()
    {
        
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene("MAIN");
    }


    public void QUITTER()
    {
        Application.Quit();
    }
}
