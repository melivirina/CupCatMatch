using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;


public class SelectManager : MonoBehaviour
{
    public Image[] Cats;
    public Sprite[] cats_sprite;
    public Sprite[] active_cat;
    public GameObject[] rounds;

    public int[] pussy_cat; private int[] cat = new int[6]; int i = 0;
    private void Awake()
    {
        LoadGame();
        LoadGame2();
        if (cat[0] == 1)
            rounds[0].SetActive(true);

        if (cat[1] == 1)
            rounds[1].SetActive(true);

        if (cat[2] == 1)
            rounds[2].SetActive(true);

        if (cat[3] == 1)
            rounds[3].SetActive(true);

        if (cat[4] == 1)
            rounds[4].SetActive(true);

        if (cat[5] == 1)
            rounds[5].SetActive(true);



        if (pussy_cat[0] == 1)
            Cats[0].sprite = cats_sprite[0];
        else
            Cats[0].sprite = active_cat[0];

        if (pussy_cat[1] == 1)
            Cats[1].sprite = cats_sprite[1];
        else
            Cats[1].sprite = active_cat[1];

        if (pussy_cat[2] == 1)
            Cats[2].sprite = cats_sprite[2];
        else
            Cats[2].sprite = active_cat[2];

        if (pussy_cat[3] == 1)
            Cats[3].sprite = cats_sprite[3];
        else
            Cats[3].sprite = active_cat[3];

        if (pussy_cat[4] == 1)
            Cats[4].sprite = cats_sprite[4];
        else
            Cats[4].sprite = active_cat[4];

        if (pussy_cat[5] == 1)
            Cats[5].sprite = cats_sprite[5];
        else
            Cats[5].sprite = active_cat[5];
    }
    public void Buy1()
    {
        if (pussy_cat[0] == 1)
            if (i < 4)
        {
                cat[0] = 1;
             i++;
            rounds[0].SetActive(true);
           SaveGame();
        }
    }
    public void Buy2()
    {
        if (pussy_cat[1] == 1)
            if (i < 4)
        {
           cat[1] = 1;
           SaveGame();
            rounds[1].SetActive(true);
            i++;
        }
    }
    public void Buy3()
    {
        if (pussy_cat[2] == 1)
            if (i < 4)
        {
                cat[2] = 1;
          SaveGame();
            i++;
            rounds[2].SetActive(true);
        }
    }

    public void Buy4()
    {
        if (pussy_cat[3] == 1)
            if (i < 4)
            {
                cat[3] = 1;
                SaveGame();
                i++;
                rounds[3].SetActive(true);
            }
    }

    public void Buy5()
    {
        if (pussy_cat[4] == 1)
            if (i < 4)
            {
                cat[4] = 1;
                SaveGame();
                i++;
                rounds[4].SetActive(true);
            }
    }

    public void Buy6()
    {
        if (pussy_cat[5] == 1)
            if (i < 4)
            {

                cat[5] = 1;
                SaveGame();
                i++;
                rounds[5].SetActive(true);
            }
    }

    public void LoadGame()
    {      
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            pussy_cat[0] = data.Cats[0];
            pussy_cat[1] = data.Cats[1];
            pussy_cat[2] = data.Cats[2];
            pussy_cat[3] = data.Cats[3];
            pussy_cat[4] = data.Cats[4];
            pussy_cat[5] = data.Cats[5];
            Debug.Log("Game data load!");
        }

    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData2.dat");
        Save data = new Save();
        data.UseCats[0] = cat[0];
        data.UseCats[1] = cat[1];
        data.UseCats[2] = cat[2];
        data.UseCats[3] = cat[3];
        data.UseCats[4] = cat[4];
        data.UseCats[4] = cat[5];
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public void LoadGame2()
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
            cat[0] = data.UseCats[0];
            cat[1] = data.UseCats[1];
            cat[2] = data.UseCats[2];
            cat[3] = data.UseCats[3];
            cat[4] = data.UseCats[4];
            cat[5] = data.UseCats[5];
            Debug.Log("Game data load!");
        }

    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
         + "/MySaveData2.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData2.dat");
            Debug.Log("Data reset complete!");
        }
        rounds[0].SetActive(false);
        rounds[1].SetActive(false);
        rounds[2].SetActive(false);
        rounds[3].SetActive(false);
        rounds[4].SetActive(false);
        rounds[5].SetActive(false);
    }
}
