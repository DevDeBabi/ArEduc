using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangueTraduction : MonoBehaviour
{

    public Text leText;

    [Multiline]
    public string Traduc_Francais;
    [Multiline]
    public string Traduc_Anglais;

    public bool Francais;
    void Start()
    {
        if (PlayerPrefs.GetInt("langue") == 0)
        {
            Francais = true;
            leText.text = Traduc_Francais;
        }
        else
        {
            Francais = false;
            leText.text = Traduc_Anglais;
        }
    }

  
}
