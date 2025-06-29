using UnityEngine;

public class DestructionOnHit : MonoBehaviour
{
    public string targetTag = "bullet";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Zombie zombie = GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.Die();
        }
        else
        {
            Destroy(gameObject);
        }

        Destroy(other.gameObject);
    }
}