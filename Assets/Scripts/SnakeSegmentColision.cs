using UnityEngine;

public class SnakeSegmentColision : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Food")
		{
			EventManager.OnFoodIsEaten();
		}
		else if (collision.tag == "Obstacle" )
		{
			EventManager.OnSnakeDied();
		}
	}
}
