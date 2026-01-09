using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    int speed = 1;
    Vector2 dir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x += 1;
        }
        rb.linearVelocity = dir * speed;
    }
}
