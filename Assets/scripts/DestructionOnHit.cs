using UnityEngine;

public class DestructionOnHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
