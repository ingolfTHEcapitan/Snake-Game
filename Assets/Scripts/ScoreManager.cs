using TMPro;
using UnityEngine;

namespace SnakeGame
{
	public class ScoreManager : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _highScoreText;
		private int _scoreCount;

		private void Awake()
		{
			GameEvents.FoodIsEaten.AddListener(IncreaseScore);
			GameEvents.SnakeDied.AddListener(ResetScore);
		}
	
		private void Start()
		{
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
		}
	
		private void IncreaseScore()
		{
			_scoreCount++;
			_scoreText.text = $"{_scoreCount}";
			UpdateScore();
		}

		private void UpdateScore()
		{
			if (_scoreCount <= PlayerPrefs.GetInt("highScore", 0)) return;
		
			PlayerPrefs.SetInt("highScore", _scoreCount);
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", _scoreCount)}");
		
			SetTextColor(Color.yellow);
		}

		private void SetTextColor(Color color)
		{
			_scoreText.color = color;
			_highScoreText.color = color;
		}
	
		private void ResetScore()
		{
			_scoreCount = 0;
			_scoreText.SetText($"{_scoreCount}");
			SetTextColor(Color.white);
		}

		public void ResetHighScore()
		{
			PlayerPrefs.DeleteKey("highScore");
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
		}
	}
}