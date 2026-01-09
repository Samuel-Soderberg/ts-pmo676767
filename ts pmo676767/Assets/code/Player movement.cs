using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = dir * PlayerSettings.Instance.speed;
    }
}
