using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JeuxFruit_Manager : MonoBehaviour
{
    public TextMeshProUGUI info;
    public List<Sprite> lesImage;
    public Sprite le_Fruit_Choisi;

    public List<Image> lesPlaces;

    public bool orange, papaye, ananas, mangue, pomme;
     int nombre_Total = 40;
    public int nombre_pour_le_fruit_concerner;
     int restant;

    public bool commencer;
    public List<Image> xxx;

    private void Awake()
    {
        for (int i = 0; i < lesPlaces.Count; i++)
        { 
            lesPlaces[i].gameObject.SetActive(false);
        }
        nombre_pour_le_fruit_concerner = Random.Range(5, 25);
        restant = nombre_Total - nombre_pour_le_fruit_concerner;

        int d = Random.Range(0, 5);

        if(d==0)
        {
            orange = true;
            info.text = "Touche le fruit : Orange ";
        }
        if (d == 1)
        {
            papaye = true;
            info.text = "Touche le fruit : Papaya ";
        }
        if (d == 2)
        {
            ananas = true;
            info.text = "Touche le fruit : Pineapple ";
        }
        if (d == 3)
        {
            mangue = true;
            info.text = "Touche le fruit : Mango ";
        }
        if (d >= 4)
        {
            pomme = true;
            info.text = "Touche le fruit : Apple ";
        }

        le_Fruit_Choisi = lesImage[d];

        lesImage.RemoveAt(d);


        InvokeRepeating("ajout", 0.1f, 0.2f);
    }

    int y;
    void ajout()
    {
        int id = Random.Range(0, lesPlaces.Count);
        xxx.Add(lesPlaces[id]);
        lesPlaces.RemoveAt(id);

        y += 1;
        if(y== restant)
        {
            CancelInvoke();
            for (int i = 0; i < lesPlaces.Count; i++)
            { //les bons
                lesPlaces[i].sprite = le_Fruit_Choisi;
                lesPlaces[i].GetComponent<onImage>().isCorrect = true;
                lesPlaces[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < xxx.Count; i++)
            {
                xxx[i].sprite = lesImage[Random.Range(0, lesImage.Count)];
                xxx[i].gameObject.SetActive(true);
            }
        }
    }

    

   
    public void Verification(bool verif)
    {
        if(verif)
        {
            print("bonne reponse");
        }
        else
        {
            print("mauvaise reponse");
        }
    }
}
