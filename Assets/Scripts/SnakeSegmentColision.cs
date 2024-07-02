using UnityEngine;

public class SnakeSegmentColision : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Food"))
		{
			EventManager.OnFoodEaten();
		}
		else if (collision.CompareTag("Obstacle"))
		{
			EventManager.OnSnakeDied();
		}
	}
}
