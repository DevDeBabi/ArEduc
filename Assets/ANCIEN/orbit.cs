using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    [SerializeField] private GameObject Solar;

    [SerializeField] private float Angle = 45;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Solar.transform.position, Vector3.up, Angle * Time.deltaTime);
    }
}
