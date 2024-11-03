using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
	public static UnityEvent Food�aten = new UnityEvent();
	public static UnityEvent SnakeDied = new UnityEvent();
	public static UnityEvent SnakeMoved = new UnityEvent();
	public static UnityEvent UpdateGraphics = new UnityEvent();

	public static void OnFoodEaten() => Food�aten?.Invoke();
	public static void OnSnakeDied() => SnakeDied?.Invoke();
	public static void OnSnakeMoved() => SnakeMoved?.Invoke();
	public static void OnUpdateGraphics() => UpdateGraphics?.Invoke();
	
}