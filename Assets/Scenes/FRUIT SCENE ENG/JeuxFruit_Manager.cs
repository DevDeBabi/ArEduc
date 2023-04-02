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
    public ManagerPrincipalMenu principalMenu;
    public List<Image> xxx;
    string currLanguage = "english";
    public AudioSource audioS;
    [System.Serializable]
    public class languageS
    {
        public AudioClip middleP;
        public AudioClip[] fruitsAudio;
        public AudioClip[] goodFind, badFind;
        public AudioClip endPerfect;
    }
    public languageS engLang;
    public languageS frLang;
    public GameObject endCanvas;
    [System.Serializable]
    public enum CurrLanguage
    {
        french, english
    }
    public CurrLanguage selectedLanguage = CurrLanguage.french;
    public int countTouched = 0;
    IEnumerator SecondPPhrase(int valP, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (selectedLanguage == CurrLanguage.french)
            audioS.PlayOneShot(frLang.fruitsAudio[valP]);
        else if (selectedLanguage == CurrLanguage.english)
            audioS.PlayOneShot(engLang.fruitsAudio[valP]);

    }
    private void Awake()
    {
        principalMenu = GetComponent<ManagerPrincipalMenu>();

       /* if (PlayerPrefs.HasKey("language"))
            currLanguage = PlayerPrefs.GetString("language");

        if (currLanguage == "english")
            selectedLanguage = CurrLanguage.french;
        else if (currLanguage == "french")
            selectedLanguage = CurrLanguage.english;
       */
        for (int i = 0; i < lesPlaces.Count; i++)
        { 
            lesPlaces[i].gameObject.SetActive(false);
        }
        nombre_pour_le_fruit_concerner = Random.Range(5, 25);
        restant = nombre_Total - nombre_pour_le_fruit_concerner;

        int d = Random.Range(0, 5);

        switch (selectedLanguage)
        {
            case CurrLanguage.english:

                audioS.PlayOneShot(engLang.middleP);
                StartCoroutine(SecondPPhrase(d, engLang.middleP.length - 0.85f));
                if (d == 0)
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
                    info.text = "Touche le fruit : Ananas ";
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
                break;
            case CurrLanguage.french:
                audioS.PlayOneShot(frLang.middleP);
                StartCoroutine(SecondPPhrase(d, frLang.middleP.length - 0.85f));
                if (d == 0)
                {
                    orange = true;
                    info.text = "touch the fruit : Orange ";
                }
                if (d == 1)
                {
                    papaye = true;
                    info.text = "touch the fruit : Papaye ";
                }
                if (d == 2)
                {
                    ananas = true;
                    info.text = "touch the fruit : Ananas ";
                }
                if (d == 3)
                {
                    mangue = true;
                    info.text = "touch the fruit : Mangue ";
                }
                if (d >= 4)
                {
                    pomme = true;
                    info.text = "touch the fruit : Pomme ";
                }
                break;
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
        CancelInvoke();
        StopAllCoroutines();
        audioS.Stop();
        if (verif)
        {
            print("bonne reponse");
            countTouched += 1;

            if(countTouched >= nombre_pour_le_fruit_concerner)
            {
                GameEnd();
            }
            else
            {
                if (selectedLanguage == CurrLanguage.french)
                    audioS.PlayOneShot(frLang.goodFind[Random.Range(0,frLang.goodFind.Length)]);
                else if (selectedLanguage == CurrLanguage.english)
                    audioS.PlayOneShot(engLang.goodFind[Random.Range(0, engLang.goodFind.Length)]);
            }
        }
        else
        {
            if (selectedLanguage == CurrLanguage.french)
                audioS.PlayOneShot(frLang.badFind[Random.Range(0, frLang.badFind.Length)]);
            else if (selectedLanguage == CurrLanguage.english)
                audioS.PlayOneShot(engLang.badFind[Random.Range(0, engLang.badFind.Length)]);
            print("mauvaise reponse");
        }
    }
    void GameEnd()
    {
        audioS.Stop();
        endCanvas.SetActive(true);
        if (selectedLanguage == CurrLanguage.french)
            audioS.PlayOneShot(frLang.endPerfect);
        else if (selectedLanguage == CurrLanguage.english)
            audioS.PlayOneShot(engLang.endPerfect);
        Invoke("GoOnDemo", 5f);
    }
    void GoOnDemo()
    {
        audioS.Stop();
        principalMenu.LoadScene("DEMO");
    }
}
