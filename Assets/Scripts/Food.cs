using UnityEngine;

public class Food : MonoBehaviour
{
	public static Food instan�e = null;
	
	private GridManager gridManager;

	void Start() => gridManager = FindAnyObjectByType<GridManager>();
	
	void Awake()
	{
		//����������, ���������� �� ��� ��������� Food
		if (instan�e == null)
			// ���� ��� ������ ������ ��������� ��������
			instan�e = this;
		else if (instan�e != this) // ���� ����������
			Destroy(gameObject); // �������, ����������� ������� ��������, ����� ��� ��������� ������ ����� ���� ������ ����
		DontDestroyOnLoad(gameObject);
		
		EventManager.snakeDied.AddListener(()=>Destroy(gameObject));
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Obstacle")
			transform.position = gridManager.RandomizePosition();
	}
}



   
