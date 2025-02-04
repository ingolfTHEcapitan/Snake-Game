using UnityEngine;

namespace SnakeGame.Snake
{
	public class SnakeBodyCollision : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Obstacle"))
			{
				GameEvents.OnSnakeDied();
			}
		}
	}
}
