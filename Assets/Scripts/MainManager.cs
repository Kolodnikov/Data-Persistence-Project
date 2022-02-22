using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    private int score;
    public Text txtScore;
    public Text txtBestScore;
    public GameObject txtGameOver;
    private bool isStarted = false;
    private bool isGameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        score = 0;
        txtBestScore.text = $"Best Score : {SaveData.Instance.dataStore.bestPlayer.name} : {SaveData.Instance.dataStore.bestPlayer.score}";

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        score += point;
        txtScore.text = $"Score : {score}";
    }

    public void GameOver()
    {
        if (score > SaveData.Instance.dataStore.bestPlayer.score)
        {
            SaveData.Instance.dataStore.bestPlayer.score = score;
            SaveData.Instance.dataStore.bestPlayer.name = SaveData.Instance.dataStore.lastPlayerName;
            SaveData.Instance.SaveScore();
        }
        isGameOver = true;
        txtGameOver.SetActive(true);
    }
}
