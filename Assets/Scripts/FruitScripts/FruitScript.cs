using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitScript : MonoBehaviour
{   
    private SpawnManager spawnManager;

    private int fruitID;
    [SerializeField] private string fruitName;

    private string[] fruitOrder = {"Blueberry", "Cherry", "Grape", "Strawberry", "Kiwi", "Lemon", "Apple", "Peach", "Orange", "Mango", "Pear", "Watermelon"};

    void Start() {
        spawnManager = FindObjectOfType<SpawnManager>();
        fruitID = Array.IndexOf(fruitOrder, fruitName);
        Debug.Log(fruitID);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == gameObject.tag) {
            if (string.Compare(collision.gameObject.name, gameObject.name) > 0) {
                spawnManager.SpawnNextObject(fruitID, transform.position);
                // Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

}
