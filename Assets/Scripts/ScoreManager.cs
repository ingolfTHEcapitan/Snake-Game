using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI HighScoreText;
	private int _scoreCount;

	private void Awake()
	{
		GameEvents.FoodÅaten.AddListener(IncreaseScore);
		GameEvents.SnakeDied.AddListener(ResetScore);
	}
	
	private void Start()
	{
		HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	private void IncreaseScore()
	{
		_scoreCount++;
		ScoreText.text = $"{_scoreCount}";
		UpdateScore();
	}

	private void UpdateScore()
	{
		if (_scoreCount > PlayerPrefs.GetInt("highScore", 0))
		{
			PlayerPrefs.SetInt("highScore", _scoreCount);

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