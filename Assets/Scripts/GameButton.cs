using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    
    public GameObject settingMenuUI;
    private static bool IsGamePaused = false;
    private static float nowTimeScale;
    public GameObject SoundMenuUI;
    private static bool IsSoundMenuOpen = false;



    public void OpenSetting()
    {
        if (!IsGamePaused&&!GameManager.Instance.IsGameOver)
        {
            nowTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            settingMenuUI.SetActive(true);
            IsGamePaused = true;
        }
        else if(IsGamePaused&&!IsSoundMenuOpen&&settingMenuUI)
        {
            //Debug.Log("гоюл");
            Time.timeScale = nowTimeScale;
            settingMenuUI.SetActive(false);
            IsGamePaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.Restart();
        //gameOverMsg.SetActive(false);
    }
    public void SoundMenu()
    {
        if(!IsSoundMenuOpen)
        {
            SoundMenuUI.SetActive(true);
            IsSoundMenuOpen = true;
        }
        else if(IsSoundMenuOpen)
        {
            SoundMenuUI.SetActive(false);
            IsSoundMenuOpen = false;
        }
    }

    
}
