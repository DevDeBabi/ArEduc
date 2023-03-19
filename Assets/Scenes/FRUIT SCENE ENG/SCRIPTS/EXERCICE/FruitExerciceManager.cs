using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FruitExerciceManager : MonoBehaviour
{
    public int score = 0;
    private int countQuestion = 0;
    public int question = 10;
    public GameObject endCanvas;
    public TextMeshProUGUI scoreTxt, scoreTxtFinishCanvas;
    public List<GameObject> fruitPrefabs;
    List<GameObject> currentFruitPrefabs = new List<GameObject>();
    public GameObject notFoundTarget;
    public GameObject firstCanvasConsigne;
    public TextMeshProUGUI firstTxtConsigne;
    public Transform cameraAr;
    public Transform[] posSpawn;
    public CanvasManager canvasManager;
    public List<GameObject> currentFruit;
    private GameObject goodFruit;
    public bool animateBtnCardFruit = true;
    public GameObject myCanvas;
    public Transform posQuestion;
    [Range(5f, 25f)] public float timeAutoGoOut = 10f;
    public AudioClip firstLancementAudio;

    public TextMeshProUGUI myCanvasTxtInfoNoFound;
    public TextMeshProUGUI txtQuestionFruit;
    public Button btnAudioRetake;
    public AudioSource audioS;
    [Range(5f, 25f)] public float showSpeed = 10f;
    public CanvasGroup activeCanvasGroup;
    public bool animateRotate = true;
    [Range(5f, 25f)] public float rotateSpeed = 10f;

    public bool animateScaleOutIn = false;
    [Range(0.1f, 3f)] public float timeScaleOutIn = 0.5f;
    private float speedScaleXFruit1, speedScaleYFruit1, speedScaleZFruit1;
    private float speedScaleXFruit2, speedScaleYFruit2, speedScaleZFruit2;

    private Vector3 searchVector3NewFruit1, searchVector3NewFruit2 = new Vector3();

    [System.Serializable]
    public enum CurrLanguage
    {
        french, english
    }
    public CurrLanguage selectedLanguage = CurrLanguage.french;

    [System.Serializable]
    public class ConfigAudio
    {
        public string firstConsigne;
        public AudioClip firstLancementAudio;
        public float timeWaitFirstLancement = 0.5f;
        public string middleConsigne;
        public AudioClip audioMiddleSentence;
        public AudioClip[] perfectFind;
        public AudioClip[] badFind;
        public string infoNotFound;
        public AudioClip clipAudioNotFound;
        public string endR = " good responses";
        [Range(0f, 3f)] public float decaleForCallAudio = 0.4f;
    }
    private bool isActive;
    [Header("Config Not Found")]
    private AudioClip selectedFor;
    public AudioSource audioNoFound;
    public ConfigAudio FrenchConf;
    public ConfigAudio EnglishConf;

    public bool isFrenchCard = false;
    private bool currentAnimate = false;
    private bool currentAnimateFruit1 = false;
    private bool currentAnimateFruit2 = false;
    private bool stopAnimateFruit1;
    private bool stopAnimateFruit2;
    private bool responseGet =false;
    void LateUpdate()
    {
        if (endCanvas.activeInHierarchy)
        {
            CanvasGroup endCvnsG = endCanvas.GetComponent<CanvasGroup>();
            endCvnsG.alpha = Mathf.MoveTowards(endCvnsG.alpha, 1, 10 * Time.deltaTime);
        }
        if (animateScaleOutIn)
        {
            if (currentAnimateFruit1)
            {
                if (currentFruit.Count > 0)
                {

                    if (currentFruit[0] != null)
                    {
                        Transform curr = currentFruit[0].transform;
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(0, curr.transform.localScale.y, curr.transform.localScale.z), Time.deltaTime * speedScaleXFruit1);
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, 0, curr.transform.localScale.z), Time.deltaTime * speedScaleYFruit1);
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, curr.transform.localScale.y, 0), Time.deltaTime * speedScaleZFruit1);
                    }
                }

            }
            else
            {
                if (currentFruit.Count > 0)
                {
                    if (currentFruit[0] != null)
                    {
                        if (!stopAnimateFruit1)
                        {
                            Transform curr = currentFruit[0].transform;
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(searchVector3NewFruit1.x, curr.transform.localScale.y, curr.transform.localScale.z), Time.deltaTime * speedScaleXFruit1);
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, searchVector3NewFruit1.y, curr.transform.localScale.z), Time.deltaTime * speedScaleYFruit1);
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, curr.transform.localScale.y, searchVector3NewFruit1.z), Time.deltaTime * speedScaleZFruit1);
                        }

                    }
                }

            }
            if (currentAnimateFruit2)
            {
                if (currentFruit.Count > 0)
                {

                    if (currentFruit[1] != null)
                    {
                        Transform curr = currentFruit[1].transform;
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(0, curr.transform.localScale.y, curr.transform.localScale.z), Time.deltaTime * speedScaleXFruit2);
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, 0, curr.transform.localScale.z), Time.deltaTime * speedScaleYFruit2);
                        curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, curr.transform.localScale.y, 0), Time.deltaTime * speedScaleZFruit2);
                    }
                }

            }
            else
            {
                if (currentFruit.Count > 0)
                {
                    if (currentFruit[1] != null)
                    {
                        if (!stopAnimateFruit2)
                        {
                            Transform curr = currentFruit[1].transform;
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(searchVector3NewFruit2.x, curr.transform.localScale.y, curr.transform.localScale.z), Time.deltaTime * speedScaleXFruit2);
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, searchVector3NewFruit2.y, curr.transform.localScale.z), Time.deltaTime * speedScaleYFruit2);
                            curr.localScale = Vector3.MoveTowards(curr.transform.localScale, new Vector3(curr.transform.localScale.x, curr.transform.localScale.y, searchVector3NewFruit2.z), Time.deltaTime * speedScaleZFruit2);
                        }
                    }
                }

            }
        }
    }
    void Update()
    {
        Vector3 camPos = new Vector3();
        if (cameraAr != null)
        {
            camPos = cameraAr.position;
            //camPos.y = showPos.position.y;
            myCanvas.transform.LookAt(camPos);
            firstCanvasConsigne.transform.LookAt(camPos);
            posQuestion.transform.LookAt(camPos);
        }

        btnAudioRetake.interactable = !audioS.isPlaying;

        if (currentFruit.Count > 0)
        {
            if (currentFruit[0] != null)
                if (animateRotate)
                {
                    posSpawn[0].Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                }
                else
                {
                    currentFruit[0].transform.LookAt(camPos);
                }

            if (currentFruit[1] != null)
                if (animateRotate)
                {
                    posSpawn[1].Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                }
                else
                {
                    currentFruit[1].transform.LookAt(camPos);
                }
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

    public void Response(GameObject selected)
    {
        if (responseGet)
            return;
        responseGet = true;
        CancelInvoke();
        countQuestion += 1;
        audioS.Stop();
        if (selected.GetComponent<FruitVariables>() == goodFruit.GetComponent<FruitVariables>())
        {
            print("perfect");
            for (int i = 0; i < currentFruit.Count; i++)
            {
                if (currentFruit[i].name.ToLower().Trim() == selected.name.ToLower().Trim())
                {
                    if (i == 0)
                    {
                        currentAnimateFruit2 = true;
                    }else
                    {
                        currentAnimateFruit1 = true;
                    }
                }
            }
            switch (selectedLanguage)
            {
                case CurrLanguage.french:
                    AudioClip selectedAudio = FrenchConf.perfectFind[Random.Range(0, FrenchConf.perfectFind.Length)];
                    audioS.PlayOneShot(selectedAudio);
                    Invoke("NextResponse", selectedAudio.length);
                    break;
                case CurrLanguage.english:
                    AudioClip selectedAudioEng = EnglishConf.perfectFind[Random.Range(0, EnglishConf.perfectFind.Length)];
                    audioS.PlayOneShot(selectedAudioEng);
                    Invoke("NextResponse", selectedAudioEng.length);
                    break;
            }
            score += 1;
        }
        else
        {
            print("bad");
            switch (selectedLanguage)
            {
                case CurrLanguage.french:
                    AudioClip selectedAudio = FrenchConf.badFind[Random.Range(0, FrenchConf.badFind.Length)];
                    audioS.PlayOneShot(selectedAudio);
                    Invoke("NextResponse", selectedAudio.length);
                    break;
                case CurrLanguage.english:
                    AudioClip selectedAudioEng = EnglishConf.badFind[Random.Range(0, EnglishConf.badFind.Length)];
                    audioS.PlayOneShot(selectedAudioEng);
                    Invoke("NextResponse", selectedAudioEng.length);
                    break;
            }
        }
        scoreTxt.text = countQuestion + " / " + question + " questions";
    }
    void NextResponse()
    {
        if (countQuestion >= question)
        {
            currentAnimate = true;
            currentAnimateFruit1 = true;
            currentAnimateFruit2 = true;
            posQuestion.GetComponent<Animator>().SetTrigger("hide");
            endCanvas.SetActive(true);
            switch (selectedLanguage)
            {
                case CurrLanguage.french:
                    scoreTxtFinishCanvas.text = score + " " +FrenchConf.endR;
                    break;
                case CurrLanguage.english:
                    scoreTxtFinishCanvas.text = score + " " + EnglishConf.endR;
                    break;
            }
            Invoke("AutoGoOut", timeAutoGoOut);
        }
        else
        {
            SpeedScaleCalcul();
        }
    }
    void AutoGoOut()
    {
        canvasManager.GoToPrincipalMenu();
    }
    public void SpeedScaleCalcul()
    {
        if (currentFruit != null)
        {
            if (animateScaleOutIn)
            {
                speedScaleXFruit1 = currentFruit[0].transform.localScale.x / timeScaleOutIn;
                speedScaleYFruit1 = currentFruit[0].transform.localScale.y / timeScaleOutIn;
                speedScaleZFruit1 = currentFruit[0].transform.localScale.z / timeScaleOutIn;

                speedScaleXFruit2 = currentFruit[1].transform.localScale.x / timeScaleOutIn;
                speedScaleYFruit2 = currentFruit[1].transform.localScale.y / timeScaleOutIn;
                speedScaleZFruit2 = currentFruit[1].transform.localScale.z / timeScaleOutIn;

                currentAnimate = true;
                currentAnimateFruit1 = true;
                currentAnimateFruit2 = true;
                StartCoroutine(AnimateWaiting());
            }
            else
            {
                NextQuestionLogic();
            }
        }
    }
    IEnumerator AnimateWaiting()
    {
        posQuestion.GetComponent<Animator>().SetTrigger("hide");
        yield return new WaitForSeconds(timeScaleOutIn);
        NextQuestionLogic();
    }
    IEnumerator StopAnimateFruit1()
    {
        yield return new WaitForSeconds(timeScaleOutIn);
        stopAnimateFruit1 = true;
    }
    IEnumerator StopAnimateFruit2()
    {
        yield return new WaitForSeconds(timeScaleOutIn);
        stopAnimateFruit1 = true;
    }
    GameObject retired;
    int good = 0;
    void NextQuestionLogic()
    {
        if (retired != null)
            currentFruitPrefabs.Remove(retired);
        if (currentFruitPrefabs.Count <= 1)
        {
            currentFruitPrefabs = new List<GameObject>(fruitPrefabs);
        }
        good = 0;
        
        if (currentFruit.Count > 0)
        {
            if (Random.Range(0, 10) % 2 == 0)
            {
                good = 1;
            }

            Destroy(currentFruit[0]);
            Destroy(currentFruit[1]);
        }

        GameObject fruitSelect1 = currentFruitPrefabs[Random.Range(0, currentFruitPrefabs.Count - 1)];
        currentFruitPrefabs.Remove(fruitSelect1);

        GameObject fruitSelect2 = currentFruitPrefabs[Random.Range(0, currentFruitPrefabs.Count - 1)];
        currentFruitPrefabs.Remove(fruitSelect2);

        GameObject newFruit1 = Instantiate(fruitSelect1, posSpawn[0].position, fruitSelect1.transform.rotation, posSpawn[0]) as GameObject;

        GameObject newFruit2 = Instantiate(fruitSelect2, posSpawn[1].position, fruitSelect2.transform.rotation, posSpawn[1]) as GameObject;

        newFruit1.GetComponent<ExerciceFruit>().fruitExerciceManager = this;
        newFruit2.GetComponent<ExerciceFruit>().fruitExerciceManager = this;
        currentFruit.Clear();

        currentFruit.Add(newFruit1);
        currentFruit.Add(newFruit2);

        if (newFruit1.GetComponent<FruitVariables>().instantiateParticule != null)
        {
            GameObject particule = Instantiate(newFruit1.GetComponent<FruitVariables>().instantiateParticule, newFruit1.transform.position, Quaternion.identity) as GameObject;
            Destroy(particule, 5f);
        }
        if (newFruit2.GetComponent<FruitVariables>().instantiateParticule != null)
        {
            GameObject particule = Instantiate(newFruit2.GetComponent<FruitVariables>().instantiateParticule, newFruit2.transform.position, Quaternion.identity) as GameObject;
            Destroy(particule, 5f);
        }

        if (animateScaleOutIn)
        {
            searchVector3NewFruit1 = newFruit1.transform.localScale * newFruit1.GetComponent<FruitVariables>().scaleMax;
            newFruit1.transform.localScale = new Vector3(0, 0, 0);
            speedScaleXFruit1 = searchVector3NewFruit1.x / timeScaleOutIn;
            speedScaleYFruit1 = searchVector3NewFruit1.y / timeScaleOutIn;
            speedScaleZFruit1 = searchVector3NewFruit1.z / timeScaleOutIn;
            stopAnimateFruit1 = false;
            StartCoroutine(StopAnimateFruit1());

            searchVector3NewFruit2 = newFruit2.transform.localScale * newFruit2.GetComponent<FruitVariables>().scaleMax;
            newFruit2.transform.localScale = new Vector3(0, 0, 0);
            speedScaleXFruit2 = searchVector3NewFruit2.x / timeScaleOutIn;
            speedScaleYFruit2 = searchVector3NewFruit2.y / timeScaleOutIn;
            speedScaleZFruit2 = searchVector3NewFruit2.z / timeScaleOutIn;
            stopAnimateFruit2 = false;
            StartCoroutine(StopAnimateFruit2());
        }
        else
        {
            newFruit1.transform.localScale = newFruit1.transform.localScale * newFruit1.GetComponent<FruitVariables>().scaleMax;
            newFruit2.transform.localScale = newFruit2.transform.localScale * newFruit2.GetComponent<FruitVariables>().scaleMax;
        }
        responseGet = false;
        currentAnimate = false;
        currentAnimateFruit1 = false;
        currentAnimateFruit2 = false;
        AudioAndTextLogic();
    }
    void AudioAndTextLogic()
    {
        audioS.Stop();

        int selectedFruit = 0;
        if (Random.Range(0, 10) % 2 == 0)
            selectedFruit = 1;

        goodFruit = currentFruit[selectedFruit];
        // Jouer et afficher les informations de l'objet toucher en fonction de la langue
        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                txtQuestionFruit.text = FrenchConf.middleConsigne + " " + currentFruit[selectedFruit].GetComponent<FruitVariables>().FrenchConf.name;
                audioS.PlayOneShot(FrenchConf.audioMiddleSentence);
                StartCoroutine(AudioPlayerFruit(currentFruit[selectedFruit].GetComponent<FruitVariables>().FrenchConf.clipAudio, FrenchConf.audioMiddleSentence.length-0.55f));
                break;
            case CurrLanguage.english:
                txtQuestionFruit.text = EnglishConf.middleConsigne + " " + currentFruit[selectedFruit].GetComponent<FruitVariables>().EnglishConf.name;
                audioS.PlayOneShot(EnglishConf.audioMiddleSentence);
                StartCoroutine(AudioPlayerFruit(currentFruit[selectedFruit].GetComponent<FruitVariables>().EnglishConf.clipAudio, EnglishConf.audioMiddleSentence.length - 0.75f));
                break;
        }
        posQuestion.GetComponent<Animator>().SetTrigger("show");
    }
    IEnumerator AudioPlayerFruit(AudioClip fruit, float timer)
    {
        yield return new WaitForSeconds(timer);
        audioS.PlayOneShot(fruit);
    }
    public void MyArFounded()
    {
        if (canvasManager.canScan == false)
        {
            canvasManager.currentlySelected = myCanvas;
            return;
        }
        notFoundTarget.SetActive(false);
        audioNoFound.Stop();
        if (currentFruit.Count > 0)
        {
            return;
        }
        if (currentFruitPrefabs.Count > 0)
            currentFruitPrefabs.Clear();
        posQuestion.GetComponent<Animator>().SetTrigger("hide");
        firstCanvasConsigne.GetComponent<Animator>().SetTrigger("show");
        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                firstTxtConsigne.text = FrenchConf.firstConsigne;
                audioS.PlayOneShot(FrenchConf.firstLancementAudio);
                Invoke("GoQuestion", FrenchConf.firstLancementAudio.length + FrenchConf.timeWaitFirstLancement);
                break;
            case CurrLanguage.english:
                firstTxtConsigne.text = EnglishConf.firstConsigne;
                audioS.PlayOneShot(EnglishConf.firstLancementAudio);
                Invoke("GoQuestion", EnglishConf.firstLancementAudio.length + EnglishConf.timeWaitFirstLancement);
                break;
        }
        canvasManager.SwitcherCanvasMode(CanvasManager.CurrentDoIt.showCanvasAr, myCanvas);

    }
    void GoQuestion()
    {
        firstCanvasConsigne.GetComponent<Animator>().SetTrigger("hide");
        audioNoFound.Stop();
        audioS.Stop();
        isActive = true;
        CancelInvoke();
        if (currentFruit.Count == 0)
        {
            NextQuestionLogic();
        }
    }
    public void MyArNotThere()
    {
        if (canvasManager.currentlySelected == null || canvasManager.canScan == false || question == countQuestion)
            return;
        notFoundTarget.SetActive(true);
        audioS.Stop();
        if (isFrenchCard)
        {
            //myCanvasTxtInfoNoFound.text = FrenchConf.infoNotFound;
            selectedFor = FrenchConf.clipAudioNotFound;
            Invoke("CallPlayNofound", FrenchConf.decaleForCallAudio);
        }
        else
        {
            // myCanvasTxtInfoNoFound.text = EnglishConf.infoNotFound;
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

    public void AllCancel()
    {
        audioS.Stop();
        score = 0;
        countQuestion = 0;
        scoreTxt.text = countQuestion + " / " + question + " questions";
        audioNoFound.Stop();
        firstCanvasConsigne.GetComponent<Animator>().SetTrigger("hide");
        posQuestion.GetComponent<Animator>().SetTrigger("hide");
        endCanvas.SetActive(false);
        if (currentFruit.Count > 0)
        {

            Destroy(currentFruit[0].gameObject, 0.2f);
            Destroy(currentFruit[1].gameObject, 0.2f);

            currentFruit.Clear();
        }
        CancelInvoke();
    }
}