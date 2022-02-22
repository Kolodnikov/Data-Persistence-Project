using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class MenuManager : MonoBehaviour
{
    public Text txtBestScore;

    private void Start()
    {
        GameObject.Find("edPlayerName").GetComponent<InputField>().text = SaveData.Instance.dataStore.lastPlayerName;
        for (int i = 0; i < SaveData.Instance.dataStore.bestPlayers.Length; i++)
        {
            if (SaveData.Instance.dataStore.bestPlayers[i].score > 0)
                GameObject.Find("txtScore" + i).GetComponent<Text>().text = $"{SaveData.Instance.dataStore.bestPlayers[i].name} : {SaveData.Instance.dataStore.bestPlayers[i].score}";
        }
    }

    public void StartNew()
    {
        SaveData.Instance.dataStore.lastPlayerName = GameObject.Find("edPlayerName").GetComponent<InputField>().text;
        SaveData.Instance.SaveScore();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }
}
