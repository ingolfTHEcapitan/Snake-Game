using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 currentDirection; // Текущее направление движения
    private List<Transform> segments = new List<Transform>(); // Список сегментов змейки

    private float minSegmentScale = 0.40f;  // Минимальный размер сегмента
    private float segmentScaleStep = 0.025f; // Шаг уменьшения размера

    public Transform segmentPrefab;
    
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
        currentDirection = direction; // Устанвливаем текущее напрвление движения
        
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

        // Проходимся по всем сегментам в списке с конца
        for (int i = segments.Count -1; i > 0; i--)
        {
            // Меняем очередность сегементов на противоположную
            // Таким образом мы гарантирует что каждый сегемент следует за тем который находится перед ним
            segments[i].position = segments[i - 1].position;
            SegmentsScale(segments, i);
        }

        // Последним сегментом будет головная часть
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x, 
            Mathf.Round(transform.position.y) + direction.y, 
            0.0f 
        );


        // Обновляем текущее направление движения
        currentDirection = direction;
    }

    private void SegmentsScale(List <Transform> segments, int index)
    {
        // Уменьшаем размер сегмента с учетом минимального размера
        Vector3 newScale = segments[index - 1].localScale - new Vector3(segmentScaleStep, segmentScaleStep, 0);
        newScale.x = Mathf.Max(newScale.x, minSegmentScale);
        newScale.y = Mathf.Max(newScale.y, minSegmentScale);
        segments[index].localScale = newScale;
    }

    private void GrowSnake()
    {
        Transform segment = Instantiate(segmentPrefab);

        // Положение сегмента равно положению последнего элемента в спсике сегментов
        segment.position = segments[segments.Count - 1].position;

        segments.Add (segment);
    }

    private void ResetGame()
    {
        // Проходимя по всем сегментам кроме головы
        for (int i = 1; i < segments.Count; i++)
        {
            // Удаляем их
            Destroy(segments[i].gameObject);
        }
        // Очищаем список сегментов
        segments.Clear();

        // добавляем голову обратно
        segments.Add(transform);

        // Добавляем начальные сегменты змейки
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            SegmentsScale(segments, i);
        }

        // сбрасываем позицию головы
        transform.position = Vector3.zero;

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
