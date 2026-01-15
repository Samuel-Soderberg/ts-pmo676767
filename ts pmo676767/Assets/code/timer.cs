using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    float timmer = 0;
    [SerializeField]
    TextMeshProUGUI text;
    public GameObject goal;
    public string sceen;
    public GameObject button;



    private void Update()
    {
        timmer += Time.deltaTime;
        text.text = ((int)timmer).ToString();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            Time.timeScale = 0f;
            print(timmer);
            button.gameObject.SetActive(true);

        }
    }

}
