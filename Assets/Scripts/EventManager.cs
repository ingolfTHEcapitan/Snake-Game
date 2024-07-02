using UnityEngine.Events;

public class EventManager
{
	public static UnityEvent Food≈atenEvent = new UnityEvent();
	public static UnityEvent SnakeDiedEvent = new UnityEvent();
	public static UnityEvent SnakeMovedEvent = new UnityEvent();

	public static void OnFoodEaten()
	{
		Food≈atenEvent?.Invoke();
	}

	public static void OnSnakeDied()
	{
		SnakeDiedEvent?.Invoke();
	}
	
	public static void OnSnakeMoved()
	{
		SnakeMovedEvent?.Invoke();
	}
}