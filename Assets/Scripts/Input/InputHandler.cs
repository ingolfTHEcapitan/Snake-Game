using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
	private InputActions _inputActions;
	private Vector2 _currentDirection = Vector2.right;
	public Vector2 Direction {get => _direction; set { _direction = value; } }
	
	private Vector2 _direction = Vector2.right;
	private Vector2 _previousDirection = Vector2.zero;
	
	public static InputHandler Instanñe{get; set; }

	private void Awake()
	{
		if (Instanñe == null)
			Instanñe = this;
		else if (Instanñe != this)
			Destroy(gameObject);

		_inputActions = new InputActions();
		_inputActions.Enable();
		_inputActions.Snake.Movment.performed += contex => UpdateDirection(contex.ReadValue<Vector2>());
	}

	private void UpdateDirection(Vector2 inputDirection)
	{
		if (inputDirection != Vector2.zero && inputDirection != -_currentDirection)
		{
			_currentDirection = inputDirection.normalized;
			_direction = _currentDirection; 
			CheckDirectionChange();
		}
	}
	
	private void CheckDirectionChange()
	{
		if (_direction != _previousDirection)
		{
			EventManager.OnSnakeMoved();
			_previousDirection = _direction;
		}
	}
}
