using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSlider : MonoBehaviour
{
    public GameObject disableBar;
    public Slider sliderSound;
    public float currentSoundValue = 0.5f;
    public TMPro.TextMeshProUGUI txtPercentValue;
    void Start()
    {
        if (PlayerPrefs.HasKey("sound_value"))
            currentSoundValue = PlayerPrefs.GetFloat("sound_value");

        if (currentSoundValue == 0)
            disableBar.SetActive(false);

        PlayerPrefs.SetFloat("sound_value", currentSoundValue);
        sliderSound.value = currentSoundValue;
        txtPercentValue.text = Mathf.FloorToInt(currentSoundValue * 100) + "%";
    }

    public void SwitchTo()
    {
        if (currentSoundValue > 0)
        {
            disableBar.SetActive(true);
            currentSoundValue = 0;
        }
        else
        {
            disableBar.SetActive(false);
            currentSoundValue = 0.2f;
        }
        txtPercentValue.text = Mathf.FloorToInt(currentSoundValue * 100) + "%";
        sliderSound.value = currentSoundValue;
        PlayerPrefs.SetFloat("sound_value", currentSoundValue);
    }
    public void SoundChange()
    {
        currentSoundValue = sliderSound.value;
        txtPercentValue.text = Mathf.FloorToInt(currentSoundValue * 100) + "%";
        PlayerPrefs.SetFloat("sound_value", currentSoundValue);

        if (sliderSound.value <= 0)
        {
            disableBar.SetActive(true);
        }
        else
        {
            disableBar.SetActive(false);
        }
    }
}
