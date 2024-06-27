using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    private BoxCollider2D gridArea;
  
    void Start()
    {
        gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
    }

    private void RandomizePosition()
    {
        // Получаем границы сетки для спавня яблок
        Bounds bounds = gridArea.bounds;

        // Выбираем случвайную позицию в пределах границ сетки
        int x = (int)Random.Range(bounds.min.x, bounds.max.x);
        int y = (int)Random.Range(bounds.min.y, bounds.max.y);

        // Присваниваем объекту эту позицию
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            RandomizePosition();
            
        }
    }

}



   
