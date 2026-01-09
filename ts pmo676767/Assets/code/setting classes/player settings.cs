using UnityEngine;

public class PlayerSettings : MonoBehaviour 
{
    [Header("Player Movement")]
    public float speed = 5f;

    
    public static PlayerSettings Instance;
    void Awake()
    {
        Instance = this;
    }
}
