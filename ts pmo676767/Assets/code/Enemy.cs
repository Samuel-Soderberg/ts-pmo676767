using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    bool haslineofsight = false;
    Rigidbody2D rb;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= 20)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
            if (ray.collider != null)
            {
                haslineofsight = ray.collider.CompareTag("Player");
                if (haslineofsight)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                    rb.linearVelocity = (player.transform.position - transform.position).normalized * 3;
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    rb.linearVelocity = new Vector2(0, 0);
                }

            }
        }
        else
            rb.linearVelocity = new Vector2(0, 0);
    }
}
