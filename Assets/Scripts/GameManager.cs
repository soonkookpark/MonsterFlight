using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    public GameObject gameOverMsg;
    public bool IsGameOver { get; private set; }

    private int score = 0;
    private int callScore;
    private float gameSpeed = 0.05f;

    //�پ��� �ػ� �����Ϸ��� ���־���.
//    private int screenWidth = 720;
  //  private int screenHeight = 1280;
    private bool IsFullScreen = false;
    private int highScore = 0; // �ְ� ���� ���� �߰�
    public int CurrentScore => score;
    // Start is called before the first frame update
    private void Awake()
    {
        //Screen.SetResolution(screenWidth, screenHeight, !IsFullScreen);
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
        //���ø����̼�
        //Application.targetFrameRate = 60;


        LoadHighScore();
        LoadSoundSettings();
        if (Instance != null)
            HighScorePrint(highScore);
    }
    public int CoinCount { get; private set; }

    public void IncreaseCoinCount()
    {
        CoinCount++;
    }
    public void SaveSoundSettings(string key, float value)
    {
        var saveFileName = "save_data.json";
        var saveData = LoadSaveFile();

        switch (key)
        {
            case "Master":
                saveData.MasterVolume = value;
                break;
            case "BGM":
                saveData.BgmVolume = value;
                break;
            case "Effect":
                saveData.EffectVolume = value;
                break;
        }

        SaveLoadSystem.Save(saveData, saveFileName);

        Debug.Log(key + " sound setting saved: " + value);
    }

    private void LoadSoundSettings()
    {

        var audioMixController = FindObjectOfType<AudioMixController>();

        if (audioMixController != null)
        {
            var savedData = LoadSaveFile();
            audioMixController.SetMasterVolume(LoadSaveFile().MasterVolume);
            audioMixController.SetBGMVolume(LoadSaveFile().BgmVolume);
            audioMixController.SetEffectVolume(LoadSaveFile().EffectVolume);
            audioMixController.m_MusicMasterSlider.value = savedData.MasterVolume;
            audioMixController.m_MusicBGMSlider.value = savedData.BgmVolume;
            audioMixController.m_MusicEffectSlider.value = savedData.EffectVolume;
        }
        else
        {
            Debug.Log("No AudioMixController Found");
        }
    }
    private SaveDataV1 LoadSaveFile()
    {
        var saveFileName = "save_data.json";
        var saveData = SaveLoadSystem.Load(saveFileName);

        if (saveData == null)
            return new SaveDataV1();

        return saveData as SaveDataV1;
    }
    private void Start()
    {
        //LoadHighScore();
        //if(Instance != null)
        //HighScorePrint(highScore);
    }
    private void LoadHighScore()
    {
        //var saveFileName = "save_data.json";

        var saveData = LoadSaveFile();

        if (saveData != null && saveData is SaveDataV1 data)
        {
            highScore = data.HighScore;

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

        if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale *= 1.1f;
        }
        if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale *= 0.9f;
        }
        if (!IsGameOver && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 1f;
        }
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
        CoinCount = 0;
        gameOverMsg.SetActive(false);
        if (EnemySpawner.instance != null)
        {
            Destroy(EnemySpawner.instance.gameObject);
            EnemySpawner.instance = null;
        }
    }
    public void OnPlayerDead()
    {
        gameOverMsg.SetActive(true);
        Time.timeScale = 0;
        IsGameOver = true;

        CheckHighscore();
    }
    public void CheckHighscore()
    {
        if (score > highScore) // ���� ������ �ְ� �������� ���� ���
        {
            highScore = score; // �ְ� ���� ����
            SaveHighScore(); // �ְ� ���� ����
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
        UIManager.instance.PrintHighScore(score);
    }

    public void AddScore(int newScore)
    {
        // ���� ������ �ƴ� ���¿����� ���� ���� ����
        if (!IsGameOver)
        {
            // ���� �߰�
            score += newScore;
            // ���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateScoreText(score,highScore);
            callScore++;
        }
    }
    public void UpdateLife(int life)
    {
        // ���� ������ �ƴ� ���¿����� ���� ���� ����
        if (!IsGameOver)
        {
            // ���� �߰�
            // ���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateLifeText(life);

        }
    }

}
