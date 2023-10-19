using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using TMPro;

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager instance already exists, destroying this one.");
            Destroy(gameObject);
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public TextMeshProUGUI scoreText; // 점수 표시용 텍스트
    public TextMeshProUGUI lifeText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public TextMeshProUGUI HighScore;

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore)
    {
        var scoreTextEx = ("Score : " + newScore).ToString();
        scoreText.SetText(scoreTextEx); //= "Score : " + newScore;
    }

    public void UpdateLifeText(int life)
    {
        var lifeTextEx = ("Life : " + life).ToString();
        lifeText.SetText(lifeTextEx); //= "Score : " + newScore;
    }

    public void PrintHighScore(int highScore)
    {
        var lifeTextEx = ("HIGH SCORE : " + highScore).ToString();
        lifeText.SetText(lifeTextEx); //= "Score : " + newScore;
    }
}