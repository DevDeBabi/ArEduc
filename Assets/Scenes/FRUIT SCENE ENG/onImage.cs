using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onImage : MonoBehaviour
{
    public bool isCorrect;
    public Button moi;

    void Start()
    {
        moi = GetComponent<Button>();
        moi.onClick.AddListener(goVerif);
    }


    void goVerif()
    {

        GameObject.FindObjectOfType<JeuxFruit_Manager>().Verification(isCorrect);
        if (isCorrect)
        {
            // gameObject.SetActive(false);
            GetComponent<Image>().color = Color.black;
        }
    }
}
