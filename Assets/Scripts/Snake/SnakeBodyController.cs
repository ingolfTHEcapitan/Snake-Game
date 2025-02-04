using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Snake
{
	public class SnakeBodyController : MonoBehaviour
	{
		[SerializeField] private Transform _segmentPrefab;
		[SerializeField] private Vector2[] _initialPositions = { new (0, 0), new (-1, 0), new (-2, 0) };
		public List<Transform> Segments {get;} = new();

		private void Awake()
		{
			EventBus.SnakeDied.AddListener(CreateSnake);
		}

		private void Start()
		{
			CreateSnake();
		}
		
		public void AddSegment()
		{
			Vector3 position = Segments[^1].position;
			Transform newSegment = Instantiate(_segmentPrefab, position, Quaternion.identity);
			newSegment.gameObject.AddComponent<SnakeBodyCollision>();
		
			Segments.Add(newSegment);
		}
		
		public void UpdateSegmentPositions(Vector2 direction)
		{
			List<Vector2> segmentsCopy = new List<Vector2>();
		
			foreach (var segment in Segments)
			{
				segmentsCopy.Add(segment.position);
			}

			segmentsCopy.Insert(0, segmentsCopy[0] + direction);
			segmentsCopy.RemoveAt(segmentsCopy.Count - 1);

			for (int i = 0; i < Segments.Count; i++)
			{
				Segments[i].position = new Vector2(segmentsCopy[i].x, segmentsCopy[i].y);
			}
		}
		
		private void CreateSnake()
		{
			ClearSnake();
			InitializeSegments();
			EventBus.OnGraphicsUpdateRequested();
		}

		private void ClearSnake()
		{
			foreach (var segment in Segments)
				Destroy(segment.gameObject);

			Segments.Clear();
		}
	
		private void InitializeSegments()
		{
			for (int i = 0; i < _initialPositions.Length; i++)
			{
				bool isHeadSegment = i == 0;
			
				Vector2 position = new Vector2(_initialPositions[i].x, _initialPositions[i].y);
				Transform segment = Instantiate(_segmentPrefab, position, Quaternion.identity);
				Segments.Add(segment);

				if (isHeadSegment)
					segment.gameObject.AddComponent<SnakeHeadCollision>();
				else
					segment.gameObject.AddComponent<SnakeBodyCollision>();
			}
		}
	
	}
}
