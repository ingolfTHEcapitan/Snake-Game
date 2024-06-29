using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    private GridManager gridManager;

    void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            transform.position = gridManager.RandomizePosition();    
        }
    }
}



   
