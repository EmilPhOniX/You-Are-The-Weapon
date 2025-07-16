using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiePlayerCollision : MonoBehaviour
{
    public string playerTag = "Player";
    public string gameOverSceneName = "GameOver";
    public bool useGameOverScreen = false; // Changé à false pour charger une scène directement
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet touché est le joueur
        if (other.CompareTag(playerTag))
        {
            // Vérifier si cet objet est bien un zombie
            Zombie zombie = GetComponent<Zombie>();
            if (zombie != null)
            {
                TriggerGameOver();
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Version avec Collision2D au cas où vous utilisez des Colliders normaux
        if (collision.gameObject.CompareTag(playerTag))
        {
            Zombie zombie = GetComponent<Zombie>();
            if (zombie != null)
            {
                TriggerGameOver();
            }
        }
    }
    
    private void TriggerGameOver()
    {
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            // Remettre le timeScale à 1 avant de changer de scène
            Time.timeScale = 1f;
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            // Fallback : recharger la scène actuelle
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}