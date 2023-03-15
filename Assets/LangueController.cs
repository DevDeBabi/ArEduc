using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangueController : MonoBehaviour
{


    public void EnFrancais()
    {
        PlayerPrefs.SetInt("langue", 0);
    }
    public void EnAnglais()
    {
        PlayerPrefs.SetInt("langue", 1);
    }
}
