using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int scoreNumber;


    private void Start()
    {
        // ������������� ����� ��� ���� High Score �� ����������� ������.
        highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
    }

    private void Awake()
    {
        EventManager.foodIs�aten.AddListener(IncreaseScore);
        EventManager.snakeDied.AddListener(ResetScore);
    }
    private void IncreaseScore()
    {
        scoreNumber++;
        // ��������� ����� ��� ���� �������� �����.
        scoreText.text = $"{scoreNumber}";
        // ���������, ���������� �� ����� ������, � ��������� ��� ��� �������������.
        UpdateScore();
    }

    private void ResetScore()
    {
        scoreNumber = 0;
        scoreText.SetText($"{scoreNumber}");
        SetTextColor(Color.white);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
        highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
    }

    private void UpdateScore()
    {
        // ���������, ��������� �� ������� ���� ������ ���������.
        if (scoreNumber > PlayerPrefs.GetInt("highScore", 0))
        {
            // ���� ��, ��������� ������ ��������� � ����������� ������.
            PlayerPrefs.SetInt("highScore", scoreNumber);

            // ��������� ����� ��� ���� "������ ���������" �� ������.
            highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", scoreNumber)}");

            SetTextColor(Color.yellow);

        }
    }

    private void SetTextColor(Color color)
    {
        scoreText.color = color;
        highScoreText.color = color;
    }
}