using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFlash : MonoBehaviour
{
    Light2D Lighting;
    float timer = 0.1f;
    public static bool Cameraflash;
    private void Start()
    {
        Lighting = GetComponent<Light2D>();
        Lighting.enabled = false;
        Lighting.pointLightOuterAngle = PlayerSettings.Instance.Cameraspread;
        Lighting.pointLightInnerAngle = PlayerSettings.Instance.Cameraspread - 5;
        Lighting.pointLightOuterRadius = PlayerSettings.Instance.Camerarange;
        Lighting.pointLightInnerRadius = PlayerSettings.Instance.Camerarange - 1;
    }
    private void Update()
    {
        if (Cameraflash)
        {
            if (!Lighting.enabled)
            {
                Invoke("turnffflash", 0.1f);
            }
            Lighting.enabled = true;
        }
    }
    void turnffflash()
    {
        Lighting.enabled = false;
        Cameraflash = false;
    }
}
