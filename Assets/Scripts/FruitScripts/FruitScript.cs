using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitScript : MonoBehaviour
{   
    private SpawnManager spawnManager;

    public int fruitID;
    public string fruitName;

    public int spawnNum;

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

}
