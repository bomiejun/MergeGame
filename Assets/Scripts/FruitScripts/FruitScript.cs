using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitScript : MonoBehaviour
{
    private string fruitName;
    private int fruitID;

    public GameObject itemPrefab; 

    private string[] fruitOrder = {"Apple", "Watermelon"};

    void Start() {
        fruitName = gameObject.tag;
        fruitID = Array.IndexOf(fruitOrder, fruitName);
        Debug.Log(fruitID);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == gameObject.tag) {
            if (string.Compare(collision.gameObject.name, gameObject.name) > 0) {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

}
