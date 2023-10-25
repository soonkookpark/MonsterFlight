using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Destroy(EnemySpawner.instance.gameObject);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Lobby");  // "MainMenu"는 메인 메뉴 씬의 이름입니다.
        Time.timeScale = 1f;  // 시간 스케일을 원래대로 복구합니다.
    }
    
}
