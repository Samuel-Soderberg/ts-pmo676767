using UnityEngine;

public class pointtocursor : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        Vector2 direction = (cam.ScreenToWorldPoint(Input.mousePosition)- transform.position).normalized;
        transform.up = direction;
    }
}
