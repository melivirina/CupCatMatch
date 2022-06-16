using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class SaveData
{
    public int[] Cats = new int[6];
}

[Serializable]
class Save
{
    public int[] UseCats = new int[6];
}


