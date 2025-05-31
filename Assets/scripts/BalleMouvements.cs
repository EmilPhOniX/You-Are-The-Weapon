using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalleMouvements : MonoBehaviour
{
    private Rigidbody2D balleRb;

    void Awake()
    {
        balleRb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed)
    {
        balleRb.linearVelocity = direction.normalized * speed;
    }
}
