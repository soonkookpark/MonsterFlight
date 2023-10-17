using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    
    public GameObject settingMenuUI;
    private static bool IsGamePaused = false;
    private static float nowTimeScale;



    public void OpenSetting()
    {
        if (!IsGamePaused)
        {
            nowTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            settingMenuUI.SetActive(true);
            IsGamePaused = true;
        }
        else if(IsGamePaused)
        {
            Debug.Log("гоюл");
            Time.timeScale = nowTimeScale;
            settingMenuUI.SetActive(false);
            IsGamePaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.Restart();
        //gameOverMsg.SetActive(false);
    }
}
