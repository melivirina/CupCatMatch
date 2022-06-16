using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Edit : MonoBehaviour
{
    MoneyManager man = new MoneyManager();
    public GameObject ResetPanel;
    public void Close()
    {
        ResetPanel.SetActive(false);
    }
    public void Open()
    {
        ResetPanel.SetActive(true);
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }

        if (File.Exists(Application.persistentDataPath
         + "/MySaveData2.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData2.dat");
            Debug.Log("Data reset complete!");
        }
        ResetPanel.SetActive(false);
    }

}
