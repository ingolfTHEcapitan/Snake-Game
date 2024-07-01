using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int scoreNumber;


    private void Start()
    {
        // Устанавливаем текст для поля High Score из сохраненных данных.
        highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
    }

    private void Awake()
    {
        EventManager.foodIsЕaten.AddListener(IncreaseScore);
        EventManager.snakeDied.AddListener(ResetScore);
    }
    private void IncreaseScore()
    {
        scoreNumber++;
        // Обновляем текст для поля текущего счета.
        scoreText.text = $"{scoreNumber}";
        // Проверяем, установлен ли новый рекорд, и обновляем его при необходимости.
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
        // Проверяем, превышает ли текущий счет лучший результат.
        if (scoreNumber > PlayerPrefs.GetInt("highScore", 0))
        {
            // Если да, обновляем лучший результат в сохраненных данных.
            PlayerPrefs.SetInt("highScore", scoreNumber);

            // Обновляем текст для поля "Лучший результат" на экране.
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