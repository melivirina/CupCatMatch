
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class List_All : MonoBehaviour
{
    public List<Sprite> all_cats = new List<Sprite>();
    private int[] pussy_cat = new int[6];
    public List<Sprite> pussy_use = new List<Sprite>();
    private void Awake()
    {
        LoadGame();
        if (pussy_cat[0] == 1)
            pussy_use[0] = all_cats[7];
        else
             if (pussy_cat[4] == 1)
            pussy_use[0] = all_cats[5];

        if (pussy_cat[1] == 1)
            pussy_use[1] = all_cats[6];
        else
             if (pussy_cat[4] == 1)
            pussy_use[0] = all_cats[5];

        if (pussy_cat[2] == 1)
            pussy_use[2] = all_cats[9];
        else
             if (pussy_cat[4] == 1)
            pussy_use[0] = all_cats[5];

        if (pussy_cat[3] == 1)
            pussy_use[3] = all_cats[2];
        else
             if (pussy_cat[4] == 1)
            pussy_use[0] = all_cats[5];

        BoardManager.pussy_use = pussy_use;
        BoardManager2.pussy_use = pussy_use;
        BoardManager3.pussy_use = pussy_use;
        BoardManager4.pussy_use = pussy_use;
    }
    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath
          + "/MySaveData2.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData2.dat", FileMode.Open);
            Save data = (Save)bf.Deserialize(file);
            file.Close();
            pussy_cat[0] = data.UseCats[0];
            pussy_cat[1] = data.UseCats[1];
            pussy_cat[2] = data.UseCats[2];
            pussy_cat[3] = data.UseCats[3];
            Debug.Log("Game data load!");
        }

    }
}
