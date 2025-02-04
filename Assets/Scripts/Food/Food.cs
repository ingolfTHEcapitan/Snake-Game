using UnityEngine;

namespace SnakeGame.Food
{
	public class Food : MonoBehaviour
	{
		private GridBounds _gridManager;

		private static Food Instance {get; set;}

		private void Start()
		{
			_gridManager = FindAnyObjectByType<GridBounds>();
		}

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(gameObject);
		
			EventBus.SnakeDied.AddListener(()=>Destroy(gameObject));
			EventBus.FoodEaten.AddListener(SetRandomPosition);
		}
	
		private void SetRandomPosition()
		{
			transform.position = _gridManager.GetRandomPosition();
		}
	}
}



   
