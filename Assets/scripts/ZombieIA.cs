using UnityEngine;

[System.Serializable]
public class SpriteWithProbability
{
    public Sprite sprite;
    [Range(0f, 1f)]
    public float probability;
}

public class ZombieIA : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.1f;
    public SpriteWithProbability[] SpritesWithProbabilities;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GetRandomSpriteByProbability();
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position) * Quaternion.Euler(0, 0, -90);
        }
    }

    private Sprite GetRandomSpriteByProbability()
    {
        float totalProbability = 0f;
        foreach (var spriteProb in SpritesWithProbabilities)
        {
            totalProbability += spriteProb.probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float currentProbability = 0f;

        foreach (var spriteProb in SpritesWithProbabilities)
        {
            currentProbability += spriteProb.probability;
            if (randomValue <= currentProbability)
            {
                return spriteProb.sprite;
            }
        }

        return SpritesWithProbabilities[0].sprite;
    }
}
