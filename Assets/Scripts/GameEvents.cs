using UnityEngine.Events;

namespace SnakeGame
{
	public static class GameEvents
	{
		public static readonly UnityEvent FoodIsEaten = new UnityEvent();
		public static readonly UnityEvent SnakeDied = new UnityEvent();
		public static readonly UnityEvent SnakeMoved = new UnityEvent();
		public static readonly UnityEvent UpdateGraphics = new UnityEvent();

		public static void OnFoodEaten() => FoodIsEaten?.Invoke();
		public static void OnSnakeDied() => SnakeDied?.Invoke();
		public static void OnSnakeMoved() => SnakeMoved?.Invoke();
		public static void OnUpdateGraphics() => UpdateGraphics?.Invoke();
	
	}
}