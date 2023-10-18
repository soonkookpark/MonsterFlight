using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
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

    //�پ��� �ػ� �����Ϸ��� ���־���.
    private int screenWidth = 720;
    private int screenHeight = 1280;
    private bool IsFullScreen = false;

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
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

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
        Debug.Log(Time.deltaTime);
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

    private int highScore = 0; // �ְ� ���� ���� �߰�

    // ...

    public void OnPlayerDead()
    {
        gameOverMsg.SetActive(true);
        Time.timeScale = 0;
        IsGameOver = true;

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


    public void AddScore(int newScore)
    {
        // ���� ������ �ƴ� ���¿����� ���� ���� ����
        if (!IsGameOver)
        {
            // ���� �߰�
            score += newScore;
            // ���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateScoreText(score);
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
