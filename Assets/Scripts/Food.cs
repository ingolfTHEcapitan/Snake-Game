using UnityEngine;

public class Food : MonoBehaviour
{
	private static Food s_instan�e = null;
	
	private GridManager _gridManager;

	void Start()
	{
		_gridManager = FindAnyObjectByType<GridManager>();
	}
	
	void Awake()
	{
		//����������, ���������� �� ��� ��������� Food
		if (s_instan�e == null)
			// ���� ��� ������ ������ ��������� ��������
			s_instan�e = this;
		else if (s_instan�e != this) // ���� ����������
			Destroy(gameObject); // �������, ����������� ������� ��������, ����� ��� ��������� ������ ����� ���� ������ ����
		DontDestroyOnLoad(gameObject);
		
		EventManager.SnakeDiedEvent.AddListener(()=>Destroy(gameObject));
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Obstacle"))
			transform.position = _gridManager.GetRandomPosition();
	}
}



   
