using UnityEngine;

public class GridManager : MonoBehaviour
{
    private BoxCollider2D gridArea;
  
    void Start()
    {
        gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
    }

    public Vector3 RandomizePosition()
    {
        // Получаем границы сетки для спавня яблок
        Bounds bounds = gridArea.bounds;

        // Выбираем случвайную позицию в пределах границ сетки
        int x = (int)Random.Range(bounds.min.x, bounds.max.x);
        int y = (int)Random.Range(bounds.min.y, bounds.max.y);

        // Присваниваем объекту эту позицию
        return new Vector3(x, y, 0);
    }
}