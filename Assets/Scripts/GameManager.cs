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

    //다양한 해상도 지원하려면 없애야함.
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

        if(!IsGameOver && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale *= 1.1f;
        }
        if(!IsGameOver && Input.GetKeyDown(KeyCode.Alpha2))
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
        
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMsg.SetActive(false);
    }

    public void OnPlayerDead()
    {
        gameOverMsg.SetActive(true);
        Time.timeScale = 0;
        IsGameOver = true;
    }

    public void AddScore(int newScore)
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!IsGameOver)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score);
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
