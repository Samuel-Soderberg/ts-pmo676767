using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    float timmer = 0;
    [SerializeField]
    TextMeshProUGUI text;
    void Start()
    {
    }
    private void Update()
    {
        timmer += Time.deltaTime;
        text.text = ((int)timmer).ToString();
    }
}
