using UnityEngine;

namespace SnakeGame.Snake
{
	public class SnakeHeadCollision : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Food")) EventBus.OnFoodEaten();
			if (collision.CompareTag("Obstacle")) EventBus.OnSnakeDied();
		}
	}
}
