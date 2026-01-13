using UnityEngine;

public class PlayerSettings : MonoBehaviour 
{
    [Header("Player Movement")]
    public float maxspeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float Flashingspeed = 0.5f;
    [Header("Camera")]
    public float Camerarange = 3f;
    public float Cameraspread = 30f;
    [Header("Flashlight")]
    public float Flashspread = 30f;
    public float Flashrange = 30f;

    public bool Flashing = false;

    public static PlayerSettings Instance;
    void Awake()
    {
        Instance = this;
    }
}
