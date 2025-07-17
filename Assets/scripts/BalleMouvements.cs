using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalleMouvements : MonoBehaviour
{
    private Rigidbody2D balleRb;
    private int penetrationLeft;
    private int maxPenetration;

    void Awake()
    {
        balleRb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed, int penetration)
    {
        balleRb.linearVelocity = direction.normalized * speed;
        maxPenetration = penetration;
        penetrationLeft = penetration;
    }

    public bool CanPenetration()
    {
        return penetrationLeft > 0;
    }

    public void usePenetration()
    {
        if (penetrationLeft > 0)
            penetrationLeft--;
    }

    public int GetPenetrationLeft() => penetrationLeft;
    public int GetMaxPenetration() => maxPenetration;
}
