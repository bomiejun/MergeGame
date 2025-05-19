using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    void OnEnable()
    {
        CollisionReporter.OnGlobalCollision += HandleCollision;
    }

    void OnDisable()
    {
        CollisionReporter.OnGlobalCollision -= HandleCollision;
    }

    void HandleCollision(GameObject a, GameObject b)
    {
        Debug.Log($"Manager: {a.name} collided with {b.name}");
        // Handle collision logic here
    }
}
