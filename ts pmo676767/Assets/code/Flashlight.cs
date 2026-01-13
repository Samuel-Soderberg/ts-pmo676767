using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    Light2D light;
    private void Update()
    {
        light = GetComponent<Light2D>();
        light.pointLightOuterAngle = PlayerSettings.Instance.Flashspread;
        light.pointLightInnerAngle = PlayerSettings.Instance.Flashspread - 5;
        light.pointLightOuterRadius = PlayerSettings.Instance.Flashrange;
        light.pointLightInnerRadius = PlayerSettings.Instance.Flashrange - 1;
    }
}
