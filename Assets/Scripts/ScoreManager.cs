using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hightScoreText;
    private int scoreNumber;


    private void Start()
    {
        // ������������� ����� ��� ���� High Score �� ����������� ������.
        hightScoreText.SetText($"{PlayerPrefs.GetInt("hightScore", 0)}");
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
    private void UpdateScore()
    {
        // ���������, ��������� �� ������� ���� ������ ���������.
        if (scoreNumber > PlayerPrefs.GetInt("hightScore", 0))
        {
            // ���� ��, ��������� ������ ��������� � ����������� ������.
            PlayerPrefs.SetInt("hightScore", scoreNumber);

            // ��������� ����� ��� ���� "������ ���������" �� ������.
            hightScoreText.SetText($"{PlayerPrefs.GetInt("hightScore", scoreNumber)}");

            SetTextColor(Color.yellow);

        }
    }

    private void SetTextColor(Color color)
    {
        scoreText.color = color;
        hightScoreText.color = color;
    }
}