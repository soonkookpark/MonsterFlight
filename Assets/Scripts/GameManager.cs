using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }


    public int score = 0;

    // Start is called before the first frame update
    private void Awake()
    {
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

    private void Start()
    {

    }

    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1.3f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1.0f;
        }
    }

    public void OnPlayerDead()
    {
        Time.timeScale = 0;
        IsGameOver = true;
    }
}
