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
        SceneManager.LoadScene("Lobby");  // "MainMenu"�� ���� �޴� ���� �̸��Դϴ�.
        Time.timeScale = 1f;  // �ð� �������� ������� �����մϴ�.
    }
    
}
