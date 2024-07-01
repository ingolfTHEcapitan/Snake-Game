using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class SnakeSegmentColision : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Food")
		{
			EventManager.OnFoodIsEaten();
			instan�e.PlaySound(instan�e.efxSource1, instan�e.eatFoodSound);
		}
		else if (collision.tag == "Obstacle" )
		{
			EventManager.OnSnakeDied();
			instan�e.PlaySound(instan�e.efxSource1, instan�e.gameOverSound);

		}
	}
}
