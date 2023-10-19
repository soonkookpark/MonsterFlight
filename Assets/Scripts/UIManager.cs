using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �ڵ�
using UnityEngine.UI; // UI ���� �ڵ�
using TMPro;

// �ʿ��� UI�� ��� �����ϰ� ������ �� �ֵ��� ����ϴ� UI �Ŵ���
public class UIManager : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
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

    private static UIManager m_instance; // �̱����� �Ҵ�� ����

    public TextMeshProUGUI scoreText; // ���� ǥ�ÿ� �ؽ�Ʈ
    public TextMeshProUGUI lifeText; // �� ���̺� ǥ�ÿ� �ؽ�Ʈ
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ�� UI 
    public TextMeshProUGUI HighScore;

    // ���� �ؽ�Ʈ ����
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