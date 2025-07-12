using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    [Header("Zombie Spawning")]
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnDistance = 2f;
    
    private Camera mainCamera;
    private float screenLeft, screenRight, screenTop, screenBottom;
    
    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            mainCamera = Camera.main;
            
        CalculateScreenBounds();
        InvokeRepeating("SpawnLoop", 0f, spawnInterval);
    }

    void CalculateScreenBounds()
    {
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        screenLeft = bottomLeft.x;
        screenRight = topRight.x;
        screenBottom = bottomLeft.y;
        screenTop = topRight.y;
    }
    
    void SpawnLoop()
    {
        Vector2 spawnPosition = GetRandomOffscreenPosition();
        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
    
    Vector2 GetRandomOffscreenPosition()
    {
        int side = Random.Range(0, 4);
        Vector2 position = Vector2.zero;
        
        switch (side)
        {
            case 0: // Côté gauche
                position = new Vector2(
                    screenLeft - spawnDistance,
                    Random.Range(screenBottom - spawnDistance, screenTop + spawnDistance)
                );
                break;
                
            case 1: // Côté droit
                position = new Vector2(
                    screenRight + spawnDistance,
                    Random.Range(screenBottom - spawnDistance, screenTop + spawnDistance)
                );
                break;
                
            case 2: // Côté haut
                position = new Vector2(
                    Random.Range(screenLeft - spawnDistance, screenRight + spawnDistance),
                    screenTop + spawnDistance
                );
                break;
                
            case 3: // Côté bas
                position = new Vector2(
                    Random.Range(screenLeft - spawnDistance, screenRight + spawnDistance),
                    screenBottom - spawnDistance
                );
                break;
        }
        
        return position;
    }
}