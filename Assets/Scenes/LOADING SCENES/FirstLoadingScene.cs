using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FirstLoadingScene : MonoBehaviour
{
    public string LoadSceneName;
    public bool MenuPrincipal = true;
    public Image LoaderImage;
    public CanvasGroup groupCanvas;
    [Range(0.8f, 3f)] public float loadingTime =0.5f;
    public bool accelerateEndLoading = false;
    [Range(0.2f, 3f)] public float timeToDisolve = 0.5f;
    public bool randomTime = false;
    [Range(0.6f, 2f)] public float min = 0.3f;
    [Range(2f, 3.8f)] public float max = 1.8f;
    private float speedTo9;
    AsyncOperation sceneLoad;
    private bool loadEnd;
    private float limite = 0.9f;
    void Start()
    {
        if (MenuPrincipal)
        {
            if (PlayerPrefs.HasKey("iduser"))
            {
                LoadSceneName = "MENU_PRINCIPAL";
            }
        }
        if(MenuPrincipal && LoadSceneName == "")
        {
            sceneLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }else if(MenuPrincipal && LoadSceneName != "")
        {
            sceneLoad = SceneManager.LoadSceneAsync(LoadSceneName, LoadSceneMode.Additive);
        }else if(MenuPrincipal == false)
        {
            sceneLoad = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("next_scene"), LoadSceneMode.Additive);
        }
        sceneLoad.allowSceneActivation = false;
        float timer = loadingTime;
        if (randomTime)
            timer = Random.Range(min, max);

        speedTo9 = 0.9f / timer;
        
    }
    private void LateUpdate()
    {
        if(groupCanvas.alpha > 0 && sceneLoad.allowSceneActivation)
        {
            groupCanvas.alpha = Mathf.MoveTowards(groupCanvas.alpha, 0, Time.deltaTime * speedTo9);
        }
        if (sceneLoad.allowSceneActivation)
            return;
        LoaderImage.fillAmount = Mathf.MoveTowards(LoaderImage.fillAmount, limite, Time.deltaTime * speedTo9);
        if(loadEnd == false && sceneLoad.progress==0.9f && LoaderImage.fillAmount == 0.9f)
        {
            loadEnd = true;
            limite = 1f;
            if (accelerateEndLoading)
                speedTo9 *= 2;
        }
        if(sceneLoad.allowSceneActivation == false && loadEnd == true && LoaderImage.fillAmount == 1)
        {
            speedTo9 = 1f / timeToDisolve;
            StartCoroutine(LoadingEnd(timeToDisolve+0.1f));
            sceneLoad.allowSceneActivation = true;
           
        }
    }
    IEnumerator LoadingEnd(float timing)
    {
        yield return new WaitForSeconds(timing);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
