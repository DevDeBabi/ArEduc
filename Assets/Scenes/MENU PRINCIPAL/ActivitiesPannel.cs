using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ActivitiesPannel : MonoBehaviour
{
    public GameObject noDataPrefab;
    public GameObject dataPrefab;

    public Transform spawnPoint;

    public List<GameObject> rowSpawned;
    [System.Serializable]
    public class dataRow
    {
        public string idCard;
        public string name;
        public int timeUsed;
        public int lastUsed;
    }
    List<dataRow> datasRow = new List<dataRow>();
    public void ShowActivities()
    {
        StopAllCoroutines();
        CancelInvoke();
        if (rowSpawned.Count > 0)
        {
            for (int i = 0; i < rowSpawned.Count; i++)
            {
                Destroy(rowSpawned[i]);
            }
        }
        rowSpawned.Clear();

        if (datasRow.Count > 0)
            datasRow.Clear();

        ActivitiesCaller();
    }
    void ActivitiesCaller()
    {
        SpawnNow();
    }
    void SpawnNow()
    {
        if (datasRow.Count == 0)
        {
            GameObject spawned = Instantiate(noDataPrefab, spawnPoint.transform.position, noDataPrefab.transform.rotation, spawnPoint) as GameObject;
            rowSpawned.Add(spawned);
        }
        else
        {
            for (int i = 0; i < datasRow.Count; i++)
            {
                GameObject spawned = Instantiate(dataPrefab, spawnPoint.transform.position, dataPrefab.transform.rotation, spawnPoint) as GameObject;

                int hours = datasRow[i].timeUsed / 60;
                int minutes = datasRow[i].timeUsed % 60;
                string timeUsed = string.Format("{0} H {1} MIN", hours, minutes);

                System.DateTime dat_Time = new System.DateTime(1965, 1, 1, 0, 0, 0, 0);
                dat_Time = dat_Time.AddSeconds(datasRow[i].lastUsed);
                //string print_the_Date = dat_Time.ToShortDateString() + " " + dat_Time.ToShortTimeString();
                string print_the_Date = dat_Time.ToShortDateString();

                spawned.GetComponent<ActivitieRow>().ShowMyValue(datasRow[i].idCard, datasRow[i].name, timeUsed, print_the_Date);
                rowSpawned.Add(spawned);
            }
        }
    }
}
