using UnityEngine;

namespace SnakeGame
{
	public class GridBounds : MonoBehaviour
	{
		private BoxCollider2D _gridArea;
  
		private void Awake()
		{
			_gridArea = GetComponent<BoxCollider2D>();
		}

		public Vector2 GetRandomPosition()
		{
			Bounds gridBounds = _gridArea.bounds;
		
			int xRandomPosition = Mathf.RoundToInt(Random.Range(gridBounds.min.x, gridBounds.max.x));
			int yRandomPosition = Mathf.RoundToInt(Random.Range(gridBounds.min.y, gridBounds.max.y));
		
			Vector2 randomPosition = new Vector2(xRandomPosition, yRandomPosition);
			return randomPosition;
		}
	}
}