using System;
using UnityEngine;

[System.Serializable]
public class RandomSprite
{
    [SerializeField] private Sprite sprite;
    [SerializeField, Range(0f, 1f)] private float probability;

    public Sprite Sprite => sprite;
    public float Probability => probability;
}

public class Zombie : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private float baseSpeed = 2f;


    [Header("Sprites")]
    [SerializeField] private RandomSprite[] randomSprites;

    private ScoreNGold scoreNGold;
    private SpriteRenderer spriteRenderer;
    private float speed;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InitializeSprite();

        if (scoreNGold == null)
        {
            scoreNGold = FindFirstObjectByType<ScoreNGold>();
            if (scoreNGold == null)
            {
                Debug.LogError("ScoreNGold component not found in the scene.");
            }
        }

        if (DifficultyManager.Instance != null)
        {
            DifficultyManager.Instance.RegisterZombie(this);
        }
        else
        {
            speed = baseSpeed;
        }

    }

    void Update()
    {
        HandleMovement();
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    private void InitializeSprite()
    {
        if (spriteRenderer != null && randomSprites.Length > 0)
        {
            spriteRenderer.sprite = GetRandomSprite();
        }
    }
    
    private void HandleMovement()
    {
        if (target == null) return;
        
        MoveTowardsTarget();
        RotateTowardsTarget();
    }
    
    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            target.position, 
            speed * Time.deltaTime
        );
    }
    
    private void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private Sprite GetRandomSprite()
    {
        if (randomSprites.Length == 0)
            return null;
            
        float totalProbability = CalculateTotalProbability();
        
        if (totalProbability <= 0f)
        {
            return randomSprites[0].Sprite;
        }
        
        float randomValue = UnityEngine.Random.Range(0f, totalProbability);
        float currentProbability = 0f;
        
        foreach (var spriteProb in randomSprites)
        {
            currentProbability += spriteProb.Probability;
            if (randomValue <= currentProbability)
            {
                return spriteProb.Sprite;
            }
        }
        
        return randomSprites[0].Sprite;
    }
    
    private float CalculateTotalProbability()
    {
        float total = 0f;
        foreach (var spriteProb in randomSprites)
        {
            total += spriteProb.Probability;
        }
        return total;
    }
    
    private void GrantLootReward()
    {
        if (scoreNGold == null)
        {
            Debug.LogWarning("ScoreNGold reference is not set. Cannot grant loot reward.");
            return;   
        }        

        var zombieReward = ZombieReward.GenerateRandom();
        scoreNGold.AddSnG(zombieReward.Score, zombieReward.Gold);
    }
    
    public void Die()
    {
        GrantLootReward();
        Destroy(gameObject);
        Console.WriteLine("Zombie died");
    }
}

[System.Serializable]
public struct ZombieReward
{
    [SerializeField] private int score;
    [SerializeField] private int gold;
    
    public int Score => score;
    public int Gold => gold;
    
    public ZombieReward(int score, int gold)
    {
        this.score = score;
        this.gold = gold;
    }
    
    public static ZombieReward GenerateRandom()
    {
        int randomScore = UnityEngine.Random.Range(1, 5);
        int calculatedGold = Mathf.RoundToInt(randomScore * 0.7f);
        
        return new ZombieReward(randomScore, calculatedGold);
    }
}