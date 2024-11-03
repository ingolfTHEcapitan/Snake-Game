using UnityEngine;

public class GridBounds : MonoBehaviour
{
	private BoxCollider2D _gridArea;
  
	private void Awake()
	{
		_gridArea = GetComponent<BoxCollider2D>();
	}

	public Vector3 GetRandomPosition()
	{
		Bounds gridBounds = _gridArea.bounds;
		
		int xRandomPosition = Mathf.RoundToInt(Random.Range(gridBounds.min.x, gridBounds.max.x));
		int yRandomPosition = Mathf.RoundToInt(Random.Range(gridBounds.min.y, gridBounds.max.y));
		
		Vector2 RandomPosition = new Vector3(xRandomPosition, yRandomPosition);
		return RandomPosition;
	}
}