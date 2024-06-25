using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 currentDirection; // Текущее направление движения
    private List<Transform> segments = new List<Transform>(); // Список сегментов змейки
    private List<Vector2> segmentDirections = new List<Vector2>(); // Список направлений сегментов


    public Transform segmentPrefab;
    public Transform tailPrefab; // Добавляем публичное поле для хвостового префабаpublic Transform cornerPrefab; // Префаб углового сегмента

    public float fixedTimestep = 0.06f;
    public int initialSize = 6; // начальный размер змейки


    private void Awake()
    {
        // Устанавливаем fixedTimestep
        Time.fixedDeltaTime = fixedTimestep;
    }


    private void Start()
    {
        ResetGame();
    }


    private void Update()
    {
        // Направление движения змейки на основе ввода
        if (Input.GetKeyDown(KeyCode.W) && currentDirection != Vector2.down)
        { 
            direction = Vector2.up;
            
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentDirection != Vector2.up) 
        { 
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentDirection != Vector2.right) 
        { 
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentDirection != Vector2.left) 
        { 
            direction = Vector2.right;
        }

       
    }

    // Движение змейки в зависимости от направления
    private void FixedUpdate()
    {
        // Сохраняем текущие направления сегментов перед обновлением позиций
        segmentDirections.Insert(0, direction);

        // Удаляем старое направление головы, чтобы сохранить соответствие длины списков сегментов и направлений
        if (segmentDirections.Count > segments.Count)
        {
            segmentDirections.RemoveAt(segmentDirections.Count - 1);
        }

        // Проходимся по всем сегментам в списке с конца
        for (int i = segments.Count - 1; i > 0; i--)
        {
            // Меняем очередность сегментов на противоположную
            segments[i].position = segments[i - 1].position;
            segments[i].rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[i])));
        }

        // Обновляем позицию и угол поворота головы
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(direction)));

        // Обновляем текущее направление движения
        currentDirection = direction;
    }


    private float GetAngleFromDirection(Vector2 direction)
   {
       
        float angle = 0f;
        if (direction == Vector2.up)
        {
            angle = 90f;
        }
        else if (direction == Vector2.down)
        {
            angle = 270f;
        }
        else if (direction == Vector2.left)
        {
            angle = 180f;
        }
        else if (direction == Vector2.right)
        {
            angle = 0f;
        }
        return angle;
    }



    private void GrowSnake()
    {
        // Проверяем, есть ли уже хвостовой сегмент
        if (segments.Count > 1)
        {
            // Заменяем текущий хвостовой сегмент на обычный сегмент
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;
            newSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
            Destroy(segments[segments.Count - 1].gameObject);
            segments[segments.Count - 1] = newSegment;
        }

        // Добавляем новый хвостовой сегмент
        Transform tailSegment = Instantiate(tailPrefab);
        tailSegment.position = segments[segments.Count - 1].position;
        tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
        segments.Add(tailSegment);

        // Добавляем новое направление для нового сегмента
        segmentDirections.Add(segmentDirections[segments.Count - 2]);
    }


    private void ResetGame()
    {
        // Удаляем все сегменты, кроме головы
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        // Очищаем список сегментов и направлений
        segments.Clear();
        segmentDirections.Clear();

        // Добавляем голову обратно
        segments.Add(transform);
        segmentDirections.Add(Vector2.right);

        // Добавляем начальные сегменты змейки, кроме последнего хвостового сегмента
        for (int i = 1; i < initialSize - 1; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            segmentDirections.Add(Vector2.right);
        }

        // Добавляем хвостовой сегмент
        if (initialSize > 1)
        {
            Transform tailSegment = Instantiate(tailPrefab);
            tailSegment.position = segments[segments.Count - 1].position;
            tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(Vector2.right)));
            segments.Add(tailSegment);
            segmentDirections.Add(Vector2.right);
        }

        // Сбрасываем позицию головы
        transform.position = Vector3.right;

        // Устанавливаем начальное направление движения
        direction = Vector3.right;

        // Устанавливаем текущее направление движения
        currentDirection = direction;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            GrowSnake();
        }

        else if (collision.tag == "Obstacle")
        {
            ResetGame();
        }
    }

   
}
