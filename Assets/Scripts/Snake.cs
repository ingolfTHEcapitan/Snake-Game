using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	private const float FIXED_TIMESTEP = 0.12f;
	
	[SerializeField] private GameObject _segmentPrefab; // Префаб сегмента змейки
	[SerializeField] private Sprite _headUp, _headDown, _headLeft, _headRight;
	[SerializeField] private Sprite _tailUp, _tailDown, _tailLeft, _tailRight;
	[SerializeField] private Sprite _bodyVertical, _bodyHorizontal, _bodyTL, _bodyTR, _bodyBL, _bodyBR;
	
	private List<Transform> _body = new List<Transform>(); // Список для хранения трансформов частей тела змейки
	private Vector2 _direction = Vector2.right;
	private Vector2 _previousDirection = Vector2.zero;
	private bool _shouldAddNewSegment = false; // Переменная для добавления нового блока


	private void Awake()
	{
		// Устанавливаем fixedTimestep
		Time.fixedDeltaTime = FIXED_TIMESTEP;
		
		EventManager.FoodЕatenEvent.AddListener(()=>_shouldAddNewSegment = true);
		EventManager.SnakeDiedEvent.AddListener(InizializeGame);
	}

	void Start()
	{
		InizializeGame();
	}

	void Update()
	{
		// Направление движения змейки на основе ввода
		if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
		{
			_direction = Vector2.up;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
		{
			_direction = Vector2.down;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
		{
			_direction = Vector2.left;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
		{
			_direction = Vector2.right;
			CheckDirectionChange();
		}
	}

	private void CheckDirectionChange()
	{
		if (_direction != _previousDirection)
		{
			EventManager.OnSnakeMoved();
			_previousDirection = _direction; // Обновляем предыдущее направление
		}
	}

	private void FixedUpdate()
	{
		MoveSnake();
	}

	private void InizializeGame()
	{
		 // Удаляем все сегменты, кроме головы
		for (int i = 0; i < _body.Count; i++)
		{
			Destroy(_body[i].gameObject);
		}

		// Очищаем списки
		_body.Clear();
		// Очищаем начальные позиции
		Vector2[] initialPositions = new Vector2[0];
		
		 // Сбрасываем направление и другие параметры
		_direction = Vector2.right;
		_shouldAddNewSegment = false;
		
		// Инициализация змейки
		initialPositions = new Vector2[] { new (0, 0),new (-1, 0),new (-2, 0) };

		foreach (var pos in initialPositions)
		{
			GameObject block = Instantiate(_segmentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
			_body.Add(block.transform);
		}

		Time.timeScale = 0;
		UpdateHeadGraphics();
		UpdateTailGraphics();
	}

	private void MoveSnake()
	{
		List<Vector2> bodyCopy = new List<Vector2>();

		if (_shouldAddNewSegment)
		{
			foreach (var block in _body)
			{
				bodyCopy.Add(block.position);
			}
			bodyCopy.Insert(0, bodyCopy[0] + _direction);
		
			Vector3 newSegmentPosition = _body[^1].position;
			GameObject newSegmentObject = Instantiate(_segmentPrefab, newSegmentPosition, Quaternion.identity);
			_body.Add(newSegmentObject.transform);
			
			for (int i = 0; i < _body.Count; i++)
			{
				_body[i].position = new Vector3(bodyCopy[i].x, bodyCopy[i].y, 0);			
			}
			_shouldAddNewSegment = false;		
		}
		else
		{
			foreach (var block in _body)
			{
				bodyCopy.Add(block.position);
			}

			bodyCopy.Insert(0, bodyCopy[0] + _direction);
			bodyCopy.RemoveAt(bodyCopy.Count - 1);

			for (int i = 0; i < _body.Count; i++)
			{
				_body[i].position = new Vector3(bodyCopy[i].x, bodyCopy[i].y, 0);
			}
		}
		
		DrawSnake();
	}

	private void DrawSnake()
	{
		UpdateHeadGraphics();
		UpdateTailGraphics();

		for (int index = 0; index < _body.Count; index++)
		{
			Transform block = _body[index];
			Vector3 position = block.position;
			SpriteRenderer renderer = block.GetComponent<SpriteRenderer>();

			if (index == 0)
				renderer.sprite = GetHeadSprite();
				
			else if (index == _body.Count - 1)
				renderer.sprite = GetTailSprite();
				
			else
			{
				Vector2 previousBlock = _body[index + 1].position - position;
				Vector2 nextBlock = _body[index - 1].position - position;

				if (previousBlock.x == nextBlock.x)
					renderer.sprite = _bodyVertical;
					
				else if (previousBlock.y == nextBlock.y)
					renderer.sprite = _bodyHorizontal;

				else
				{
					if ((previousBlock.x == -1 && nextBlock.y == -1) || (previousBlock.y == -1 && nextBlock.x == -1))
						renderer.sprite = _bodyBL;
						
					else if ((previousBlock.x == -1 && nextBlock.y == 1) || (previousBlock.y == 1 && nextBlock.x == -1))
						renderer.sprite = _bodyTL;
						
					else if ((previousBlock.x == 1 && nextBlock.y == -1) || (previousBlock.y == -1 && nextBlock.x == 1))
						renderer.sprite = _bodyBR;
						
					else if ((previousBlock.x == 1 && nextBlock.y == 1) || (previousBlock.y == 1 && nextBlock.x == 1))
						renderer.sprite = _bodyTR;
				}
			}
		}
	}

	private void UpdateHeadGraphics()
	{
		Vector2 headRelation = _body[1].position - _body[0].position;
		SpriteRenderer snakeHead = _body[0].GetComponent<SpriteRenderer>();
		
		if (headRelation == Vector2.right) snakeHead.sprite = _headLeft;
		else if (headRelation == Vector2.left) snakeHead.sprite = _headRight;
		else if (headRelation == Vector2.up) snakeHead.sprite = _headDown;
		else if (headRelation == Vector2.down) snakeHead.sprite = _headUp;
	}

	private void UpdateTailGraphics()
	{
		//  body[^2] означет второй жлемент с конца, ^ - символ для обозначения индекса с обратной стороны
		Vector2 tailRelation = _body[^2].position - _body[^1].position;
		SpriteRenderer snakeTail = _body[^1].GetComponent<SpriteRenderer>();
		
		if (tailRelation == Vector2.right) snakeTail.sprite = _tailLeft;		
		else if (tailRelation == Vector2.left) snakeTail.sprite = _tailRight;
		else if (tailRelation == Vector2.up) snakeTail.sprite = _tailDown;
		else if (tailRelation == Vector2.down) snakeTail.sprite = _tailUp;
	}

	private Sprite GetHeadSprite()
	{
		Vector2 headRelation = _body[1].position - _body[0].position;
		if (headRelation == Vector2.right) return _headLeft;
		if (headRelation == Vector2.left) return _headRight;
		if (headRelation == Vector2.up) return _headDown;
		return _headUp;
	}

	private Sprite GetTailSprite()
	{
		Vector2 tailRelation = _body[^2].position - _body[^1].position;
		if (tailRelation == Vector2.right) return _tailLeft;
		if (tailRelation == Vector2.left) return _tailRight;
		if (tailRelation == Vector2.up) return _tailDown;
		return _tailUp;
	}
}


