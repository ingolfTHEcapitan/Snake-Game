using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 currentDirection; // Текущее направление движения
    private Vector2 previousDirection = Vector2.zero;
    private List<Transform> segments = new List<Transform>(); // Список сегментов змейки
    private List<Vector2> segmentDirections = new List<Vector2>(); // Список направлений сегментов
    private List<bool> isCornerSegment = new List<bool>(); // Флаги для определения угловых сегментов

    public Transform segmentPrefab;
    public Transform tailPrefab;
    public Transform cornerUpRightPrefab; // Префаб углового сегмента: вверх-вправо
    public Transform cornerRightDownPrefab; // Префаб углового сегмента: вправо-вниз
    public Transform cornerDownLeftPrefab; // Префаб углового сегмента: вниз-влево
    public Transform cornerLeftUpPrefab; // Префаб углового сегмента: влево-вверх

    

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
            CheckDirectionChange();

        }
        else if (Input.GetKeyDown(KeyCode.S) && currentDirection != Vector2.up)
        {
            direction = Vector2.down;
            CheckDirectionChange();
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentDirection != Vector2.right)
        {
            direction = Vector2.left;
            CheckDirectionChange();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentDirection != Vector2.left)
        {
            direction = Vector2.right;
            CheckDirectionChange();

        }
    }

    private void CheckDirectionChange()
    {
        if (direction != previousDirection)
        {
            SoundManager.instanсe.PlaySound(SoundManager.instanсe.efxSource2, SoundManager.instanсe.moveSound);
            previousDirection = direction; // Обновляем предыдущее направление
        }
    }

    // Движение змейки в зависимости от направления
    private void FixedUpdate()
    {
        // Сохраняем текущие направления сегментов перед обновлением позиций
        segmentDirections.Insert(0, direction);

        // Проходимся по всем сегментам в списке с конца
        for (int i = segments.Count - 1; i > 0; i--)
        {

            // Обновляем позицию сегмента
            segments[i].position = segments[i - 1].position;

            

            // Определяем предыдущее и текущее направления сегментов
            Vector2 previousDirection = segmentDirections[i];
            Vector2 currentDirection = segmentDirections[i - 1];
            
            segments[i].rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection2( previousDirection, currentDirection)));

            // Проверяем все возможные комбинации направлений для угловых сегментов
            if ((previousDirection == Vector2.up && currentDirection == Vector2.right) ||
                (previousDirection == Vector2.left && currentDirection == Vector2.down))
            {
                ReplaceWithCornerPrefab(i, cornerUpRightPrefab);
            }
            else if ((previousDirection == Vector2.right && currentDirection == Vector2.down) ||
                     (previousDirection == Vector2.up && currentDirection == Vector2.left))
            {
                ReplaceWithCornerPrefab(i, cornerRightDownPrefab);
            }
            else if ((previousDirection == Vector2.down && currentDirection == Vector2.left) ||
                     (previousDirection == Vector2.right && currentDirection == Vector2.up))
            {
                ReplaceWithCornerPrefab(i, cornerDownLeftPrefab);
            }
            else if ((previousDirection == Vector2.left && currentDirection == Vector2.up) ||
                     (previousDirection == Vector2.down && currentDirection == Vector2.right))
            {
                ReplaceWithCornerPrefab(i, cornerLeftUpPrefab);
            }

            else
            {
                segments[i].rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[i])));


                // Если сегмент был угловым и сейчас перестал быть, заменяем обратно на segmentPrefab,
                // но если сегмент был последним, заменяем на tailPrefab
                if (isCornerSegment[i])
                {
                    Destroy(segments[i].gameObject);
                    Transform newSegment;
                    if (i == segments.Count - 1) // Проверяем, является ли сегмент последним
                    {
                        newSegment = Instantiate(tailPrefab);
                    }
                    else
                    {
                        newSegment = Instantiate(segmentPrefab);
                    }
                    newSegment.position = segments[i].position;
                    newSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(currentDirection)));
                    segments[i] = newSegment;
                    isCornerSegment[i] = false; // Помечаем сегмент как не угловой
                }
            }

            
        }

        // Обновляем позицию
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
        // Обновляем угол поворота головы
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(direction)));

        // Обновляем текущее направление движения
        currentDirection = direction;
        
    }

    private void ReplaceWithCornerPrefab(int index, Transform cornerPrefab)
    {
        // Если сегмент еще не был заменен на угловой, заменяем на соответствующий cornerPrefab
        if (!isCornerSegment[index])
        {
            Destroy(segments[index].gameObject);
            Transform cornerSegment = Instantiate(cornerPrefab);
            cornerSegment.position = segments[index].position;
            segments[index] = cornerSegment;
            isCornerSegment[index] = true; // Помечаем сегмент как угловой
        }
    }

    private float GetAngleFromDirection2( Vector2 previousDirection, Vector2 currentDirection)
    {
        float angle = 0f;

        if ((previousDirection == Vector2.down && currentDirection == Vector2.right) ||
        (previousDirection == Vector2.up && currentDirection == Vector2.left) ||
        (previousDirection == Vector2.left && currentDirection == Vector2.down) ||
        (previousDirection == Vector2.right && currentDirection == Vector2.up))
        {
            angle = 90f;
        }
        else if ((previousDirection == Vector2.left && currentDirection == Vector2.up) ||
                (previousDirection == Vector2.up && currentDirection == Vector2.right) ||
                (previousDirection == Vector2.down && currentDirection == Vector2.left) ||
                (previousDirection == Vector2.right && currentDirection == Vector2.down))
        {
            angle = -90f;
        }


       
        return angle;
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
            isCornerSegment[segments.Count - 1] = false; // Новый сегмент не является угловым
        }

        // Добавляем новый хвостовой сегмент
        Transform tailSegment = Instantiate(tailPrefab);
        tailSegment.position = segments[segments.Count - 1].position;
        tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
        segments.Add(tailSegment);
        segmentDirections.Add(segmentDirections[segments.Count - 2]);
        isCornerSegment.Add(false); // Новый сегмент не является угловым
    }

    private void ResetGame()
    {
        // Удаляем все сегменты, кроме головы
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Очищаем списки
        segments.Clear();
        segmentDirections.Clear();
        isCornerSegment.Clear();

        // Добавляем голову обратно
        segments.Add(transform);
        segmentDirections.Add(Vector2.zero);
        isCornerSegment.Add(false); // Голова не является угловой

        // Добавляем начальные сегменты змейки, кроме последнего хвостового сегмента
        for (int i = 1; i < initialSize - 1; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            segmentDirections.Add(Vector2.zero);
            isCornerSegment.Add(false); // Начальные сегменты не являются угловыми
        }

        // Добавляем хвостовой сегмент
        if (initialSize > 1)
        {
            Transform tailSegment = Instantiate(tailPrefab);
            tailSegment.position = new Vector3(-1, 0);
            tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(Vector2.right)));
            segments.Add(tailSegment);
            segmentDirections.Add(Vector2.right);
            isCornerSegment.Add(false); // Хвостовой сегмент не является угловым
        }

        // Сбрасываем позицию головы
        transform.position = new Vector3(1, 0);

        // Устанавливаем начальное направление движения
        direction = Vector3.right;

        previousDirection = Vector2.zero;
        CheckDirectionChange();

        // Устанавливаем текущее направление движения
        currentDirection = direction;

        Console.WriteLine("wqd");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            GrowSnake();
            SoundManager.instanсe.PlaySound(SoundManager.instanсe.efxSource1, SoundManager.instanсe.eatFoodSound);
        }
        else if (collision.tag == "Obstacle")
        {
            ResetGame();
            SoundManager.instanсe.PlaySound(SoundManager.instanсe.efxSource1, SoundManager.instanсe.gameOverSound);

        }
    }
}
