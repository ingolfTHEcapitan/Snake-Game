using UnityEngine;

public class Food : MonoBehaviour
{
	private GridBounds _gridManager;
	
	public static Food Instance {get; set;}
	
	void Start()
	{
		_gridManager = FindAnyObjectByType<GridBounds>();
	}
	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		
		GameEvents.SnakeDied.AddListener(()=>Destroy(gameObject));
		GameEvents.Food≈aten.AddListener(SetRandomPosition);
	}
	
	private void SetRandomPosition()
	{
		transform.position = _gridManager.GetRandomPosition();
	}
}



   
