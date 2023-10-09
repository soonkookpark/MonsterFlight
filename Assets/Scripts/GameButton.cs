using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public GameObject settingMenuUI;
    private bool IsGamePaused = false;
    private float nowTimeScale;


    public void OpenSetting()
    {
        if(!IsGamePaused)
        {
            nowTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            settingMenuUI.SetActive(true);
            IsGamePaused = true;
        }
        else if(IsGamePaused)
        {
            Time.timeScale = nowTimeScale;
            settingMenuUI.SetActive(false);
            IsGamePaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOverMsg.SetActive(false);
    }
}
