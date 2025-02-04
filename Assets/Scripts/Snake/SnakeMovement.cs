
using UnityEngine;

namespace SnakeGame.Snake
{
	public class SnakeMovement : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 0.12f;

		private const float MaxMovementSpeed = 0.05f;
		private const float MinMovementSpeed = 0.12f;
		private bool _isAddingNewSegment  = false;
		private SnakeBodyController _snakeBody;

		private void Awake()
		{
			Time.fixedDeltaTime = _movementSpeed;
		
			_snakeBody = GetComponent<SnakeBodyController>();
		
			GameEvents.FoodIsEaten.AddListener(OnFoodEaten);
			GameEvents.SnakeDied.AddListener(OnSnakeDied);
		}

		private void OnFoodEaten()
		{
			_isAddingNewSegment = true;
		
			if (Time.fixedDeltaTime > MaxMovementSpeed)
			{
				_movementSpeed -= 0.002f;
				Time.fixedDeltaTime = _movementSpeed;
			}
		}

		private void OnSnakeDied()
		{
			_movementSpeed = MinMovementSpeed; 
			Time.fixedDeltaTime = _movementSpeed;
		}
	
		private void FixedUpdate()
		{
			MoveSnake();
		}

		private void MoveSnake()
		{
			if (_isAddingNewSegment)
			{
				_snakeBody.AddSegment();
				_isAddingNewSegment = false;
			}
		
			_snakeBody.UpdateSegmentPositions(InputHandler.CurrentDirection);
			GameEvents.OnUpdateGraphics();
		}
	}
}
