using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    public AudioClip Bienvenu, ScanneConsigne;
    public AudioSource As;

    private void Awake()
    {
       
    }

    void Start()
    {
        As = GameObject.FindObjectOfType<AudioSource>();
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

     
        BIENVENUE_clip();
    }

   public void Quitter()
    {
        Application.Quit();
    }

    public void BIENVENUE_clip()
    {
      
            if (As.isPlaying)
            {
                As.Stop();
            }
            else
            {
                As.PlayOneShot(Bienvenu);
            }

          
    }

    public void SCANNECONSIGNE_clip()
    {

        if (As.isPlaying)
        {
            As.Stop();
        }
        else
        {
            As.PlayOneShot(ScanneConsigne);
        }
    }

    private void OnApplicationQuit()
    {
       // PlayerPrefs.DeleteKey("quitter");
    }
}
