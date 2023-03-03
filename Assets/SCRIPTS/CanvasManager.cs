using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject principalCanvas;
    public GameObject noArCanvas;
    public List<GameObject> allAppCanvas;
    public GameObject currentlySelected;
    public bool canScan;
    [Range(5f, 25f)] public float speedAnimationAlpha = 10f;

    [System.Serializable]
    public enum CurrentDoIt
    {
        showPrincipalMenu,
        showNoAr,
        showCanvasAr,
        showNotFoundCanvarAr,
    }
    public CurrentDoIt currentDoIt = CurrentDoIt.showPrincipalMenu;

    private void Start()
    {
        CheckApplyCanvas();
    }

    void ShowPrincipalMenu()
    {
        for (int i = 0; i < allAppCanvas.Count; i++)
        {
            if (allAppCanvas[i] != principalCanvas)
                allAppCanvas[i].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(allAppCanvas[i].GetComponent<CanvasGroup>().alpha, 0, speedAnimationAlpha * Time.deltaTime * 2);
            else
                principalCanvas.GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(principalCanvas.GetComponent<CanvasGroup>().alpha, 1, speedAnimationAlpha * Time.deltaTime);
        }
    }

    void ShowNoAr()
    {
        for (int i = 0; i < allAppCanvas.Count; i++)
        {
            if (allAppCanvas[i] != noArCanvas)
                allAppCanvas[i].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(allAppCanvas[i].GetComponent<CanvasGroup>().alpha, 0, speedAnimationAlpha * Time.deltaTime * 2);
            else
                noArCanvas.GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(noArCanvas.GetComponent<CanvasGroup>().alpha, 1, speedAnimationAlpha * Time.deltaTime);
        }
    }
    void ShowCanvarArNormal()
    {
        for (int i = 0; i < allAppCanvas.Count; i++)
        {
            if (allAppCanvas[i] != currentlySelected)
                allAppCanvas[i].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(allAppCanvas[i].GetComponent<CanvasGroup>().alpha, 0, speedAnimationAlpha * Time.deltaTime * 2);
            else
                currentlySelected.GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(currentlySelected.GetComponent<CanvasGroup>().alpha, 1, speedAnimationAlpha * Time.deltaTime);
        }
    }
    void ShowNotFoundCanvas()
    {
        for (int i = 0; i < allAppCanvas.Count; i++)
        {
            if (allAppCanvas[i] != currentlySelected)
                allAppCanvas[i].GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(allAppCanvas[i].GetComponent<CanvasGroup>().alpha, 0, speedAnimationAlpha * Time.deltaTime * 2);
            else
                currentlySelected.GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(currentlySelected.GetComponent<CanvasGroup>().alpha, 1, speedAnimationAlpha * Time.deltaTime);
        }
    }

    void CheckApplyCanvas()
    {
        switch (currentDoIt)
        {
            case CurrentDoIt.showPrincipalMenu:
                currentlySelected = null;
                canScan = false;
                for (int i = 0; i < allAppCanvas.Count; i++)
                {
                    if (allAppCanvas[i] != principalCanvas)
                    {
                        allAppCanvas[i].GetComponent<CanvasGroup>().interactable = false;
                        allAppCanvas[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                    else
                    {
                        principalCanvas.GetComponent<CanvasGroup>().interactable = true;
                        principalCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                }
                break;
            case CurrentDoIt.showNoAr:
                canScan = true;
                for (int i = 0; i < allAppCanvas.Count; i++)
                {
                    if (allAppCanvas[i] != noArCanvas)
                    {
                        allAppCanvas[i].GetComponent<CanvasGroup>().interactable = false;
                        allAppCanvas[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                    else
                    {
                        noArCanvas.GetComponent<CanvasGroup>().interactable = true;
                        noArCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                }
                break;
            case CurrentDoIt.showCanvasAr:
                currentlySelected.transform.GetChild(currentlySelected.transform.childCount - 1).gameObject.SetActive(false);
                for (int i = 0; i < allAppCanvas.Count; i++)
                {
                    if (allAppCanvas[i] != currentlySelected)
                    {
                        allAppCanvas[i].GetComponent<CanvasGroup>().interactable = false;
                        allAppCanvas[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                    else
                    {
                        currentlySelected.GetComponent<CanvasGroup>().interactable = true;
                        currentlySelected.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                }
                break;
            case CurrentDoIt.showNotFoundCanvarAr:
                currentlySelected.transform.GetChild(currentlySelected.transform.childCount - 1).gameObject.SetActive(true);
                for (int i = 0; i < allAppCanvas.Count; i++)
                {
                    if (allAppCanvas[i] != currentlySelected)
                    {
                        allAppCanvas[i].GetComponent<CanvasGroup>().interactable = false;
                        allAppCanvas[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                    else
                    {
                        currentlySelected.GetComponent<CanvasGroup>().interactable = true;
                        currentlySelected.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                }
                break;
        }
    }

    public void SwitcherCanvasMode(CurrentDoIt doItNow, GameObject canvas)
    {
        currentlySelected = canvas;
        currentDoIt = doItNow;

        CheckApplyCanvas();
    }

    public void GoForScan()
    {
        canScan = true;

        SwitcherCanvasMode(CurrentDoIt.showNoAr, currentlySelected);
      
    }
    private void LateUpdate()
    {
        switch (currentDoIt)
        {
            case CurrentDoIt.showPrincipalMenu:
                ShowPrincipalMenu();
                break;
            case CurrentDoIt.showNoAr:
                ShowNoAr();
                break;
            case CurrentDoIt.showCanvasAr:
                ShowCanvarArNormal();
                break;
            case CurrentDoIt.showNotFoundCanvarAr:
                ShowNotFoundCanvas();
                break;
        }
    }
}
