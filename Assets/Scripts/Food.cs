using UnityEngine;

public class Food : MonoBehaviour
{
	public static Food instanсe = null;
	
	private GridManager gridManager;

	void Start() => gridManager = FindAnyObjectByType<GridManager>();
	
	void Awake()
	{
		//Проверьяем, существует ли уже экземпляр Food
		if (instanсe == null)
			// Если нет делаем текщий экземпляр основным
			instanсe = this;
		else if (instanсe != this) // Если существует
			Destroy(gameObject); // Удаляем, реализирует принцип Синглтон, точто что экземпляр класса может быть только один
		DontDestroyOnLoad(gameObject);
		
		EventManager.snakeDied.AddListener(()=>Destroy(gameObject));
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Obstacle")
			transform.position = gridManager.RandomizePosition();
	}
}



   
