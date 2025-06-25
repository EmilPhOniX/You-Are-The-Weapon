using UnityEngine;

public class DestructionOnHit : MonoBehaviour
{
    public string targetTag = "bullet";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}