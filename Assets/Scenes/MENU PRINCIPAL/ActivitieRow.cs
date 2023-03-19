using UnityEngine;
using TMPro;
public class ActivitieRow : MonoBehaviour
{
    public TextMeshProUGUI txtName, txtTime, txtLast;

    public void ShowMyValue(string id ,string name, string time, string last)
    {
        txtName.text = name;
        txtTime.text = time;
        txtLast.text = last;

        gameObject.name = "row id : " + id;
    }
}
