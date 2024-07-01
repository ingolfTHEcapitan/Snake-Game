using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform applePrefab;
	private GridManager gridManager;

	// Start is called before the first frame update
	
	void Awake() => EventManager.snakeDied.AddListener(RandomlySpawn);
	
	void Start()
	{
		gridManager = FindAnyObjectByType<GridManager>();
		RandomlySpawn();
	}
	public IEnumerator SpawnRutine()
	{
		yield return new WaitForSeconds(Random.Range(2, 5));

		Transform food = Instantiate(applePrefab);
		food.transform.position = gridManager.RandomizePosition();
	}
	
	private void RandomlySpawn()
	{
		StartCoroutine(SpawnRutine());
	}

}
