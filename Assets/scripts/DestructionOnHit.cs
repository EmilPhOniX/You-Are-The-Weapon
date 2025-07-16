using UnityEngine;

public class DestructionOnHit : MonoBehaviour
{
    public string targetTag = "bullet";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Zombie zombie = GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.Die();
                Destroy(other.gameObject);
            }
        }
    }
}