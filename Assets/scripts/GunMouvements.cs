using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvements : MonoBehaviour
{
    private Camera MainCamera;
    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, MainCamera.nearClipPlane));

        float angleRad = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDeg));
    }
}