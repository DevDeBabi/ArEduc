using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FruitVariables : MonoBehaviour
{
    [Range(1f, 3f)] public float scaleMax = 1.1f;
    public GameObject instantiateParticule;
    [System.Serializable]
    public class ConfigObj
    {
        public string name;
        public AudioClip clipAudioTo;
        public AudioClip clipAudio;
    }
    [Header("Config fruit")]
    public ConfigObj FrenchConf;
    public ConfigObj EnglishConf;
    public Sprite mySprite;
}
