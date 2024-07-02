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
        // Получаем границы сетки для спавня яблок
        Bounds bounds = gridArea.bounds;

        // Выбираем случвайную позицию в пределах границ сетки
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        // Присваниваем объекту эту позицию
        return new Vector3(x, y, 0);
    }
}