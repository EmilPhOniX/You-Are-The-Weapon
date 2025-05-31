using UnityEngine;

public class shoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float spawnInterval = 3f;

    void Start()
    {
        InvokeRepeating(nameof(ShootBullet), 0f, spawnInterval);
    }

    void ShootBullet()
    {
        Vector2 shootDirection = transform.right.normalized;
        Vector2 spawnPosition = (Vector2)transform.position + shootDirection * 2f;

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        BalleMouvements balleScript = bullet.GetComponent<BalleMouvements>();
        balleScript.Initialize(shootDirection, bulletSpeed);

        Destroy(bullet, 2f);
    }
}
