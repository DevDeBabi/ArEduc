using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnexionManager : MonoBehaviour
{
   public ManagerPrincipalMenu managerPrincipalMenu;
   public void ConnexionDev()
    {
        PlayerPrefs.SetString("iduser", "user" + Random.Range(0, 100));
        managerPrincipalMenu.LoadScene("MENU_PRINCIPAL");
    }


    public void Quitter()
    {
        Application.Quit();
    }
}
