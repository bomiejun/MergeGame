using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    private int currentSpawn = 0;

    public int incSpawnNum() {
        currentSpawn++;
        return currentSpawn;
    }

    public void SpawnNextObject(int fruitID, Vector3 position1, Vector3 position2) {
        if (fruitID >= fruitPrefabs.Length - 1) {
            Debug.Log("Game Won!"); // do something for end game
        } else {
            Vector3 midpos = new Vector3((position1.x + position2.x)/2, (position1.y + position2.y)/2, (position1.z + position2.z)/2);
            Debug.Log(midpos);
            Instantiate(fruitPrefabs[fruitID + 1], midpos, Quaternion.identity);
        }
    }
}
