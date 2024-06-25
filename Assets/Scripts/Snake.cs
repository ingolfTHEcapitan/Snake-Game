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
    private Vector2 currentDirection; // ������� ����������� ��������
    private List<Transform> segments = new List<Transform>(); // ������ ��������� ������

    private float minSegmentScale = 0.40f;  // ����������� ������ ��������
    private float segmentScaleStep = 0.025f; // ��� ���������� �������

    public Transform segmentPrefab;
    
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
        currentDirection = direction; // ������������ ������� ���������� ��������
        
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

        // ���������� �� ���� ��������� � ������ � �����
        for (int i = segments.Count -1; i > 0; i--)
        {
            // ������ ����������� ���������� �� ���������������
            // ����� ������� �� ����������� ��� ������ �������� ������� �� ��� ������� ��������� ����� ���
            segments[i].position = segments[i - 1].position;
            SegmentsScale(segments, i);
        }

        // ��������� ��������� ����� �������� �����
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x, 
            Mathf.Round(transform.position.y) + direction.y, 
            0.0f 
        );


        // ��������� ������� ����������� ��������
        currentDirection = direction;
    }

    private void SegmentsScale(List <Transform> segments, int index)
    {
        // ��������� ������ �������� � ������ ������������ �������
        Vector3 newScale = segments[index - 1].localScale - new Vector3(segmentScaleStep, segmentScaleStep, 0);
        newScale.x = Mathf.Max(newScale.x, minSegmentScale);
        newScale.y = Mathf.Max(newScale.y, minSegmentScale);
        segments[index].localScale = newScale;
    }

    private void GrowSnake()
    {
        Transform segment = Instantiate(segmentPrefab);

        // ��������� �������� ����� ��������� ���������� �������� � ������ ���������
        segment.position = segments[segments.Count - 1].position;

        segments.Add (segment);
    }

    private void ResetGame()
    {
        // ��������� �� ���� ��������� ����� ������
        for (int i = 1; i < segments.Count; i++)
        {
            // ������� ��
            Destroy(segments[i].gameObject);
        }
        // ������� ������ ���������
        segments.Clear();

        // ��������� ������ �������
        segments.Add(transform);

        // ��������� ��������� �������� ������
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
            SegmentsScale(segments, i);
        }

        // ���������� ������� ������
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
