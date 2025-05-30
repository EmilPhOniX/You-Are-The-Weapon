using UnityEngine;

public class ZombieIA : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.1f * Time.deltaTime);
        }
    }
}
