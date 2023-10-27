using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using TMPro;

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour
{
    public LayoutElement layoutElement;
    // 싱글톤 접근용 프로퍼티
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    Debug.LogError("UIManager instance is not found.");
                }
            }

            return _instance;
        }
    }
    public void Awake()
    {
        SetNotch();
    }


    public TextMeshProUGUI scoreText; // 점수 표시용 텍스트
    public TextMeshProUGUI lifeText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public TextMeshProUGUI HighScore;

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore,int highScore)
    {
        var scoreTextEx = ("Score : " + newScore).ToString();
        scoreText.SetText(scoreTextEx); //= "Score : " + newScore;
        if (GameManager.Instance.CurrentScore > highScore)
            scoreText.color = Color.red;
    }

    public void UpdateLifeText(int life)
    {
        var lifeTextEx = ("Life : " + life).ToString();
        lifeText.SetText(lifeTextEx); //= "Score : " + newScore;
    }

    public void PrintHighScore(int highScore)
    {
        var HighScoreEx = ("HIGH SCORE : " + highScore).ToString();
        HighScore.SetText(HighScoreEx); //= "Score : " + newScore;
    }

    public void SetNotch()
    {
        var notchSize = Screen.safeArea.y;
        if (layoutElement != null)
        {
            layoutElement.minHeight = notchSize;
        }
        else
        {
            Debug.LogError("LayoutElement is not assigned.");
        }
    }
}