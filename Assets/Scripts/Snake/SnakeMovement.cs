
using UnityEngine;

namespace SnakeGame.Snake
{
	[RequireComponent(typeof(SnakeBodyController))]
	public class SnakeMovement : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 0.12f;

		private const float MaxMovementSpeed = 0.05f;
		private const float MinMovementSpeed = 0.12f;
		private bool _isAddingNewSegment;
		private SnakeBodyController _body;

		private void Awake()
		{
			Time.fixedDeltaTime = _movementSpeed;
		
			_body = GetComponent<SnakeBodyController>();
		
			EventBus.FoodEaten.AddListener(IncreaseMovementSpeed);
			EventBus.SnakeDied.AddListener(ResetMovementSpeed);
		}

		private void IncreaseMovementSpeed()
		{
			_isAddingNewSegment = true;
		
			if (Time.fixedDeltaTime > MaxMovementSpeed)
			{
				_movementSpeed -= 0.002f;
				Time.fixedDeltaTime = _movementSpeed;
			}
		}

		private void ResetMovementSpeed()
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
				_body.AddSegment();
				_isAddingNewSegment = false;
			}
		
			_body.UpdateSegmentPositions(InputHandler.CurrentDirection);
			EventBus.OnGraphicsUpdateRequested();
		}
	}
}
