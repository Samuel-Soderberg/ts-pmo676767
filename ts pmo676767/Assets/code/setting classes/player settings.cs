using UnityEngine;

public class PlayerSettings : MonoBehaviour 
{
    [Header("Player Movement")]
    public float maxspeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float Camerarange = 3f;
    public float Cameraspread = 30f;

    public static PlayerSettings Instance;
    void Awake()
    {
        Instance = this;
    }
}
