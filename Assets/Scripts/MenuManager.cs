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
        if (SaveData.Instance.dataStore.bestPlayer.score > 0)
            txtBestScore.text = $"Best Score : {SaveData.Instance.dataStore.bestPlayer.name} : {SaveData.Instance.dataStore.bestPlayer.score}";
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
