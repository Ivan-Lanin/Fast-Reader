using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManger : MonoBehaviour
{
    private SaveData saveData;

    private void Awake()
    {
        saveData = LoadData();
        Controls.OnPlayButtonPressed += SaveData;
        Player.OnWordIndexChange += SetCurrentWordIndex;
    }

    private SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
            saveData.currentWordIndexData = 0;
            return new SaveData();
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(saveData);
        string path = Application.persistentDataPath + "/saveData.json";
        System.IO.File.WriteAllText(path, json);
    }

    public void SetCurrentWordIndex(int index)
    {
        saveData.currentWordIndexData = index;
    }

    public int GetCurrentWordIndex()
    {
        return saveData.currentWordIndexData;
    }
}

[SerializeField]
public class SaveData
{ 
    public int currentWordIndexData;
}

