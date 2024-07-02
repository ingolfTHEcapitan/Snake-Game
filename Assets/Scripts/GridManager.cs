using UnityEngine;

public class GridManager : MonoBehaviour
{
    private BoxCollider2D gridArea;
  
    void Start()
    {
        gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPosition()
    {
        // �������� ������� ����� ��� ������ �����
        Bounds bounds = gridArea.bounds;

        // �������� ���������� ������� � �������� ������ �����
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        // ������������ ������� ��� �������
        return new Vector3(x, y, 0);
    }
}