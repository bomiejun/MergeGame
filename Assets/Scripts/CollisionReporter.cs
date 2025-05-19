using UnityEngine;
using System;

public class CollisionReporter : MonoBehaviour
{
    public static event Action<GameObject, GameObject> OnGlobalCollision;

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnGlobalCollision?.Invoke(gameObject, collision.gameObject);
    }
}
