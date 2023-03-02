using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{

    public GameObject AnimalPanel, MathPanel;
    public AudioSource As;
    public GameObject[] audioBouton;
  

    void Start()
    {
        As = GameObject.FindObjectOfType<AudioSource>();
    }

  

    public void ON_TARGET_LOST()
    {
        for (int i = 0; i < audioBouton.Length; i++)
        {
            audioBouton[i].SetActive(false);
        }
    }

}
