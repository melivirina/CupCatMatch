using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    private string Buy_name;
    public Image[] Cats;
    public Sprite[] cats_sprite;
    public Sprite[] active_cat;
    public int[] pussy_cat;
    public GameObject MoneyPanel;
    public Text NoMoneyText;
    private void Awake()
    {
        
       LoadGame();
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
    void Update()
    {
        moneyText.text = Money.i.ToString();
    }
    public void Close_window()
    {
        MoneyPanel.SetActive(false);
    } 
    public void Buy1()
    {
         Buy_name = "Cat0";
        if(pussy_cat[0] != 1)
            MoneyPanel.SetActive(true); 
    }
    public void Buy2()
    {
        Buy_name = "Cat1";
        if (pussy_cat[1] != 1)
            MoneyPanel.SetActive(true);
    }
    public void Buy3()
    {
        Buy_name = "Cat2";
        if (pussy_cat[2] != 1)
            MoneyPanel.SetActive(true);
    }

    public void Buy4()
    {
        Buy_name = "Cat3";
        if (pussy_cat[3] != 1)
            MoneyPanel.SetActive(true);
    }

    public void Buy5()
    {
        Buy_name = "Cat4";
        if (pussy_cat[4] != 1)
            MoneyPanel.SetActive(true);
    }

    public void Buy6()
    {
        Buy_name = "Cat5";
        if (pussy_cat[5] != 1)
            MoneyPanel.SetActive(true);
    }



    public void Buy()
    {
        if (Buy_name == "Cat0" && Money.i >= 5)
        {
            pussy_cat[0] = 1;
            Cats[0].sprite = cats_sprite[0];
            Money.i -= 5;
            SaveGame();
            Close_window();
        }
        if (Buy_name == "Cat1" && Money.i >= 10)
        {
            pussy_cat[1] = 1;
            Cats[1].sprite = cats_sprite[1];
            Money.i -= 10;
            SaveGame();
            Close_window();
        }
        if (Buy_name == "Cat2" && Money.i >= 19)
        {
            pussy_cat[2]= 1;
            Cats[2].sprite = cats_sprite[2];
            Money.i -= 19;
           SaveGame(); 
           Close_window();
        }
        if (Buy_name == "Cat3" && Money.i >= 55)
        {
            pussy_cat[3] = 1;
            Cats[3].sprite = cats_sprite[3];
            Money.i -= 55;
            SaveGame();
            Close_window();
        }
        if (Buy_name == "Cat4" && Money.i >= 77)
        {
            pussy_cat[4] = 1;
            Cats[4].sprite = cats_sprite[4];
            Money.i -= 77;
            SaveGame();
            Close_window();
        }
        if (Buy_name == "Cat5" && Money.i >= 99)
        {
            pussy_cat[5] = 1;
            Cats[5].sprite = cats_sprite[5];
            Money.i -= 99;
            SaveGame();
            Close_window();
        }
        else
        {
                NoMoneyText.text = "NO MONEY \n NOOOOO";
            StartCoroutine(STOP());
        }
        
        
    }
    private IEnumerator STOP()
    {
        yield return new WaitForSeconds(.5f);
        NoMoneyText.text = "";
        
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
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.Cats[0] = pussy_cat[0];
        data.Cats[1] = pussy_cat[1];
        data.Cats[2] = pussy_cat[2];
        data.Cats[3] = pussy_cat[3];
        data.Cats[4] = pussy_cat[4];
        data.Cats[5] = pussy_cat[5];
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

}
