using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	[SerializeField] private float _movementSpeed = 0.12f;

	private readonly float _maxMovementSpeed = 0.05f;
	private readonly float _minMovementSpeed = 0.12f;
	private bool _isAddingNewSegment  = false;

	private SnakeBodyController _snakeBody;

	private void Awake()
	{
		Time.fixedDeltaTime = _movementSpeed;
		
		_snakeBody = GetComponent<SnakeBodyController>();
		
		GameEvents.FoodÅaten.AddListener(OnFoodEaten);
		GameEvents.SnakeDied.AddListener(OnSnakeDied);
	}

	private void OnFoodEaten()
	{
		_isAddingNewSegment = true;
		
		if (Time.fixedDeltaTime > _maxMovementSpeed)
		{
			_movementSpeed -= 0.002f;
			Time.fixedDeltaTime = _movementSpeed;
		}
	}

	public void OnSnakeDied()
	{
		_movementSpeed = _minMovementSpeed; 
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
