using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private GameObject tempFruit;

    public bool gamePlay = true;

    public TextMeshProUGUI scoreText;

    public GameObject gameOverBoard;
    public Button restartButton;
    float leftmostX = float.MaxValue;
    float rightmostX = float.MinValue;

    private int score = 0;

    public GameObject[] fruitPrefabs;
    private int currentSpawn = 0;

    private int biggestFruit = 0;

    private GameObject dropFruit;

    void Start() {
        score = 0;
        newDropFruit();
        restartButton.gameObject.SetActive(false);
        gameOverBoard.gameObject.SetActive(false);

        restartButton.onClick.AddListener(restartGame);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.deltaPosition.x < 0)
            {
                // move left!
                dropFruit.transform.position += Vector3.left * 3f * Time.deltaTime;
            }

            if (touch.deltaPosition.x > 0)
            {
                dropFruit.transform.position += Vector3.right * 3f * Time.deltaTime;
            }

            if (gamePlay && touch.tapCount == 2 && dropFruit != null)
            {
                dropFruit.GetComponent<Rigidbody2D>().gravityScale = 1f;
                StartCoroutine(SetBeenReleased(dropFruit));
                dropFruit = null;
                StartCoroutine(SpawnAfterDelay());
            }
        }

        if (gamePlay && Input.GetKeyDown(KeyCode.Space) && dropFruit != null)
        {            
            dropFruit.GetComponent<Rigidbody2D>().gravityScale = 1f;
            StartCoroutine(SetBeenReleased(dropFruit));
            dropFruit = null;
            StartCoroutine(SpawnAfterDelay());
        }
        if (gamePlay && dropFruit != null) {
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

            if (leftmostX > -3.36 && Input.GetKey(KeyCode.LeftArrow))
            {
                dropFruit.transform.position += Vector3.left * 3f * Time.deltaTime;
            } else if (rightmostX < 3.36 && Input.GetKey(KeyCode.RightArrow))
            {
                dropFruit.transform.position += Vector3.right * 3f * Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);
        if (tempFruit != null) {
            tempFruit.GetComponent<FruitScript>().beenRel = true;
        }
        newDropFruit();
    }

    IEnumerator SetBeenReleased(GameObject fruit)
    {
        yield return new WaitForSeconds(3f);
        if (fruit != null) {
            fruit.GetComponent<FruitScript>().beenRel = true;
        }
    }

    void newDropFruit() {
        if (fruitPrefabs.Length == 0)
        {
            Debug.Log("AAA");
            return;
        }
        dropFruit = Instantiate(fruitPrefabs[Random.Range(0, Mathf.Min(biggestFruit+1, 5))], new Vector3(0, 4.4f, 0), Quaternion.identity);
        dropFruit.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-60f, 60f));
        dropFruit.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

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
            if (fruitID + 1 > biggestFruit) {
                biggestFruit = fruitID + 1;
            }
            score += (int)Mathf.Pow(2f, (float)fruitID);
            Debug.Log("yo");
            Debug.Log(score);
            scoreText.text = score.ToString();
            GameObject mergeFruit = Instantiate(fruitPrefabs[fruitID + 1], midpos, Quaternion.identity);
            mergeFruit.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        }
    }

    void restartGame() {
        Debug.Log("burh");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
