using System.Collections;
using UnityEngine;

namespace SnakeGame.Food
{
	public class FoodSpawner : MonoBehaviour
	{
		[SerializeField] private float _minSpawnTime = 2;
		[SerializeField] private float _maxSpawnTime = 5;
		[SerializeField] private Transform _foodPrefab;
	
		private GridBounds _gridManager;

		private void Awake()
		{
			_gridManager = FindAnyObjectByType<GridBounds>();
			GameEvents.SnakeDied.AddListener(RandomSpawn);
		}

		private void Start() => RandomSpawn();

		private IEnumerator SpawnRutine()
		{
			yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

			var food = Instantiate(_foodPrefab);
			food.transform.position = _gridManager.GetRandomPosition();
		}

		private void RandomSpawn() => StartCoroutine(SpawnRutine());
	}
}
