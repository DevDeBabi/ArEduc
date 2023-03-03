using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FruitsShower : MonoBehaviour
{
    public Transform cameraAr;
    public Transform showPos;
    public CanvasManager canvasManager;
    public GameObject myCanvas;
    public TextMeshProUGUI myCanvasTxtInfoNoFound;
    public TextMeshProUGUI txtShowed;
    public AudioSource audioS;
    [Range(0.2f, 1)] public float radius = 0.65f;
    public List<GameObject> listObjets;

    [Range(5f, 25f)] public float rotateSpeed = 10f;
    private List<GameObject> objects;

    public bool turnCurrently = true;

    public GameObject currentObjet = null;

    [System.Serializable]
    public enum CurrLanguage
    {
        french,english
    }
    public CurrLanguage selectedLanguage = CurrLanguage.french;

    [System.Serializable]
    public class ConfigNotFound
    {
        public string infoNotFound;
        public AudioClip clipAudioNotFound;
       [Range(0f,3f)] public float decaleForCallAudio = 0.4f;
    }
    [Header("Config Not Found")]
    public AudioSource audioNoFound;
    public ConfigNotFound FrenchConf;
    public ConfigNotFound EnglishConf;

    public bool isFrenchNoFound = true;

    [Range(5f, 25f)] public float movementSelectedObjectSpeedToward = 10f;
    [Range(1f, 2f)] public float scaleObjectSected = 1.2f;
    void Start()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < listObjets.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / listObjets.Count;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            //Quaternion rot = Quaternion.LookRotation(transform.position - pos, Vector3.up);
            GameObject obj = Instantiate(listObjets[i], pos, listObjets[i].transform.rotation, transform);

            // Assigner la référence fruitShower et les datas à chaque fruit
            FruitClass myFruit = obj.GetComponent<FruitClass>();
            if (myFruit != null)
            {
                myFruit.movementSpeedToward = movementSelectedObjectSpeedToward;
                myFruit.fruitsShower = this;
            }

            objects.Add(obj);
        }
    }

 

    void Update()
    {
        // Faire tourner le cercle
        if (turnCurrently)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (currentObjet != null)
        {
            Vector3 camPos = cameraAr.position;
            camPos.y = showPos.position.y;
            showPos.LookAt(camPos);
        }
    }

    public void ObjectSelected(GameObject obj)
    {
        // Retour de l'ancien objet a sa position d'origine
        if (currentObjet != null)
        {
            currentObjet.GetComponent<FruitClass>().nextPosition = null;
        }
        if (currentObjet == null)
            showPos.GetComponentInChildren<Animator>().SetTrigger("show");

        FruitClass currF = obj.GetComponent<FruitClass>();
        currentObjet = obj;
        audioS.Stop();

        // Jouer et afficher les informations de l'objet toucher en fonction de la langue
        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                txtShowed.text = currF.FrenchConf.name;
                if (currF.FrenchConf.clipAudio != null)
                    audioS.PlayOneShot(currF.FrenchConf.clipAudio);
                break;
            case CurrLanguage.english:
                txtShowed.text = currF.EnglishConf.name;
                if (currF.EnglishConf.clipAudio != null)
                    audioS.PlayOneShot(currF.EnglishConf.clipAudio);
                break;
        }

        // Changer la position et le scale de l'objet selectionner
        currentObjet.GetComponent<FruitClass>().nextPosition = showPos;
        currentObjet.GetComponent<FruitClass>().nextScale = scaleObjectSected;
    }

    public void MyArFounded()
    {
        if (canvasManager.canScan == false)
        {
            canvasManager.currentlySelected = myCanvas;
            return;
        }
        canvasManager.SwitcherCanvasMode(CanvasManager.CurrentDoIt.showCanvasAr, myCanvas);
        audioNoFound.Stop();
        CancelInvoke();

    }
    AudioClip selectedFor;
    public void MyArNotThere()
    {
        print("ok");
        if (canvasManager.currentlySelected == null || canvasManager.canScan == false)
            return;
        if (isFrenchNoFound)
        {
            myCanvasTxtInfoNoFound.text = FrenchConf.infoNotFound;
            selectedFor = FrenchConf.clipAudioNotFound;
            Invoke("CallPlayNofound", FrenchConf.decaleForCallAudio);
        }
        else
        {
            myCanvasTxtInfoNoFound.text = EnglishConf.infoNotFound;
            selectedFor = EnglishConf.clipAudioNotFound;
            Invoke("CallPlayNofound", EnglishConf.decaleForCallAudio);
        }
        canvasManager.SwitcherCanvasMode(CanvasManager.CurrentDoIt.showNotFoundCanvarAr, myCanvas);
    }

    void CallPlayNofound()
    {
        if (selectedFor != null)
            audioNoFound.PlayOneShot(selectedFor);
    }
}