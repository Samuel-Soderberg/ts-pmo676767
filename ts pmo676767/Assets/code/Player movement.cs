using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 currentVelocity;
    Rigidbody2D rb;
    Vector2 direction;
    static Animator animator;
    public static string currentState;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetKey(KeyCode.A))
        {
            ChangeAnimationState("walk left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ChangeAnimationState("walk right");
        }
        else
        {
            ChangeAnimationState("idle");
        }
    }
    void FixedUpdate()
    {
        if (PlayerSettings.Instance.Flashing)
        {
            Vector2 flashtargetVelocity = direction * PlayerSettings.Instance.maxspeed * PlayerSettings.Instance.Flashingspeed;
            if (direction.magnitude > 0)
            {
                currentVelocity = Vector2.MoveTowards(currentVelocity, flashtargetVelocity, PlayerSettings.Instance.acceleration * PlayerSettings.Instance.Flashingspeed * Time.fixedDeltaTime);
            }
            else
            {
                currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, PlayerSettings.Instance.deceleration * PlayerSettings.Instance.Flashingspeed * Time.fixedDeltaTime);
            }
            rb.linearVelocity = currentVelocity;
        }
        else
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
    public static void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("camera")) return;
        animator.Play(newState); 
        currentState = newState;
    }
}
