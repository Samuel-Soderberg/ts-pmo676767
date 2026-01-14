using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    Collider2D col;
    Collider2D enemycol;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        
    }
    void Update()
    {
        foreach (var enemy in EnemyRegestry.allEnemys)
        {
            enemycol = enemy.GetComponent<Collider2D>();
           if(col.IsTouching(enemycol))
                SceneManager.LoadScene("start meny");
        }
    }
}
