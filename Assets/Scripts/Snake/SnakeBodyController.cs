using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
	[SerializeField] private Transform _segmentPrefab;
	[SerializeField] private Vector2[] _initialPositions = new Vector2[] { new (0, 0), new (-1, 0), new (-2, 0) };
	public List<Transform> Body {get; private set;} = new List<Transform>();

	private void Awake()
	{
		GameEvents.SnakeDied.AddListener(CreateSnake);
	}

	private void Start()
	{
		CreateSnake();
	}

	private void CreateSnake()
	{
		ClearSnake();
		InitializeSegments();
		GameEvents.OnUpdateGraphics();
	}

	private void ClearSnake()
	{
		foreach (var segment in Body)
			Destroy(segment.gameObject);

		Body.Clear();
	}
	
	private void InitializeSegments()
	{
		for (int i = 0; i < _initialPositions.Length; i++)
		{
			bool isHeadSegment = i == 0;
			
			Vector2 position = new Vector3(_initialPositions[i].x, _initialPositions[i].y);
			Transform segment = Instantiate(_segmentPrefab, position, Quaternion.identity);
			Body.Add(segment);

			if (isHeadSegment)
				segment.gameObject.AddComponent<SnakeHeadCollision>();
			else
				segment.gameObject.AddComponent<SnakeBodyCollision>();
		}
	}
	
	public void UpdateSegmentPositions(Vector2 direction)
	{
		List<Vector2> bodyCopy = new List<Vector2>();
		
		foreach (var segment in Body)
		{
			bodyCopy.Add(segment.position);
		}

		bodyCopy.Insert(0, bodyCopy[0] + direction);
		bodyCopy.RemoveAt(bodyCopy.Count - 1);

		for (int i = 0; i < Body.Count; i++)
		{
			Body[i].position = new Vector2(bodyCopy[i].x, bodyCopy[i].y);
		}
	}
	
	public void AddSegment()
	{
		Vector2 position = Body[^1].position;
		Transform newSegment = Instantiate(_segmentPrefab, position, Quaternion.identity);
		newSegment.gameObject.AddComponent<SnakeBodyCollision>();
		
		Body.Add(newSegment);
	}
}
