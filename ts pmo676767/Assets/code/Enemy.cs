using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private bool haslineofsight = false;
    private Rigidbody2D rb;
    public bool IsFlashed;
    [SerializeField] private float Speed;
    [SerializeField] private float FlashedSpeed;
    static Animator animator;
    public static string currentStateenemy;
    Vector2 dir = new Vector2 (0,0);
    private Light2D childLight;
    LayerMask mask;
    void Awake()
    {
        mask = LayerMask.GetMask("player", "wall");
        childLight = GetComponentInChildren<Light2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        EnemyRegestry.allEnemys.Add(this);
    }
    private void Update()
    {
        if (IsFlashed)
        {
            //ChangeAnimationStateenemy("attack");
            childLight.enabled = true;
        }
   
        else if (dir == new Vector2(0, 0))
        {
            childLight.enabled = false;
            //ChangeAnimationStateenemy("idle");
        }
        else if (dir != new Vector2(0, 0))
        {
            childLight.enabled = true;
            //ChangeAnimationStateenemy("walk");
        }
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= 20)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position,Mathf.Infinity,mask);
            if (ray.collider != null)
            {
                haslineofsight = ray.collider.CompareTag("Player");
                if (haslineofsight)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                    if (IsFlashed)
                    {
                        dir = (player.transform.position - transform.position).normalized * (Speed * FlashedSpeed);
                    }
                    else
                        dir = (player.transform.position - transform.position).normalized * Speed;
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    dir = new Vector2(0, 0);
                }

            }
        }
        else
            dir = new Vector2(0, 0);

        
        

        if (dir.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if (dir.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        rb.linearVelocity = dir;
    }
    /*public static void ChangeAnimationStateenemy(string newState)
    {
        if (currentStateenemy == newState) return;
        Debug.Log(newState);
        animator.Play(newState);
        currentStateenemy = newState;
    }*/
}
