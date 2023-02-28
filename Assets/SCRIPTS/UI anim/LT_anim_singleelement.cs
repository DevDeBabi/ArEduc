using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LT_anim_singleelement : MonoBehaviour
{
    public bool FERMER;
    public float delay;
    public Vector2 v;
  

    public bool setEaseOutBounce;
    public bool setEaseInOutBounce;

    public bool setEaseOutElastic;

    public bool setEaseOutBack;
    private void Start()
    {
       transform.localScale = Vector2.zero;
    }


    private void OnEnable()
    {
        Invoke("SetBounce", delay);   
    }

    void SetBounce()
    {
        if (setEaseOutBounce == false && setEaseInOutBounce == false && setEaseOutElastic == false || setEaseOutBounce == true && setEaseInOutBounce == true && setEaseOutElastic == false)
        {
            transform.LeanScale(v, 0.8f).setEaseOutBounce();
        }

         if(setEaseOutBounce == true)
        {
            transform.LeanScale(v, 0.8f).setEaseOutBounce();
        }

         if(setEaseInOutBounce == true)
        {
             transform.LeanScale(v, 0.8f).setEaseInOutBounce();
        }

         if (setEaseOutElastic == true)
        {
            transform.LeanScale(v, 0.8f).setEaseOutElastic();
        }

        if (setEaseOutBack == true)
        {
            transform.LeanScale(v, 0.8f).setEaseOutBack();
        }

    }

    public void CloseBox()
    {
        if (FERMER)
        {
            transform.LeanScale(Vector2.zero, 0.5f).setEaseOutSine().setOnComplete(OnComplete);
        }
        else
        {
            transform.LeanScale(Vector2.zero, 1f).setEaseOutBounce().setOnComplete(OnComplete);
        }
    }

    void OnComplete ()
    {
        if (FERMER==true)
        {
            gameObject.SetActive(false);
        }
        print(gameObject.name + "onComplete call");
    }

    private void OnDisable()
    {
      //  gameObject.SetActive(false);
        transform.localScale = Vector2.zero;
    }
}
