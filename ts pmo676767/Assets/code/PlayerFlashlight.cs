using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFlashlight : MonoBehaviour
{
    GameObject flashlight;
    Light2D flash;
    private void Awake()
    {
        flashlight = GameObject.FindWithTag("flashlight");
        flash = flashlight.GetComponent<Light2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) TryTakePhoto();
        else
        {
            foreach (Enemy enemy in EnemyRegestry.allEnemys)
            {
                enemy.IsFlashed = false;
            }
            PlayerSettings.Instance.Flashing = false;
            flash.enabled = false;
        }
    }
    void TryTakePhoto()
    {
        PlayerSettings.Instance.Flashing = true;
        flash.enabled = true;
        foreach (Enemy enemy in EnemyRegestry.allEnemys)
        {
            if (IsInFlashCone(enemy.transform.position))
            {
                enemy.IsFlashed = true;
                Debug.Log("flashed: " + enemy.name);
            }
            else
                enemy.IsFlashed = false;
        }
    }
    private bool IsInFlashCone(Vector2 enemypos)
    {
        Camera cam = Camera.main;
        Vector2 toTarget = enemypos - new Vector2(transform.position.x, transform.position.y);
        Vector2 tomouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Vector2.Angle(toTarget, tomouse) <= PlayerSettings.Instance.Flashspread)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, enemypos - new Vector2(transform.position.x, transform.position.y));
            if (ray.collider.CompareTag("Enemy"))
            {
                if (Vector2.Distance(transform.position, enemypos) <= PlayerSettings.Instance.Flashrange)
                    return true;
            }

            return false;
        }
        else
            return false;
    }
}
