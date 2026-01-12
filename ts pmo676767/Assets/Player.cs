using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    Vector2 dir;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        /* dir = Vector2.zero;
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
        
        rb.linearVelocity = dir.normalized * speed;
        Debug.Log(dir.normalized);
        */

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = dir * speed;
    }
}
