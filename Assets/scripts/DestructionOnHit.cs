using UnityEngine;
public class DestructionOnHit : MonoBehaviour
{
    public string targetTag = "bullet";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Zombie zombie = GetComponent<Zombie>();
            BalleMouvements bullet = other.GetComponent<BalleMouvements>();
            
            if (zombie != null && bullet != null)
            {
                // Le zombie meurt
                zombie.Die();
                
                // Utilise une charge de pénétration
                bullet.usePenetration();
                
                // Détruit la balle seulement si elle n'a plus de pénétration
                if (!bullet.CanPenetration())
                {
                    Destroy(other.gameObject);
                }
                
                Debug.Log($"Zombie hit! Bullet penetration left: {bullet.GetPenetrationLeft()}");
            }
            else if (zombie != null)
            {
                // Fallback si pas de système de pénétration
                zombie.Die();
                Destroy(other.gameObject);
            }
        }
    }
}