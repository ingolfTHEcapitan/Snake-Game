using UnityEngine;

namespace SnakeGame
{
	public class InputHandler : MonoBehaviour
	{
		private InputActions _inputActions;
		private Vector2 _previousDirection = Vector2.zero;
	
		public static Vector2 CurrentDirection {get; private set;} = Vector2.right;

		private void Awake()
		{
			_inputActions = new InputActions();
			_inputActions.Enable();
			_inputActions.Snake.Movement.performed += context => UpdateDirection(context.ReadValue<Vector2>());
		
			EventBus.SnakeDied.AddListener(ResetSnakeDirection);
		}

		private void UpdateDirection(Vector2 inputDirection)
		{
			bool isDiagonalMovement = Mathf.Abs(inputDirection.x) > 0 && Mathf.Abs(inputDirection.y) > 0;
			bool isOppositeDirection = inputDirection == -CurrentDirection;
			bool isDirectionChanged = inputDirection != _previousDirection;
		
			if (isDiagonalMovement || isOppositeDirection) return;

			if (isDirectionChanged)
			{
				CurrentDirection = inputDirection;
				_previousDirection = CurrentDirection;
				EventBus.OnSnakeMoved();
			}
		}

		private void ResetSnakeDirection() => CurrentDirection = Vector2.right;
	}
}
