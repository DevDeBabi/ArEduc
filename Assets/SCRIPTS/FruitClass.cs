using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitClass : MonoBehaviour
{
    public FruitsShower fruitsShower;
    [HideInInspector]
    public Transform nextPosition=null;
    [HideInInspector]
    public float nextScale = 1.1f;
    private Vector3 myPerfectPos;
    private Quaternion myPerfectRot;
    private Vector3 myPerfectScale;
    [HideInInspector]
    public float movementSpeedToward;

    [System.Serializable]
    public class ConfigObj
    {
        public string name;
        public AudioClip clipAudio;
    }
    [Header("Config fruit")]
    public ConfigObj FrenchConf;
    public ConfigObj EnglishConf;

    private void Start()
    {
        myPerfectPos = transform.localPosition;
        myPerfectRot = transform.localRotation;
        myPerfectScale = transform.localScale;
    }
    public void OnMouseDown()
    {
        print(gameObject.name);
        fruitsShower.ObjectSelected(gameObject);
    }

    public void LateUpdate()
    {
        if(nextPosition != null)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPosition.localPosition, movementSpeedToward * Time.deltaTime);
            //transform.localRotation = new Quaternion(Mathf.MoveTowards(transform.localRotation.x,nextPosition.localRotation.x,movementSpeedToward*Time.deltaTime), Mathf.MoveTowards(transform.localRotation.y, nextPosition.localRotation.y, movementSpeedToward * Time.deltaTime), Mathf.MoveTowards(transform.localRotation.z, nextPosition.localRotation.z, movementSpeedToward * Time.deltaTime), Mathf.MoveTowards(transform.localRotation.w, nextPosition.localRotation.w, movementSpeedToward * Time.deltaTime));
            transform.localScale = Vector3.MoveTowards(transform.localScale, myPerfectScale * nextScale, movementSpeedToward * Time.deltaTime * 2);
        }
        else
        {
           if(transform.localPosition != myPerfectPos || transform.localRotation != myPerfectRot || transform.localScale != myPerfectScale)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, myPerfectPos, movementSpeedToward * Time.deltaTime);
                transform.localRotation = new Quaternion(Mathf.MoveTowards(transform.localRotation.x, myPerfectRot.x, movementSpeedToward * Time.deltaTime), Mathf.MoveTowards(transform.localRotation.y, myPerfectRot.y, movementSpeedToward * Time.deltaTime), Mathf.MoveTowards(transform.localRotation.z, myPerfectRot.z, movementSpeedToward * Time.deltaTime), Mathf.MoveTowards(transform.localRotation.w, myPerfectRot.w, movementSpeedToward * Time.deltaTime));
                transform.localScale = Vector3.MoveTowards(transform.localScale, myPerfectScale, movementSpeedToward * Time.deltaTime);
            }
        }
    }
}
