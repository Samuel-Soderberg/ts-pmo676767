using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 currentVelocity;
    Rigidbody2D rb;
    Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        Vector2 targetVelocity = direction * PlayerSettings.Instance.maxspeed;
        if (direction.magnitude > 0)
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, PlayerSettings.Instance.acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, PlayerSettings.Instance.deceleration * Time.fixedDeltaTime);
        }
        rb.linearVelocity = currentVelocity;
    }
}
