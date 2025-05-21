using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float leftmostX = float.MaxValue;
    float rightmostX = float.MinValue;

    public GameObject[] fruitPrefabs;
    private int currentSpawn = 0;

    private int biggestFruit = 0;

    private GameObject dropFruit;

    void Start() {
        newDropFruit();
    }

    void Update() {
        PolygonCollider2D poly = dropFruit.GetComponent<PolygonCollider2D>();

        leftmostX = float.MaxValue;
        rightmostX = float.MinValue;

        foreach (Vector2 localPoint in poly.points)
        {
            // Convert local collider point to world space
            Vector2 worldPoint = transform.TransformPoint(localPoint);

            if (dropFruit.transform.position.x + worldPoint.x < leftmostX)
            {
                leftmostX = dropFruit.transform.position.x + worldPoint.x;
            }
            if (dropFruit.transform.position.x + worldPoint.x > rightmostX) {
                rightmostX = dropFruit.transform.position.x + worldPoint.x;
            }
        }

        // Debug.Log(dropFruit.transform.position.x);

        // Debug.Log(leftmostX);

        if (Input.GetKeyDown(KeyCode.Space))
        {            
            dropFruit.GetComponent<Rigidbody2D>().gravityScale = 1f;
            StartCoroutine(SpawnAfterDelay());
        }
        if (leftmostX > -3.36 && Input.GetKey(KeyCode.LeftArrow))
        {
            dropFruit.transform.position += Vector3.left * 3f * Time.deltaTime;
        } else if (rightmostX < 3.36 && Input.GetKey(KeyCode.RightArrow))
        {
            dropFruit.transform.position += Vector3.right * 3f * Time.deltaTime;
        }
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        newDropFruit();
    }

    void newDropFruit() {
        Debug.Log("yoyo");
        dropFruit = Instantiate(fruitPrefabs[Random.Range(0, Mathf.Min(biggestFruit+1, 5))], new Vector3(0, 4.4f, 0), Quaternion.identity);
        dropFruit.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        dropFruit.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public int incSpawnNum() {
        currentSpawn++;
        return currentSpawn;
    }

    public void SpawnNextObject(int fruitID, Vector3 position1, Vector3 position2) {
        Debug.Log("bro wtf");
        if (fruitID >= fruitPrefabs.Length - 1) {
            Debug.Log("Game Won!"); // do something for end game
        } else {
            Vector3 midpos = new Vector3((position1.x + position2.x)/2, (position1.y + position2.y)/2, (position1.z + position2.z)/2);
            Debug.Log(midpos);
            if (fruitID + 1 > biggestFruit) {
                biggestFruit = fruitID + 1;
            }
            GameObject mergeFruit = Instantiate(fruitPrefabs[fruitID + 1], midpos, Quaternion.identity);
            mergeFruit.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        }
    }
}
