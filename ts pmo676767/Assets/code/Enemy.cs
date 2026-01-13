using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private bool haslineofsight = false;
    private Rigidbody2D rb;
    public bool IsFlashed;
    [SerializeField] private float Speed;
    [SerializeField] private float FlashedSpeed;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        EnemyRegestry.allEnemys.Add(this);
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
                    if (IsFlashed)
                    {
                        rb.linearVelocity = (player.transform.position - transform.position).normalized * (Speed * FlashedSpeed);
                    }
                    else
                        rb.linearVelocity = (player.transform.position - transform.position).normalized * Speed;
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
