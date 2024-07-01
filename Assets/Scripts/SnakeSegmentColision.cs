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
			instanñe.PlaySound(instanñe.efxSource1, instanñe.eatFoodSound);
		}
		else if (collision.tag == "Obstacle" )
		{
			EventManager.OnSnakeDied();
			instanñe.PlaySound(instanñe.efxSource1, instanñe.gameOverSound);

		}
	}
}
