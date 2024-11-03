using UnityEngine;

public class SnakeGraphics : MonoBehaviour
{
	[SerializeField] private Sprite _headUp, _headDown, _headLeft, _headRight;
	[SerializeField] private Sprite _tailUp, _tailDown, _tailLeft, _tailRight;
	[SerializeField] private Sprite _bodyVertical, _bodyHorizontal, _bodyTopLeft, _bodyTopRight, _bodyBottomLeft, _bodyBottomRight;
	
	private SnakeBodyController _snakeBody;

	private void Awake() 
	{
		_snakeBody = GetComponent<SnakeBodyController>();
		GameEvents.UpdateGraphics.AddListener(UpdateGraphics);
	}
	
	private void UpdateGraphics()
	{
		UpdateHeadGraphics(); 
		UpdateTailGraphics();
		UpdateBodyGraphics();
	}

	public void UpdateBodyGraphics()
	{
		for (int index = 1; index < _snakeBody.Body.Count - 1; index++)
		{
			Transform segment = _snakeBody.Body[index];
			
			SpriteRenderer renderer = segment.GetComponent<SpriteRenderer>();

			Vector3 segmentPosition = segment.position;
			Vector2 previousSegmentPosition = _snakeBody.Body[index + 1].position - segmentPosition;
			Vector2 nextSegmentPosition = _snakeBody.Body[index - 1].position - segmentPosition;

			if (previousSegmentPosition.x == nextSegmentPosition.x)
				renderer.sprite = _bodyVertical;
				
			else if (previousSegmentPosition.y == nextSegmentPosition.y)
				renderer.sprite = _bodyHorizontal;

			else
			{
				renderer.sprite = DetermineCornerSprite(previousSegmentPosition, nextSegmentPosition);
			}
		}
	}

	private Sprite DetermineCornerSprite(Vector2 previousSegmentPosition, Vector2 nextSegmentPosition)
	{
		if ((previousSegmentPosition.x == -1 && nextSegmentPosition.y == -1) || (previousSegmentPosition.y == -1 && nextSegmentPosition.x == -1))
			return _bodyBottomLeft;
		if ((previousSegmentPosition.x == -1 && nextSegmentPosition.y == 1) || (previousSegmentPosition.y == 1 && nextSegmentPosition.x == -1))
			return _bodyTopLeft;
		if ((previousSegmentPosition.x == 1 && nextSegmentPosition.y == -1) || (previousSegmentPosition.y == -1 && nextSegmentPosition.x == 1))
			return _bodyBottomRight;
		if ((previousSegmentPosition.x == 1 && nextSegmentPosition.y == 1) || (previousSegmentPosition.y == 1 && nextSegmentPosition.x == 1))
			return _bodyTopRight;

		return null;
	}

	public void UpdateHeadGraphics()
	{
		Vector2 headRelation = _snakeBody.Body[1].position - _snakeBody.Body[0].position;
		SpriteRenderer snakeHead = _snakeBody.Body[0].GetComponent<SpriteRenderer>();
		
		if (headRelation == Vector2.right) snakeHead.sprite = _headLeft;
		else if (headRelation == Vector2.left) snakeHead.sprite = _headRight;
		else if (headRelation == Vector2.up) snakeHead.sprite = _headDown;
		else if (headRelation == Vector2.down) snakeHead.sprite = _headUp;
	}

	public void UpdateTailGraphics()
	{
		Vector2 tailRelation = _snakeBody.Body[^2].position - _snakeBody.Body[^1].position;
		SpriteRenderer snakeTail = _snakeBody.Body[^1].GetComponent<SpriteRenderer>();
		
		if (tailRelation == Vector2.right) snakeTail.sprite = _tailLeft;		
		else if (tailRelation == Vector2.left) snakeTail.sprite = _tailRight;
		else if (tailRelation == Vector2.up) snakeTail.sprite = _tailDown;
		else if (tailRelation == Vector2.down) snakeTail.sprite = _tailUp;
	}

	public Sprite GetHeadSprite()
	{
		Vector2 headRelation = _snakeBody.Body[1].position - _snakeBody.Body[0].position;
		
		if (headRelation == Vector2.right) return _headLeft;
		if (headRelation == Vector2.left) return _headRight;
		if (headRelation == Vector2.up) return _headDown;
		
		return _headUp;
	}

	public Sprite GetTailSprite()
	{
		Vector2 tailRelation = _snakeBody.Body[^2].position - _snakeBody.Body[^1].position;
		
		if (tailRelation == Vector2.right) return _tailLeft;
		if (tailRelation == Vector2.left) return _tailRight;
		if (tailRelation == Vector2.up) return _tailDown;
		
		return _tailUp;
	}
}


