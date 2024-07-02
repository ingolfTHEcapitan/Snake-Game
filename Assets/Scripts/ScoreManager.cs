using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI HighScoreText;
	private int _scoreCount;

	private void Awake()
	{
		EventManager.FoodЕatenEvent.AddListener(IncreaseScore);
		EventManager.SnakeDiedEvent.AddListener(ResetScore);
	}
	
	private void Start()
	{
		// Устанавливаем текст для поля High Score из сохраненных данных.
		HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	private void IncreaseScore()
	{
		_scoreCount++;
		// Обновляем текст для поля текущего счета.
		ScoreText.text = $"{_scoreCount}";
		// Проверяем, установлен ли новый рекорд, и обновляем его при необходимости.
		UpdateScore();
	}

	private void UpdateScore()
	{
		// Проверяем, превышает ли текущий счет лучший результат.
		if (_scoreCount > PlayerPrefs.GetInt("highScore", 0))
		{
			// Если да, обновляем лучший результат в сохраненных данных.
			PlayerPrefs.SetInt("highScore", _scoreCount);

			// Обновляем текст для поля "Лучший результат" на экране.
			HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", _scoreCount)}");

			SetTextColor(Color.yellow);

		}
	}

	private void SetTextColor(Color color)
	{
		ScoreText.color = color;
		HighScoreText.color = color;
	}
	
	private void ResetScore()
	{
		_scoreCount = 0;
		ScoreText.SetText($"{_scoreCount}");
		SetTextColor(Color.white);
	}

	public void ResetHighScore()
	{
		PlayerPrefs.DeleteKey("highScore");
		HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	
}