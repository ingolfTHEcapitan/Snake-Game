using UnityEngine.Events;

namespace SnakeGame
{
	public static class EventBus
	{
		public static readonly UnityEvent FoodEaten = new UnityEvent();
		public static readonly UnityEvent SnakeDied = new UnityEvent();
		public static readonly UnityEvent SnakeMoved = new UnityEvent();
		public static readonly UnityEvent GraphicsUpdateRequested = new UnityEvent();

		public static void OnFoodEaten() => FoodEaten?.Invoke();
		public static void OnSnakeDied() => SnakeDied?.Invoke();
		public static void OnSnakeMoved() => SnakeMoved?.Invoke();
		public static void OnGraphicsUpdateRequested() => GraphicsUpdateRequested?.Invoke();
	
	}
}