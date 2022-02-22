using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveData : MonoBehaviour
{
    public DataStore dataStore;
    private string path;
    private const string fileName = "/scores.tbl";
    public static SaveData Instance;

    [System.Serializable]
    public class DataStore
    {
        public PlayerData bestPlayer;
        public string lastPlayerName;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public string name;
        public int score;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath;
        dataStore = new DataStore();
        LoadScore();
    }

    public void SaveScore()
    {
        string json = JsonUtility.ToJson(dataStore);
        File.WriteAllText(path + fileName, json);
    }

    public void LoadScore()
    {
        if (File.Exists(path + fileName))
        {
            string json = File.ReadAllText(path + fileName);
            dataStore = JsonUtility.FromJson<DataStore>(json);
        }
    }
}
