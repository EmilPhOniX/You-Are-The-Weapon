using UnityEngine;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyInterval = 5f;
    
    [Header("Speed Scaling")]
    [SerializeField] private float baseZombieSpeed = 2f;
    [SerializeField] private float speedIncrement = 0.1f;
    [SerializeField] private float maxZombieSpeed = 10f;
    
    [Header("Spawn Rate Scaling")]
    [SerializeField] private bool scaleSpawnRate = true;
    [SerializeField] private float spawnRateDecrease = 0.1f;
    [SerializeField] private float minSpawnInterval = 0.5f;

    public static DifficultyManager InstanceDM { get; private set; }
    
    private float currentSpeedMultiplier = 1f;
    private float currentSpawnRateMultiplier = 1f;
    private List<Zombie> activeZombies = new List<Zombie>();
    private List<SpawnZombies> spawners = new List<SpawnZombies>();
    
    public float CurrentZombieSpeed => Mathf.Min(baseZombieSpeed * currentSpeedMultiplier, maxZombieSpeed);
    public float CurrentSpawnRateMultiplier => currentSpawnRateMultiplier;
    
    void Awake()
    {
        if (InstanceDM == null)
            InstanceDM = this;
        else
            Destroy(gameObject);
            return;
    }
    
    void Start()
    {
        InvokeRepeating("IncreaseDifficulty", difficultyInterval, difficultyInterval);
    }
    
    private void IncreaseDifficulty()
    {
        float newSpeed = baseZombieSpeed * (currentSpeedMultiplier + speedIncrement);
        if (newSpeed <= maxZombieSpeed)
        {
            currentSpeedMultiplier += speedIncrement;
            UpdateAllZombiesSpeed();
        }
        
        if (scaleSpawnRate)
        {
            float newSpawnRateMultiplier = currentSpawnRateMultiplier + spawnRateDecrease;
            if (newSpawnRateMultiplier <= GetMaxSpawnRateMultiplier())
            {
                currentSpawnRateMultiplier = newSpawnRateMultiplier;
                UpdateAllSpawnersRate();
            }
        }
        
        Debug.Log($"Difficulty increased! Speed: {CurrentZombieSpeed}, Spawn Rate Multiplier: {currentSpawnRateMultiplier}");
    }
    
    private float GetMaxSpawnRateMultiplier()
    {
        return 4f;
    }
    
    private void UpdateAllZombiesSpeed()
    {
        // Nettoyer la liste des zombies détruits
        activeZombies.RemoveAll(zombie => zombie == null);
        
        // Mettre à jour la vitesse de tous les zombies actifs
        foreach (var zombie in activeZombies)
        {
            zombie.UpdateSpeed(CurrentZombieSpeed);
        }
    }
    
    private void UpdateAllSpawnersRate()
    {
        // Nettoyer la liste des spawners détruits
        spawners.RemoveAll(spawner => spawner == null);
        
        // Mettre à jour le taux de spawn de tous les spawners
        foreach (var spawner in spawners)
        {
            spawner.UpdateSpawnRate(currentSpawnRateMultiplier, minSpawnInterval);
        }
    }
    
    public void RegisterZombie(Zombie zombie)
    {
        if (!activeZombies.Contains(zombie))
        {
            activeZombies.Add(zombie);
            zombie.UpdateSpeed(CurrentZombieSpeed);
        }
    }
    
    public void UnregisterZombie(Zombie zombie)
    {
        activeZombies.Remove(zombie);
    }
    
    public void RegisterSpawner(SpawnZombies spawner)
    {
        if (!spawners.Contains(spawner))
            spawners.Add(spawner);
    }
    
    public void UnregisterSpawner(SpawnZombies spawner)
    {
        spawners.Remove(spawner);
    }
    
    // Optionnel : Reset de la difficulté
    public void ResetDifficulty()
    {
        currentSpeedMultiplier = 1f;
        currentSpawnRateMultiplier = 1f;
        UpdateAllZombiesSpeed();
        UpdateAllSpawnersRate();
    }
    
    public bool IsMaxSpeedReached()
    {
        return CurrentZombieSpeed >= maxZombieSpeed;
    }
    
    public bool IsMinSpawnIntervalReached()
    {
        return currentSpawnRateMultiplier >= GetMaxSpawnRateMultiplier();
    }
}