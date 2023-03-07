using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitsCardManager : MonoBehaviour
{
    public List<GameObject> fruitPrefabs; // Liste de prefabs de fruits

    private int currentFruitIndex = -1; // Index du fruit actuellement sélectionné

    public Transform cameraAr;
    public Transform showPos;
    public CanvasManager canvasManager;
    public GameObject myCanvas;
    public TextMeshProUGUI myCanvasTxtInfoNoFound;
    public TextMeshProUGUI txtShowNameFruit;
    public AudioSource audioS;
    public bool animateBtnCardFruit = true;
    [Range(5f, 25f)] public float showSpeed = 10f;
    public CanvasGroup activeCanvasGroup;
    public Transform PosNameFruit;
    private FruitVariables currentFruit;

    public bool animateRotate = true;
    [Range(5f, 25f)] public float rotateSpeed = 10f;
    public bool animateScaleOutIn = false;
    [Range(0.1f, 3f)] public float timeScaleOutIn = 0.5f;
    private float speedScaleX, speedScaleY, speedScaleZ;
    private Vector3 searchVector3NewFruit = new Vector3();

    [System.Serializable]
    public enum CurrLanguage
    {
        french, english
    }
    public CurrLanguage selectedLanguage = CurrLanguage.french;

    [System.Serializable]
    public class ConfigNotFound
    {
        public string infoNotFound;
        public AudioClip clipAudioNotFound;
        [Range(0f, 3f)] public float decaleForCallAudio = 0.4f;
    }
    private bool isActive;
    [Header("Config Not Found")]
    private AudioClip selectedFor;
    public AudioSource audioNoFound;
    public ConfigNotFound FrenchConf;
    public ConfigNotFound EnglishConf;

    public bool isFrenchCard = true;
    private bool currentAnimate = false;
    private bool stopAnimate ;

    void LateUpdate()
    {
        if (animateScaleOutIn)
            if (currentAnimate)
            {
                if (currentFruit != null)
                {
                    Transform curr = currentFruit.transform;
                    curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(0, currentFruit.transform.localScale.y, currentFruit.transform.localScale.z), Time.deltaTime * speedScaleX);
                    curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(currentFruit.transform.localScale.x, 0, currentFruit.transform.localScale.z), Time.deltaTime * speedScaleY);
                    curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(currentFruit.transform.localScale.x, currentFruit.transform.localScale.y, 0), Time.deltaTime * speedScaleZ);
                }
            }
            else
            {
                if (currentFruit != null)
                {
                    Transform curr = currentFruit.transform;
                    if (!stopAnimate)
                    {
                        print("here");
                        curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(searchVector3NewFruit.x, currentFruit.transform.localScale.y, currentFruit.transform.localScale.z), Time.deltaTime * speedScaleX);
                        curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(currentFruit.transform.localScale.x, searchVector3NewFruit.y, currentFruit.transform.localScale.z), Time.deltaTime * speedScaleY);
                        curr.localScale = Vector3.MoveTowards(currentFruit.transform.localScale, new Vector3(currentFruit.transform.localScale.x, currentFruit.transform.localScale.y, searchVector3NewFruit.z), Time.deltaTime * speedScaleZ);
                    }
                }
            }
    }
    void Update()
    {
        // Faire tourner
        Vector3 camPos = new Vector3();
        if (cameraAr != null)
        {
            camPos = cameraAr.position;
            //camPos.y = showPos.position.y;
            PosNameFruit.LookAt(camPos);
        }

        if (currentFruit != null)
            if (animateRotate)
            {
                showPos.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            else
            {
                currentFruit.transform.LookAt(camPos);
            }

        if (animateBtnCardFruit)
            if (isActive)
            {
                if (currentAnimate == false)
                {
                    if (activeCanvasGroup.interactable != true || activeCanvasGroup.blocksRaycasts != true || activeCanvasGroup.alpha != 1)
                    {
                        activeCanvasGroup.alpha = Mathf.MoveTowards(activeCanvasGroup.alpha, 1, Time.deltaTime * showSpeed);
                    }
                    if (activeCanvasGroup.interactable != true || activeCanvasGroup.blocksRaycasts != true)
                    {
                        activeCanvasGroup.interactable = true;
                        activeCanvasGroup.blocksRaycasts = true;
                    }
                }
                else
                {
                    if (activeCanvasGroup.interactable != true || activeCanvasGroup.blocksRaycasts != true || activeCanvasGroup.alpha != 1)
                    {
                        activeCanvasGroup.alpha = Mathf.MoveTowards(activeCanvasGroup.alpha, 0.7f, Time.deltaTime * showSpeed * 1.5f);
                    }
                    if (activeCanvasGroup.interactable != false || activeCanvasGroup.blocksRaycasts != false)
                    {
                        activeCanvasGroup.interactable = false;
                        activeCanvasGroup.blocksRaycasts = false;
                    }
                }
            }
            else
            {
                if (activeCanvasGroup.interactable != false || activeCanvasGroup.blocksRaycasts != false || activeCanvasGroup.alpha != 0)
                {
                    activeCanvasGroup.interactable = false;
                    activeCanvasGroup.blocksRaycasts = false;
                    activeCanvasGroup.alpha = Mathf.MoveTowards(activeCanvasGroup.alpha, 0, Time.deltaTime * showSpeed * 2);
                }
            }
    }
    public void PrevNextLogic(bool prev)
    {
        if (currentFruit != null)
        {
            if (animateScaleOutIn)
            {
                speedScaleX = currentFruit.transform.localScale.x / timeScaleOutIn;
                speedScaleY = currentFruit.transform.localScale.y / timeScaleOutIn;
                speedScaleZ = currentFruit.transform.localScale.z / timeScaleOutIn;
                
                currentAnimate = true;
                StartCoroutine(AnimateWaiting(prev));
            }
            else
            {
                SpawnLogic(prev);
            }
        }
    }
    IEnumerator AnimateWaiting(bool prev)
    {
        PosNameFruit.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(timeScaleOutIn);
        SpawnLogic(prev);
    }
    IEnumerator StopAnimate()
    {
        yield return new WaitForSeconds(timeScaleOutIn);
        stopAnimate = true;
    }

    void SpawnLogic(bool prev)
    {
        if (currentFruit != null)
            Destroy(currentFruit.gameObject);
        if (!prev)
        {
            if (currentFruitIndex < fruitPrefabs.Count - 1)
            {
                currentFruitIndex++;
            }
            else
            {
                currentFruitIndex = 0;
            }
        }
        else
        {
            if (currentFruitIndex > 0)
            {
                currentFruitIndex--;
            }
            else
            {
                currentFruitIndex = fruitPrefabs.Count - 1;
            }
        }
        
        GameObject newFruit = Instantiate(fruitPrefabs[currentFruitIndex], showPos.position, fruitPrefabs[currentFruitIndex].transform.rotation, showPos);
        currentFruit = newFruit.GetComponent<FruitVariables>();
        if (currentFruit.instantiateParticule != null)
        {
            GameObject particule = Instantiate(currentFruit.instantiateParticule, newFruit.transform.position, Quaternion.identity) as GameObject;
            Destroy(particule, 5f);
        }
        if (animateScaleOutIn)
        {
            searchVector3NewFruit = currentFruit.transform.localScale * currentFruit.scaleMax;
            currentFruit.transform.localScale = new Vector3(0,0,0);
            speedScaleX = searchVector3NewFruit.x / timeScaleOutIn;
            speedScaleY = searchVector3NewFruit.y / timeScaleOutIn;
            speedScaleZ = searchVector3NewFruit.z / timeScaleOutIn;
            stopAnimate = false;
            StartCoroutine(StopAnimate());

        }
        else
        {
            currentFruit.transform.localScale = currentFruit.transform.localScale * currentFruit.scaleMax;
        }
       
        currentAnimate = false;
        AudioAndTextLogic();

    }
    
    void AudioAndTextLogic()
    {
        audioS.Stop();
        // Jouer et afficher les informations de l'objet toucher en fonction de la langue
        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                txtShowNameFruit.text = currentFruit.FrenchConf.name;
                if (currentFruit.FrenchConf.clipAudio != null)
                    audioS.PlayOneShot(currentFruit.FrenchConf.clipAudio);
                break;
            case CurrLanguage.english:
                txtShowNameFruit.text = currentFruit.EnglishConf.name;
                if (currentFruit.EnglishConf.clipAudio != null)
                    audioS.PlayOneShot(currentFruit.EnglishConf.clipAudio);
                break;
        }
        PosNameFruit.GetComponent<Animator>().SetTrigger("show");
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
        isActive = true;
        CancelInvoke();
        if (currentFruit == null)
        {

            SpawnLogic(false);
        }
    }

    public void MyArNotThere()
    {
        if (canvasManager.currentlySelected == null || canvasManager.canScan == false)
            return;
        if (isFrenchCard)
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
        isActive = false;
        canvasManager.SwitcherCanvasMode(CanvasManager.CurrentDoIt.showNotFoundCanvarAr, myCanvas);
    }

    void CallPlayNofound()
    {
        if (selectedFor != null)
        {
            audioNoFound.PlayOneShot(selectedFor);
            print("audio played:" + selectedFor.name);
        }
    }
}
