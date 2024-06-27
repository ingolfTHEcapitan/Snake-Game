using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 currentDirection; // ������� ����������� ��������
    private Vector2 previousDirection = Vector2.zero;
    private List<Transform> segments = new List<Transform>(); // ������ ��������� ������
    private List<Vector2> segmentDirections = new List<Vector2>(); // ������ ����������� ���������
    private List<bool> isCornerSegment = new List<bool>(); // ����� ��� ����������� ������� ���������

    public Transform segmentPrefab;
    public Transform tailPrefab;
    public Transform cornerUpRightPrefab; // ������ �������� ��������: �����-������
    public Transform cornerRightDownPrefab; // ������ �������� ��������: ������-����
    public Transform cornerDownLeftPrefab; // ������ �������� ��������: ����-�����
    public Transform cornerLeftUpPrefab; // ������ �������� ��������: �����-�����

    

    public float fixedTimestep = 0.06f;
    public int initialSize = 6; // ��������� ������ ������



    private void Awake()
    {
        // ������������� fixedTimestep
        Time.fixedDeltaTime = fixedTimestep;
    }

    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        // ����������� �������� ������ �� ������ �����
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
            SoundManager.instan�e.PlaySound(SoundManager.instan�e.efxSource2, SoundManager.instan�e.moveSound);
            previousDirection = direction; // ��������� ���������� �����������
        }
    }

    // �������� ������ � ����������� �� �����������
    private void FixedUpdate()
    {
        // ��������� ������� ����������� ��������� ����� ����������� �������
        segmentDirections.Insert(0, direction);

        // ���������� �� ���� ��������� � ������ � �����
        for (int i = segments.Count - 1; i > 0; i--)
        {

            // ��������� ������� ��������
            segments[i].position = segments[i - 1].position;

            

            // ���������� ���������� � ������� ����������� ���������
            Vector2 previousDirection = segmentDirections[i];
            Vector2 currentDirection = segmentDirections[i - 1];
            
            segments[i].rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection2( previousDirection, currentDirection)));

            // ��������� ��� ��������� ���������� ����������� ��� ������� ���������
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


                // ���� ������� ��� ������� � ������ �������� ����, �������� ������� �� segmentPrefab,
                // �� ���� ������� ��� ���������, �������� �� tailPrefab
                if (isCornerSegment[i])
                {
                    Destroy(segments[i].gameObject);
                    Transform newSegment;
                    if (i == segments.Count - 1) // ���������, �������� �� ������� ���������
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
                    isCornerSegment[i] = false; // �������� ������� ��� �� �������
                }
            }

            
        }

        // ��������� �������
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
        // ��������� ���� �������� ������
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(direction)));

        // ��������� ������� ����������� ��������
        currentDirection = direction;
        
    }

    private void ReplaceWithCornerPrefab(int index, Transform cornerPrefab)
    {
        // ���� ������� ��� �� ��� ������� �� �������, �������� �� ��������������� cornerPrefab
        if (!isCornerSegment[index])
        {
            Destroy(segments[index].gameObject);
            Transform cornerSegment = Instantiate(cornerPrefab);
            cornerSegment.position = segments[index].position;
            segments[index] = cornerSegment;
            isCornerSegment[index] = true; // �������� ������� ��� �������
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
        // ���������, ���� �� ��� ��������� �������
        if (segments.Count > 1)
        {
            // �������� ������� ��������� ������� �� ������� �������
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;
            newSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
            Destroy(segments[segments.Count - 1].gameObject);
            segments[segments.Count - 1] = newSegment;
            isCornerSegment[segments.Count - 1] = false; // ����� ������� �� �������� �������
        }

        // ��������� ����� ��������� �������
        Transform tailSegment = Instantiate(tailPrefab);
        tailSegment.position = segments[segments.Count - 1].position;
        tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
        segments.Add(tailSegment);
        segmentDirections.Add(segmentDirections[segments.Count - 2]);
        isCornerSegment.Add(false); // ����� ������� �� �������� �������
    }

    private void ResetGame()
    {
        // ������� ��� ��������, ����� ������
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // ������� ������
        segments.Clear();
        segmentDirections.Clear();
        isCornerSegment.Clear();

        // ��������� ������ �������
        segments.Add(transform);
        segmentDirections.Add(Vector2.zero);
        isCornerSegment.Add(false); // ������ �� �������� �������

        // ��������� ��������� �������� ������, ����� ���������� ���������� ��������
        for (int i = 1; i < initialSize - 1; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            segmentDirections.Add(Vector2.zero);
            isCornerSegment.Add(false); // ��������� �������� �� �������� ��������
        }

        // ��������� ��������� �������
        if (initialSize > 1)
        {
            Transform tailSegment = Instantiate(tailPrefab);
            tailSegment.position = new Vector3(-1, 0);
            tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(Vector2.right)));
            segments.Add(tailSegment);
            segmentDirections.Add(Vector2.right);
            isCornerSegment.Add(false); // ��������� ������� �� �������� �������
        }

        // ���������� ������� ������
        transform.position = new Vector3(1, 0);

        // ������������� ��������� ����������� ��������
        direction = Vector3.right;

        previousDirection = Vector2.zero;
        CheckDirectionChange();

        // ������������� ������� ����������� ��������
        currentDirection = direction;

        Console.WriteLine("wqd");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            GrowSnake();
            SoundManager.instan�e.PlaySound(SoundManager.instan�e.efxSource1, SoundManager.instan�e.eatFoodSound);
        }
        else if (collision.tag == "Obstacle")
        {
            ResetGame();
            SoundManager.instan�e.PlaySound(SoundManager.instan�e.efxSource1, SoundManager.instan�e.gameOverSound);

        }
    }
}
