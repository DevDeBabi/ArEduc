using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitVariables : MonoBehaviour
{
    [Range(1f, 3f)] public float scaleMax = 1.1f;
    public GameObject instantiateParticule;
    [System.Serializable]
    public class ConfigObj
    {
        public string name;
        public AudioClip clipAudio;
    }
    [Header("Config fruit")]
    public ConfigObj FrenchConf;
    public ConfigObj EnglishConf;
}
