using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LT_anim_custom : MonoBehaviour
{
    public Transform box;
    public CanvasGroup Cg;
    public Vector2 v;
    private void Start()
    {
        box.transform.localScale = Vector2.zero;
    }


    private void OnEnable()
    {
        Cg.alpha = 0;
        Cg.LeanAlpha(1, 0.5f);

        //box.localPosition = new Vector2(0, -Screen.height);
        //box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;

        box.transform.LeanScale(v, 0.8f).setEaseOutBounce();
    }


    public void CloseBox()
    {
        Cg.LeanAlpha(0, 0.5f);
        //box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);

        // box. transform.LeanScale(Vector2.zero, 1f).setEaseInBack().setOnComplete(OnComplete);
        box.transform.LeanScale(Vector2.zero, 1f).setEaseOutBounce().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
