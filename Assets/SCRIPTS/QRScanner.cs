using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;

public class QRScanner : MonoBehaviour
{
    public TextMeshProUGUI debugTXT;
    public RawImage rawImage;
    WebCamTexture webcamTexture;
    string QrCode = string.Empty;
    [Range(0f, 1.5f)] public float waitTime = 0.1f;
    public ManagerPrincipalMenu managerPrincipalMenu;
    bool finded = false;
    public GameObject badCardPannel;
    public void ActiveQrScan()
    {
        badCardPannel.SetActive(false);
        finded = false;
        var renderer = rawImage;
        webcamTexture = new WebCamTexture(512, 512);
        renderer.texture = webcamTexture;
        Invoke("QrScanLaunch", waitTime);
    }
    [System.Serializable]
    public class CodeJson
    {
        public string id;
        public string name;
    }
    public CodeJson jsonCode;

    public void DisableQrScan()
    {
        CancelInvoke();
        StopAllCoroutines();
        webcamTexture.Stop();
        gameObject.SetActive(false);
    }

    void QrScanLaunch()
    {
        //renderer.material.mainTexture = webcamTexture;
        StartCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();

        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                barCodeReader.Options.TryInverted = true;

                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);

                if (Result != null)
                {

                    QrCode = Result.Text;

                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        debugTXT.text = QrCode;
                        jsonCode= JsonUtility.FromJson<CodeJson>(QrCode);
                        GoOnGoodScene(jsonCode.id,"",jsonCode.name);
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
        webcamTexture.Stop();
    }
    public void DevLaunching(string sceneName)
    {
        if (sceneName == "BAD")
        {
            StartCoroutine(CheckIdCardUsername("12ed", sceneName, false));
        }
        else
            StartCoroutine(CheckIdCardUsername("12ed", sceneName));
    }

    void GoOnGoodScene(string id, string name, string sceneName)
    {
        if (finded)
            return;
        StartCoroutine(CheckIdCardUsername(id, sceneName));
    }

    IEnumerator CheckIdCardUsername(string id, string sceneName, bool good = true)
    {
        finded = true;
        yield return new WaitForSeconds(1f);
        if (good == false)
        {
            badCardPannel.SetActive(true);
            CancelInvoke();
            StopAllCoroutines();
            webcamTexture.Stop();
        }
        else
        {
            managerPrincipalMenu.LoadScene(sceneName);
        }


    }
    //private void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 50;
    //    style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //    string text =QrCode;
    //    GUI.Label(rect, text, style);
    //}
}
