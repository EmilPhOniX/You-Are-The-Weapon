using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int bulletPenetration = 1;
    
    [Header("Firing Settings")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireDelay = 1f;
    
    [Header("Upgrade Values")]
    [SerializeField] private float speedUpgradeAmount = 5f;
    [SerializeField] private float fireRateUpgradeAmount = 0.2f;
    [SerializeField] private int penetrationUpgradeAmount = 1;
    
    [Header("Upgrade Limits")]
    [SerializeField] private float maxBulletSpeed = 50f;
    [SerializeField] private float maxFireRate = 10f;
    [SerializeField] private int maxPenetration = 10;
    
    [Header("Spawn Settings")]
    [SerializeField] private float bulletSpawnDistance = 2f;
    [SerializeField] private float bulletLifetime = 5f;
    
    public static Gun InstanceGun { get; private set; }
    private float currentFireInterval;
    
    void Start()
    {
        UpdateFireInterval();
        InvokeRepeating(nameof(FireBullet), fireDelay, currentFireInterval);
    }
    
    void FireBullet()
    {
        Vector2 shootDirection = transform.right.normalized;
        Vector2 spawnPosition = (Vector2)transform.position + shootDirection * bulletSpawnDistance;
        
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        
        BalleMouvements bulletMovement = bullet.GetComponent<BalleMouvements>();
        if (bulletMovement != null)
            bulletMovement.Initialize(shootDirection, bulletSpeed, bulletPenetration);
        else
            Debug.LogError("BalleMouvements component not found on bullet prefab!");
        
        Destroy(bullet, bulletLifetime);
    }
    
    private void UpdateFireInterval()
    {
        currentFireInterval = 1f / fireRate;
    }
    
    private void RestartFiring()
    {
        CancelInvoke(nameof(FireBullet));
        UpdateFireInterval();
        InvokeRepeating(nameof(FireBullet), 0f, currentFireInterval);
    }
    
    public void UpgradeBulletSpeed()
    {
        float newSpeed = bulletSpeed + speedUpgradeAmount;
        bulletSpeed = Mathf.Min(maxBulletSpeed, newSpeed);
        
        Debug.Log($"Bullet speed upgraded to: {bulletSpeed}");
    }
    
    public void UpgradeBulletPenetration()
    {
        int newPenetration = bulletPenetration + penetrationUpgradeAmount;
        bulletPenetration = Mathf.Min(maxPenetration, newPenetration);
        
        Debug.Log($"Bullet penetration upgraded to: {bulletPenetration}");
    }
    
    public void UpgradeFireRate()
    {
        float newFireRate = fireRate + fireRateUpgradeAmount;
        fireRate = Mathf.Min(maxFireRate, newFireRate);
        
        RestartFiring();
        Debug.Log($"Fire rate upgraded to: {fireRate} shots/sec");
    }
    
    public float GetBulletSpeed() => bulletSpeed;
    public int GetBulletPenetration() => bulletPenetration;
    public float GetFireRate() => fireRate;
    public float GetCurrentFireInterval() => currentFireInterval;
    
    public bool CanUpgradeSpeed() => bulletSpeed < maxBulletSpeed;
    public bool CanUpgradePenetration() => bulletPenetration < maxPenetration;
    public bool CanUpgradeFireRate() => fireRate < maxFireRate;
}