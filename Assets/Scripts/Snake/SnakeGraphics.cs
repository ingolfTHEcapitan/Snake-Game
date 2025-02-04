using UnityEngine;
using UnityEngine.Serialization;

namespace SnakeGame.Snake
{
	public class SnakeGraphics : MonoBehaviour
	{
		[SerializeField] private Sprite _headUp, _headDown, _headLeft, _headRight;
		[SerializeField] private Sprite _tailUp, _tailDown, _tailLeft, _tailRight;
		[SerializeField] private Sprite _bodyVertical, _bodyHorizontal, _bodyTopLeft, _bodyTopRight, _bodyBottomLeft, _bodyBottomRight;
		
		private SnakeBodyController _body;

		private void Awake() 
		{
			_body = GetComponent<SnakeBodyController>();
			EventBus.GraphicsUpdateRequested.AddListener(UpdateGraphics);
		}
	
		private void UpdateGraphics()
		{
			UpdateHeadGraphics(); 
			UpdateTailGraphics();
			UpdateBodyGraphics();
		}

		private void UpdateBodyGraphics()
		{
			for (int index = 1; index < _body.Segments.Count - 1; index++)
			{
				Transform segment = _body.Segments[index];
			
				SpriteRenderer spriteRenderer = segment.GetComponent<SpriteRenderer>();

				Vector3 segmentPosition = segment.position;
				Vector3 previousSegmentPosition = _body.Segments[index + 1].position - segmentPosition;
				Vector3 nextSegmentPosition = _body.Segments[index - 1].position - segmentPosition;

				if (previousSegmentPosition.x == nextSegmentPosition.x)
					spriteRenderer.sprite = _bodyVertical;
				
				else if (previousSegmentPosition.y == nextSegmentPosition.y)
					spriteRenderer.sprite = _bodyHorizontal;
				
				else
					spriteRenderer.sprite = DetermineCornerSprite(previousSegmentPosition, nextSegmentPosition);
			}
		}

		private Sprite DetermineCornerSprite(Vector3 previousSegmentPosition, Vector3 nextSegmentPosition)
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

		private void UpdateHeadGraphics()
		{
			Vector2 headRelation = _body.Segments[1].position - _body.Segments[0].position;
			SpriteRenderer snakeHead = _body.Segments[0].GetComponent<SpriteRenderer>();
		
			if (headRelation == Vector2.right) snakeHead.sprite = _headLeft;
			else if (headRelation == Vector2.left) snakeHead.sprite = _headRight;
			else if (headRelation == Vector2.up) snakeHead.sprite = _headDown;
			else if (headRelation == Vector2.down) snakeHead.sprite = _headUp;
		}

		private void UpdateTailGraphics()
		{
			Vector2 tailRelation = _body.Segments[^2].position - _body.Segments[^1].position;
			SpriteRenderer snakeTail = _body.Segments[^1].GetComponent<SpriteRenderer>();
		
			if (tailRelation == Vector2.right) snakeTail.sprite = _tailLeft;		
			else if (tailRelation == Vector2.left) snakeTail.sprite = _tailRight;
			else if (tailRelation == Vector2.up) snakeTail.sprite = _tailDown;
			else if (tailRelation == Vector2.down) snakeTail.sprite = _tailUp;
		}
	}
}


