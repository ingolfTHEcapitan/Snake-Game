using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform ApplePrefab;
	private GridManager _gridManager;

	
	void Awake() => EventManager.SnakeDiedEvent.AddListener(RandomSpawn);
	
	void Start()
	{
		_gridManager = FindAnyObjectByType<GridManager>();
		RandomSpawn();
	}
	public IEnumerator SpawnRutine()
	{
		yield return new WaitForSeconds(Random.Range(2, 5));

		Transform food = Instantiate(ApplePrefab);
		food.transform.position = _gridManager.GetRandomPosition();
	}
	
	private void RandomSpawn()
	{
		StartCoroutine(SpawnRutine());
	}

}
