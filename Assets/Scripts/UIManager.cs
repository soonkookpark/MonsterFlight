using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �ڵ�
using UnityEngine.UI; // UI ���� �ڵ�
using TMPro;

// �ʿ��� UI�� ��� �����ϰ� ������ �� �ֵ��� ����ϴ� UI �Ŵ���
public class UIManager : MonoBehaviour
{
    public LayoutElement layoutElement;
    // �̱��� ���ٿ� ������Ƽ
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


    public TextMeshProUGUI scoreText; // ���� ǥ�ÿ� �ؽ�Ʈ
    public TextMeshProUGUI lifeText; // �� ���̺� ǥ�ÿ� �ؽ�Ʈ
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ�� UI 
    public TextMeshProUGUI HighScore;

    // ���� �ؽ�Ʈ ����
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