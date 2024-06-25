using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 currentDirection; // ������� ����������� ��������
    private List<Transform> segments = new List<Transform>(); // ������ ��������� ������
    private List<Vector2> segmentDirections = new List<Vector2>(); // ������ ����������� ���������


    public Transform segmentPrefab;
    public Transform tailPrefab; // ��������� ��������� ���� ��� ���������� �������public Transform cornerPrefab; // ������ �������� ��������

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

    // �������� ������ � ����������� �� �����������
    private void FixedUpdate()
    {
        // ��������� ������� ����������� ��������� ����� ����������� �������
        segmentDirections.Insert(0, direction);

        // ������� ������ ����������� ������, ����� ��������� ������������ ����� ������� ��������� � �����������
        if (segmentDirections.Count > segments.Count)
        {
            segmentDirections.RemoveAt(segmentDirections.Count - 1);
        }

        // ���������� �� ���� ��������� � ������ � �����
        for (int i = segments.Count - 1; i > 0; i--)
        {
            // ������ ����������� ��������� �� ���������������
            segments[i].position = segments[i - 1].position;
            segments[i].rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[i])));
        }

        // ��������� ������� � ���� �������� ������
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0.0f
        );
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(direction)));

        // ��������� ������� ����������� ��������
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
        // ���������, ���� �� ��� ��������� �������
        if (segments.Count > 1)
        {
            // �������� ������� ��������� ������� �� ������� �������
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;
            newSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
            Destroy(segments[segments.Count - 1].gameObject);
            segments[segments.Count - 1] = newSegment;
        }

        // ��������� ����� ��������� �������
        Transform tailSegment = Instantiate(tailPrefab);
        tailSegment.position = segments[segments.Count - 1].position;
        tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(segmentDirections[segments.Count - 1])));
        segments.Add(tailSegment);

        // ��������� ����� ����������� ��� ������ ��������
        segmentDirections.Add(segmentDirections[segments.Count - 2]);
    }


    private void ResetGame()
    {
        // ������� ��� ��������, ����� ������
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        // ������� ������ ��������� � �����������
        segments.Clear();
        segmentDirections.Clear();

        // ��������� ������ �������
        segments.Add(transform);
        segmentDirections.Add(Vector2.right);

        // ��������� ��������� �������� ������, ����� ���������� ���������� ��������
        for (int i = 1; i < initialSize - 1; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            segmentDirections.Add(Vector2.right);
        }

        // ��������� ��������� �������
        if (initialSize > 1)
        {
            Transform tailSegment = Instantiate(tailPrefab);
            tailSegment.position = segments[segments.Count - 1].position;
            tailSegment.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromDirection(Vector2.right)));
            segments.Add(tailSegment);
            segmentDirections.Add(Vector2.right);
        }

        // ���������� ������� ������
        transform.position = Vector3.right;

        // ������������� ��������� ����������� ��������
        direction = Vector3.right;

        // ������������� ������� ����������� ��������
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
