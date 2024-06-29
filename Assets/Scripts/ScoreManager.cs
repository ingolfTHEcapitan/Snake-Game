using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hightScoreText;
    private int scoreNumber;


    private void Start()
    {
        // Устанавливаем текст для поля High Score из сохраненных данных.
        hightScoreText.SetText($"{PlayerPrefs.GetInt("hightScore", 0)}");
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
    private void UpdateScore()
    {
        // Проверяем, превышает ли текущий счет лучший результат.
        if (scoreNumber > PlayerPrefs.GetInt("hightScore", 0))
        {
            // Если да, обновляем лучший результат в сохраненных данных.
            PlayerPrefs.SetInt("hightScore", scoreNumber);

            // Обновляем текст для поля "Лучший результат" на экране.
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