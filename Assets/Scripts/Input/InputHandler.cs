using UnityEngine;

public class InputHandler : MonoBehaviour
{
	private InputActions _inputActions;
	private Vector2 _previousDirection = Vector2.zero;
	
	public static Vector2 CurrentDirection {get; private set;} = Vector2.right;

	private void Awake()
	{
		_inputActions = new InputActions();
		_inputActions.Enable();
		_inputActions.Snake.Movement.performed += contex => UpdateDirection(contex.ReadValue<Vector2>());
		
		GameEvents.SnakeDied.AddListener(ResetSnakeDirecction);
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
			GameEvents.OnSnakeMoved();
		}
	}

	private void ResetSnakeDirecction() => CurrentDirection = Vector2.right;
}
