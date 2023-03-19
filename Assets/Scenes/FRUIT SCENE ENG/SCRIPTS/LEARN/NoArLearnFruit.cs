using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NoArLearnFruit : MonoBehaviour
{
    public GameObject ClickBtn;
    public GameObject learnPannel;
    public Image showImg;
    CanvasGroup showImgCanvas;
    [Range(0.3f,1.5f)]public float timeForNext = 0.3f;
    public TextMeshProUGUI txtNameFruit;
    public AudioSource audioS;
    public List<GameObject> prefabFruit;
    public int currentIndex = -1;
    public CanvasGroup canvasBtn;
    bool currentAnimate;
    float speed = 0.1f;
    int oldIndex = -1;
    public Button listenAgain;
    
    [System.Serializable]
    public enum language
    {
        english, french
    }
    public language configLanguage;
    public void NewLaunch()
    {
        currentIndex = -1;
        oldIndex = -1;
        txtNameFruit.text = "";
        currentAnimate = false;
        ClickBtn.SetActive(true);
        learnPannel.SetActive(false);
        speed = 1f / 0.3f;
        showImgCanvas = showImg.GetComponent<CanvasGroup>();
        showImgCanvas.alpha = 0;
    }
    public void GoLaunch()
    {
        PrevNext(false);
        canvasBtn.interactable = false;
        ClickBtn.SetActive(false);
        learnPannel.SetActive(true);
    }
    public void QuitMenuLearn()
    {
        audioS.Stop();
        gameObject.SetActive(false);
    }
    public void ListenAgain()
    {
        GameObject current = prefabFruit[currentIndex];
        switch (configLanguage)
        {
            case language.english:
                audioS.PlayOneShot(current.GetComponent<FruitVariables>().EnglishConf.clipAudio);
                break;
            case language.french:

                audioS.PlayOneShot(current.GetComponent<FruitVariables>().FrenchConf.clipAudio);
                break;
        }
    }
    public void PrevNext(bool prev)
    {
        if(prev)
        {
            if (currentIndex <= 0)
            {
                currentIndex = prefabFruit.Count - 1;
            }
            else
            {
                currentIndex -= 1;
            }
        }
        else
        {
            if (currentIndex >= prefabFruit.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex += 1;
            }
        }
        canvasBtn.interactable = false;
        currentAnimate = true;
    }
    AudioClip goodAudio;
    void CallFruit()
    {
        oldIndex = currentIndex;
        currentAnimate = false;
        StopAllCoroutines();
        CancelInvoke();
        audioS.Stop();
        GameObject current = prefabFruit[currentIndex];
        showImg.sprite = current.GetComponent<FruitVariables>().mySprite;
        showImg.preserveAspect = true;
        switch (configLanguage)
        {
            case language.english:
                if(current.GetComponent<FruitVariables>().EnglishConf.clipAudioTo!= null)
                {
                    audioS.PlayOneShot(current.GetComponent<FruitVariables>().EnglishConf.clipAudioTo);
                    goodAudio = current.GetComponent<FruitVariables>().EnglishConf.clipAudio;
                    Invoke("CallGoodName", current.GetComponent<FruitVariables>().EnglishConf.clipAudioTo.length - 0.8f);
                }
                else
                {
                    if (current.GetComponent<FruitVariables>().EnglishConf.clipAudio != null)
                        audioS.PlayOneShot(current.GetComponent<FruitVariables>().EnglishConf.clipAudio);
                }
                txtNameFruit.text = current.GetComponent<FruitVariables>().EnglishConf.name;
                break;
            case language.french:
                if (current.GetComponent<FruitVariables>().FrenchConf.clipAudioTo != null)
                {
                    audioS.PlayOneShot(current.GetComponent<FruitVariables>().FrenchConf.clipAudioTo);
                    goodAudio = current.GetComponent<FruitVariables>().FrenchConf.clipAudio;
                    Invoke("CallGoodName", current.GetComponent<FruitVariables>().FrenchConf.clipAudioTo.length - 0.8f);
                }
                else
                {
                    if (current.GetComponent<FruitVariables>().FrenchConf.clipAudio != null)
                        audioS.PlayOneShot(current.GetComponent<FruitVariables>().FrenchConf.clipAudio);
                }
               
                txtNameFruit.text = current.GetComponent<FruitVariables>().FrenchConf.name;
                break;
        }
        canvasBtn.interactable = true;
    }

    void CallGoodName()
    {
        audioS.PlayOneShot(goodAudio);
    }

    private void Update()
    {
        if (currentAnimate && oldIndex != currentIndex)
        {
            showImgCanvas.alpha = Mathf.MoveTowards(showImgCanvas.alpha, 0, Time.deltaTime * speed);
            if (showImgCanvas.alpha == 0)
            {
                CallFruit();
            }
        }
        if (oldIndex == currentIndex && currentAnimate == false && showImgCanvas.alpha < 1)
        {
            showImgCanvas.alpha = Mathf.MoveTowards(showImgCanvas.alpha, 1, Time.deltaTime * speed);
        }
        listenAgain.interactable = !audioS.isPlaying;
    }
}
