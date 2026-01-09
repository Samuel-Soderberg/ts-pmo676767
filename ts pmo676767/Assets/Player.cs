using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    int speed = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {


        }
        if (Input.GetKey(KeyCode.DownArrow))
        {


        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {


        }
        else
        {
            speed = 0;
        }
    }
}
