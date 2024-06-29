using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent foodIs≈aten = new UnityEvent();
    public static UnityEvent snakeDied = new UnityEvent();

    public static void OnFoodIsEaten()
    {
        foodIs≈aten?.Invoke();
    }

    public static void OnSnakeDied()
    {
        snakeDied?.Invoke();
    }
}