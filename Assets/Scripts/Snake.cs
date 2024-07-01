using System;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using static SoundManager;

public class Snake : MonoBehaviour
{
	public GameObject segmentPrefab; // Префаб сегмента змейки
	
	public Sprite headUp, headDown, headLeft, headRight;
	public Sprite tailUp, tailDown, tailLeft, tailRight;
	public Sprite bodyVertical, bodyHorizontal, bodyTL, bodyTR, bodyBL, bodyBR;

	private List<Transform> body = new List<Transform>(); // Список для хранения трансформов частей тела змейки
	
	private Vector2 direction = Vector2.right;
	private Vector2 previousDirection = Vector2.zero;
	
	private readonly float fixedTimestep = 0.12f;
	private bool newBlock = false; // Переменная для добавления нового блока


	private void Awake()
	{
		// Устанавливаем fixedTimestep
		Time.fixedDeltaTime = fixedTimestep;
		
		EventManager.foodIsЕaten.AddListener(()=>newBlock = true);
		EventManager.snakeDied.AddListener(InizializeGame);
	}

	void Start()
	{
		InizializeGame();
	}

	void Update()
	{
		// Направление движения змейки на основе ввода
		if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
		{
			direction = Vector2.up;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
		{
			direction = Vector2.down;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
		{
			direction = Vector2.left;
			CheckDirectionChange();
		}
		else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
		{
			direction = Vector2.right;
			CheckDirectionChange();
		}
	}

	private void CheckDirectionChange()
	{
		if (direction != previousDirection)
		{
			instanсe.PlaySound(instanсe.efxSource2, instanсe.moveSound);
			previousDirection = direction; // Обновляем предыдущее направление
		}
	}

	private void FixedUpdate()
	{
		MoveSnake();
	}

	private void InizializeGame()
	{
		 // Удаляем все сегменты, кроме головы
		for (int i = 0; i < body.Count; i++)
		{
			Destroy(body[i].gameObject);
		}

		// Очищаем списки
		body.Clear();
		// Очищаем начальные позиции
		Vector2[] initialPositions = new Vector2[0];
		
		 // Сбрасываем направление и другие параметры
		direction = Vector2.right;
		newBlock = false;
		
		// Инициализация змейки
		initialPositions = new Vector2[] { new (2, 2),new (1, 2),new (0, 2) };

		foreach (var pos in initialPositions)
		{
			GameObject block = Instantiate(segmentPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
			body.Add(block.transform);
		}

		Time.timeScale = 0;
		UpdateHeadGraphics();
		UpdateTailGraphics();
	}

	private void MoveSnake()
	{
		List<Vector2> bodyCopy = new List<Vector2>();

		if (newBlock)
		{
			foreach (var block in body)
			{
				bodyCopy.Add(block.position);
			}
			bodyCopy.Insert(0, bodyCopy[0] + direction);
		
			Vector3 newSegmentPosition = body[^1].position;
			GameObject newSegmentObject = Instantiate(segmentPrefab, newSegmentPosition, Quaternion.identity);
			body.Add(newSegmentObject.transform);
			
			for (int i = 0; i < body.Count; i++)
			{
				body[i].position = new Vector3(bodyCopy[i].x, bodyCopy[i].y, 0);			
			}
			newBlock = false;		
		}
		else
		{
			foreach (var block in body)
			{
				bodyCopy.Add(block.position);
			}

			bodyCopy.Insert(0, bodyCopy[0] + direction);
			bodyCopy.RemoveAt(bodyCopy.Count - 1);

			for (int i = 0; i < body.Count; i++)
			{
				body[i].position = new Vector3(bodyCopy[i].x, bodyCopy[i].y, 0);
			}
		}
		
		DrawSnake();
	}

	
	private void DrawSnake()
	{
		UpdateHeadGraphics();
		UpdateTailGraphics();

		for (int index = 0; index < body.Count; index++)
		{
			Transform block = body[index];
			Vector3 position = block.position;
			SpriteRenderer renderer = block.GetComponent<SpriteRenderer>();

			if (index == 0)
				renderer.sprite = GetHeadSprite();
				
			else if (index == body.Count - 1)
				renderer.sprite = GetTailSprite();
				
			else
			{
				Vector2 previousBlock = body[index + 1].position - position;
				Vector2 nextBlock = body[index - 1].position - position;

				if (previousBlock.x == nextBlock.x)
					renderer.sprite = bodyVertical;
					
				else if (previousBlock.y == nextBlock.y)
					renderer.sprite = bodyHorizontal;

				else
				{
					if ((previousBlock.x == -1 && nextBlock.y == -1) || (previousBlock.y == -1 && nextBlock.x == -1))
						renderer.sprite = bodyBL;
						
					else if ((previousBlock.x == -1 && nextBlock.y == 1) || (previousBlock.y == 1 && nextBlock.x == -1))
						renderer.sprite = bodyTL;
						
					else if ((previousBlock.x == 1 && nextBlock.y == -1) || (previousBlock.y == -1 && nextBlock.x == 1))
						renderer.sprite = bodyBR;
						
					else if ((previousBlock.x == 1 && nextBlock.y == 1) || (previousBlock.y == 1 && nextBlock.x == 1))
						renderer.sprite = bodyTR;
				}
			}
		}
	}

	private void UpdateHeadGraphics()
	{
		Vector2 headRelation = body[1].position - body[0].position;
		SpriteRenderer snakeHead = body[0].GetComponent<SpriteRenderer>();
		
		if (headRelation == Vector2.right) snakeHead.sprite = headLeft;
		else if (headRelation == Vector2.left) snakeHead.sprite = headRight;
		else if (headRelation == Vector2.up) snakeHead.sprite = headDown;
		else if (headRelation == Vector2.down) snakeHead.sprite = headUp;
	}

	private void UpdateTailGraphics()
	{
		//  body[^2] означет второй жлемент с конца, ^ - символ для обозначения индекса с обратной стороны
		Vector2 tailRelation = body[^2].position - body[^1].position;
		SpriteRenderer snakeTail = body[^1].GetComponent<SpriteRenderer>();
		
		if (tailRelation == Vector2.right) snakeTail.sprite = tailLeft;		
		else if (tailRelation == Vector2.left) snakeTail.sprite = tailRight;
		else if (tailRelation == Vector2.up) snakeTail.sprite = tailDown;
		else if (tailRelation == Vector2.down) snakeTail.sprite = tailUp;
	}

	private Sprite GetHeadSprite()
	{
		Vector2 headRelation = body[1].position - body[0].position;
		if (headRelation == Vector2.right) return headLeft;
		if (headRelation == Vector2.left) return headRight;
		if (headRelation == Vector2.up) return headDown;
		return headUp;
	}

	private Sprite GetTailSprite()
	{
		Vector2 tailRelation = body[^2].position - body[^1].position;
		if (tailRelation == Vector2.right) return tailLeft;
		if (tailRelation == Vector2.left) return tailRight;
		if (tailRelation == Vector2.up) return tailDown;
		return tailUp;
	}
}


