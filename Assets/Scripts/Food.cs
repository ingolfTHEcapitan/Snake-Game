using UnityEngine;

public class Food : MonoBehaviour
{
	private static Food s_instanсe = null;
	
	private GridManager _gridManager;

	void Start()
	{
		_gridManager = FindAnyObjectByType<GridManager>();
	}
	
	void Awake()
	{
		//Проверьяем, существует ли уже экземпляр Food
		if (s_instanсe == null)
			// Если нет делаем текщий экземпляр основным
			s_instanсe = this;
		else if (s_instanсe != this) // Если существует
			Destroy(gameObject); // Удаляем, реализирует принцип Синглтон, точто что экземпляр класса может быть только один
		DontDestroyOnLoad(gameObject);
		
		EventManager.SnakeDiedEvent.AddListener(()=>Destroy(gameObject));
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Obstacle"))
			transform.position = _gridManager.GetRandomPosition();
	}
}



   
