using UnityEngine;
using System.Collections;

public class SpawnZombies : MonoBehaviour
{
    [Header("Zombie Spawning")]
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float spawnInterval = 3f;
    
    void Start()
    {
        InvokeRepeating("SpawnLoop", 0f, spawnInterval);
    }

    void SpawnLoop()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-20, 20), Random.Range(-15, 15));
        GameObject zombie = Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
    }
}