using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    public void SpawnNextObject(int fruitID, Vector3 position) {
        if (fruitID >= fruitPrefabs.Length - 1) {
            Debug.Log("Game Won!"); // do something for end game
        } else {
            Instantiate(fruitPrefabs[fruitID + 1], position, Quaternion.identity);
        }
    }
}
