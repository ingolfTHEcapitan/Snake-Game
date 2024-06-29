using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform applePrefab;
    private GridManager gridManager;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        StartCoroutine(SpawnRutine());
    }
    public IEnumerator SpawnRutine()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));

        Transform food = Instantiate(applePrefab);
        food.transform.position = gridManager.RandomizePosition();
    }

}
