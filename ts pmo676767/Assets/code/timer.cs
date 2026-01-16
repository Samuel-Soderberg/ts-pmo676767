using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class timer : MonoBehaviour
{
    float timmer = 0;
    [SerializeField]
    TextMeshProUGUI timertext;
    [SerializeField]
    TextMeshProUGUI bodytext;
    [SerializeField]
    TextMeshProUGUI gaoltext;
    public GameObject goal;
    public string sceen;
    public GameObject button;
    int bodiesfound = 0;
    [SerializeField]
    GameObject image;

    Collider2D col;
    Collider2D enemycol;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        image.SetActive(false);
        bodytext.enabled = true;
        gaoltext.enabled = false;
        timertext.enabled = true;
    }
    private void Update()
    {
        foreach (var enemy in EnemyRegestry.allEnemys)
        {
            enemycol = enemy.GetComponent<Collider2D>();
            if (col.IsTouching(enemycol))
                death();
        }
        timmer += Time.deltaTime;
        timertext.text = $"Time elapsed: {((int)timmer).ToString()}";
        bodiesfound = 0;
        foreach (Body b in BodyRegistry.allBodies)
        {
            if (b.photographed)
            {
                bodiesfound++;
            }
        }
        bodytext.text = $"Bodies found {bodiesfound}/{BodyRegistry.allBodies.Count}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == goal)
        {
            if (bodiesfound == BodyRegistry.allBodies.Count)
                wictory();
        }
    }
    void wictory()
    {
        Time.timeScale = 0f;
        bodytext.enabled = false;
        timertext.enabled = false;
        image.SetActive(true);
        gaoltext.enabled = true;
        gaoltext.text = "Victory";
    }
    void death()
    {
        Time.timeScale = 0f;
        bodytext.enabled = false;
        timertext.enabled = false;
        image.SetActive(true);
        gaoltext.enabled = true;
        gaoltext.text = "Defeat";
    }
}
