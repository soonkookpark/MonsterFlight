using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public GameObject gameOverMsg;
    public bool IsGameOver { get; private set; }

    private int score = 0;
    private int callScore;
    private float gameSpeed = 0.05f;

    //다양한 해상도 지원하려면 없애야함.
    private int screenWidth = 720;
    private int screenHeight = 1280;
    private bool IsFullScreen = false;
    private int highScore = 0; // 최고 점수 변수 추가
    public int CurrentScore => score;
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.SetResolution(screenWidth, screenHeight, !IsFullScreen);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager instance already exists, destroying this one.");
            //Destroy(gameObject);
            DontDestroyOnLoad(Instance);
        }

        LoadHighScore();

        if (Instance != null)
            HighScorePrint(highScore);
    }


    private void Start()
    {
        //LoadHighScore();
        //if(Instance != null)
        //HighScorePrint(highScore);
    }
    private void LoadHighScore()
    {
        var saveFileName = "save_data.json";

        var saveData = SaveLoadSystem.Load(saveFileName);

        if (saveData != null && saveData is SaveDataV1 saveDataV1)
        {
            highScore = saveDataV1.HighScore;

            Debug.Log("High score loaded: " + highScore);
        }
        else
        {
            Debug.LogWarning("No existing save data found.");
        }
    }
    private void Update()
    {
        //if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        //{
        //    Restart();
        //}

        //if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Time.timeScale *= 1.1f;
        //}
        //if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Time.timeScale *= 0.9f;
        //}
        //if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Time.timeScale = 1f;
        //}
        if (callScore>=10)
        {
            Time.timeScale += gameSpeed;
            callScore = 0;
        }
        //Debug.Log(Time.deltaTime);
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMsg.SetActive(false);
        if (EnemySpawner.instance != null)
        {
            Destroy(EnemySpawner.instance.gameObject);
            EnemySpawner.instance = null;
            
        }
    }


    // ...

    public void OnPlayerDead()
    {
        gameOverMsg.SetActive(true);
        Time.timeScale = 0;
        IsGameOver = true;

        CheckHighscore();
    }
    public void CheckHighscore()
    {
        if (score > highScore) // 현재 점수가 최고 점수보다 높을 경우
        {
            highScore = score; // 최고 점수 갱신
            SaveHighScore(); // 최고 점수 저장
        }
    }
    private void SaveHighScore()
    {
        var saveFileName = "save_data.json";

        var saveData = new SaveDataV1();

        saveData.HighScore = highScore;

        SaveLoadSystem.Save(saveData, saveFileName);

        Debug.Log("High score saved: " + highScore);
    }
    public void HighScorePrint(int score)
    {
        UIManager.instance.PrintHighScore(highScore);
    }

    public void AddScore(int newScore)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!IsGameOver)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score,highScore);
            callScore++;
        }
    }
    public void UpdateLife(int life)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!IsGameOver)
        {
            // 점수 추가
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateLifeText(life);

        }
    }

}
