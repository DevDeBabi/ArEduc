using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NoArExerciceFruit : MonoBehaviour
{
    public int score = 0;
    private int countQuestion = 0;
    public int question = 10;
    public GameObject endCanvas;
    public TextMeshProUGUI scoreTxt, scoreTxtFinishCanvas;
    public List<GameObject> fruitPrefabs;
    private List<GameObject> currentFruitPrefabs = new List<GameObject>();
    public GameObject firstCanvasConsigne;
    public TextMeshProUGUI firstTxtConsigne;
    public GameObject[] posSpawn;
    public Image[] images;
    public List<GameObject> currentFruit;
    public GameObject touchPannel;
    public GameObject ExercicePannel;
    public GameObject questionQ;
    public TextMeshProUGUI txtQuestionFruit;
    public bool animateBtnCardFruit = true;
    [Range(5f, 25f)] public float timeAutoGoOut = 10f;
    public AudioClip firstLancementAudio;
    public AudioSource audioS;
    [Range(0.1f, 1f)] public float showTime = 0.3f;
    private float speedG;
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
    public ConfigAudio FrenchConf;
    public ConfigAudio EnglishConf;
    private bool responseGet = false;
    public GameObject findGood, badFind;
    [System.Serializable]
    public enum CurrLanguage
    {
        french, english
    }
    public CurrLanguage selectedLanguage = CurrLanguage.french;
    private bool currentAnimateFruit1 = false;
    private bool currentAnimateFruit2 = false;
    public void ResetAndGoAgain()
    {
        StopAllCoroutines();
        CancelInvoke();
        speedG = 1f / showTime;
        endCanvas.SetActive(false);
        countQuestion = 0;
        score = 0;
        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                scoreTxtFinishCanvas.text = score + " " + FrenchConf.endR;
                break;
            case CurrLanguage.english:
                scoreTxtFinishCanvas.text = score + " " + EnglishConf.endR;
                break;
        }
        scoreTxt.text = countQuestion + " / " + question + " questions";
        if (currentFruitPrefabs.Count > 0)
            currentFruitPrefabs.Clear();
        touchPannel.SetActive(true);
        firstCanvasConsigne.SetActive(false);
        ExercicePannel.SetActive(false);
        responseGet = false;
        if(animateBtnCardFruit==false)
        {
            posSpawn[0].GetComponent<CanvasGroup>().alpha = 1;
            posSpawn[1].GetComponent<CanvasGroup>().alpha = 1;
        }
        currentAnimateFruit1 = true;
        currentAnimateFruit2 = true;
        txtQuestionFruit.text = "";
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
        }
    }

    public void TouchBegin()
    {
        touchPannel.SetActive(false);
        firstCanvasConsigne.SetActive(true);
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
    }

    void GoQuestion()
    {
        firstCanvasConsigne.GetComponent<Animator>().SetTrigger("hide");
        ExercicePannel.SetActive(true);
        NextQuestion();
    }
    GameObject retired;
    int good = 0;

    public void Response(int val)
    {
        if (responseGet)
            return;
        responseGet = true;
        StopAllCoroutines();
        CancelInvoke();
        countQuestion += 1;
        audioS.Stop();

        if(good == val)
        {
            score += 1;
            if (animateBtnCardFruit)
            {
                if (good == 0)
                    currentAnimateFruit2 = true;
                else
                    currentAnimateFruit1 = true;
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
            findGood.GetComponent<Animator>().SetTrigger("show");
        }
        else
        {
            badFind.GetComponent<Animator>().SetTrigger("show");
            switch (selectedLanguage)
            {
                case CurrLanguage.french:
                    AudioClip selectedAudio = FrenchConf.badFind[Random.Range(0, FrenchConf.perfectFind.Length)];
                    audioS.PlayOneShot(selectedAudio);
                    Invoke("NextResponse", selectedAudio.length);
                    break;
                case CurrLanguage.english:
                    AudioClip selectedAudioEng = EnglishConf.badFind[Random.Range(0, EnglishConf.perfectFind.Length)];
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
            endCanvas.SetActive(true);
            currentAnimateFruit1 = true;
            currentAnimateFruit2 = true;
            questionQ.GetComponent<Animator>().SetTrigger("hide");
            endCanvas.SetActive(true);
            switch (selectedLanguage)
            {
                case CurrLanguage.french:
                    scoreTxtFinishCanvas.text = score + " " + FrenchConf.endR;
                    break;
                case CurrLanguage.english:
                    scoreTxtFinishCanvas.text = score + " " + EnglishConf.endR;
                    break;
            }
            Invoke("AutoGoOut", timeAutoGoOut);
        }
        else
        {
            Invoke("NextQuestion", showTime);
        }
    }
    void AutoGoOut()
    {
        audioS.Stop();
        gameObject.SetActive(false);
    }
    void NextQuestion()
    {
        CancelInvoke();
        StopAllCoroutines();
        audioS.Stop();
        if (retired != null)
            currentFruitPrefabs.Remove(retired);

        if (currentFruitPrefabs.Count <= 1)
        {
            currentFruitPrefabs = new List<GameObject>(fruitPrefabs);
        }
        good = 0;
        GameObject selectedLeft = currentFruitPrefabs[Random.Range(0, currentFruitPrefabs.Count - 1)];
        currentFruitPrefabs.Remove(selectedLeft);
        GameObject selectedRight = currentFruitPrefabs[Random.Range(0, currentFruitPrefabs.Count - 1)];
        currentFruitPrefabs.Remove(selectedRight);

        images[0].sprite = selectedLeft.GetComponent<FruitVariables>().mySprite;
        images[0].preserveAspect = true;

        images[1].sprite = selectedRight.GetComponent<FruitVariables>().mySprite;
        images[1].preserveAspect = true;

        currentAnimateFruit1 = false;
        currentAnimateFruit2 = false;
        FruitVariables selected = selectedLeft.GetComponent<FruitVariables>();
        if (Random.Range(0, 10) % 2 == 0)
        {
            good = 1;
            selected = selectedRight.GetComponent<FruitVariables>();
        }


        switch (selectedLanguage)
        {
            case CurrLanguage.french:
                txtQuestionFruit.text = FrenchConf.middleConsigne + " " + selected.FrenchConf.name;
                audioS.PlayOneShot(FrenchConf.audioMiddleSentence);
                StartCoroutine(AudioPlayerFruit(selected.FrenchConf.clipAudio, FrenchConf.audioMiddleSentence.length - 0.75f));
                break;
            case CurrLanguage.english:

                txtQuestionFruit.text = EnglishConf.middleConsigne + " " + selected.EnglishConf.name;
                audioS.PlayOneShot(EnglishConf.audioMiddleSentence);
                StartCoroutine(AudioPlayerFruit(selected.EnglishConf.clipAudio, EnglishConf.audioMiddleSentence.length - 0.75f));
                break;
        }
        responseGet = false;
        questionQ.GetComponent<Animator>().SetTrigger("show");
    }

    IEnumerator AudioPlayerFruit(AudioClip fruit, float timer)
    {
        yield return new WaitForSeconds(timer);
        audioS.PlayOneShot(fruit);
    }

    private void Update()
    {
        if (animateBtnCardFruit)
        {
            if(currentAnimateFruit1==true && posSpawn[0].GetComponent<CanvasGroup>().alpha > 0)
            {
                posSpawn[0].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(posSpawn[0].GetComponent<CanvasGroup>().alpha, 0, speedG * Time.deltaTime);
            }
            if (currentAnimateFruit1 == false && posSpawn[0].GetComponent<CanvasGroup>().alpha < 1)
            {
                posSpawn[0].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(posSpawn[0].GetComponent<CanvasGroup>().alpha, 1, speedG * Time.deltaTime);
            }
            if (currentAnimateFruit2 == true && posSpawn[1].GetComponent<CanvasGroup>().alpha > 0)
            {
                posSpawn[1].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(posSpawn[1].GetComponent<CanvasGroup>().alpha, 0, speedG * Time.deltaTime);
            }
            if (currentAnimateFruit2 == false && posSpawn[1].GetComponent<CanvasGroup>().alpha < 1)
            {
                posSpawn[1].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(posSpawn[1].GetComponent<CanvasGroup>().alpha, 1, speedG * Time.deltaTime);
            }
        }
    }
}
