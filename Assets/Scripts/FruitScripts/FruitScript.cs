using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class FruitScript : MonoBehaviour
{   
    private SpawnManager spawnManager;

    public int fruitID;
    public string fruitName;

    public int spawnNum;

    private float topmostY;
    public bool beenRel = false;

    private string[] fruitOrder = {"Blueberry", "Cherry", "Grape", "Strawberry", "Kiwi", "Lemon", "Apple", "Peach", "Orange", "Mango", "Pear", "Watermelon"};

    void Start() {
        spawnManager = FindObjectOfType<SpawnManager>();
        fruitID = Array.IndexOf(fruitOrder, fruitName);
        Debug.Log(fruitID);

        spawnNum = spawnManager.incSpawnNum();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        FruitScript fruit = collision.gameObject.GetComponent<FruitScript>();

        if (!(fruit == null) && fruit.fruitID == fruitID) {
            // Debug.Log(fruit.fruitID);
            // Debug.Log(fruitID);
            if (fruit.spawnNum > spawnNum) {
                Debug.Log(fruitID);
                Debug.Log("here");
                spawnManager.SpawnNextObject(fruitID, transform.position, collision.transform.position);
                // Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void Update() {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();

        topmostY = float.MinValue;

        foreach (Vector2 localPoint in poly.points)
        {
            // Convert local collider point to world space
            Vector2 worldPoint = transform.TransformPoint(localPoint);

            if (transform.position.y + worldPoint.y > topmostY)
            {
                topmostY = transform.position.y + worldPoint.y;
            }
        }

        if (topmostY >= 6.15 && beenRel) {
            Debug.Log(fruitName);
            Debug.Log(topmostY);
            Debug.Log(gameObject.transform.position);
            spawnManager.gameOverBoard.gameObject.SetActive(true);
            spawnManager.restartButton.gameObject.SetActive(true);

            spawnManager.gamePlay = false;
        }
    }

}
