using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    float timer = 1;
    LayerMask mask;
    public AudioSource audioSource;

    private void Awake()
    {
        mask = LayerMask.GetMask("body", "wall");
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)&&timer <=0) 
        {
            Invoke("TryTakePhoto", 0.3f);
            PlayerMovement.ChangeAnimationState("camera");
            timer = 1;
        }
    }
    void TryTakePhoto()
    {
        audioSource.Play();
        //Debug.Log("body");
        CameraFlash.Cameraflash = true;
        foreach (Body body in BodyRegistry.allBodies)
        {
            if (body.photographed)
                continue;
            else if (IsInCameraCone(body.transform.position))
            {
                //Debug.Log("body photo");
                body.photographed = true;
            }
        }
    }
    private bool IsInCameraCone(Vector2 bodypos)
    {
        Camera cam = Camera.main;
        Vector2 toTarget = bodypos - new Vector2(transform.position.x, transform.position.y);
        Vector2 tomouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Debug.Log(Vector2.Angle(toTarget, tomouse));
        if (Vector2.Angle(toTarget, tomouse) <= PlayerSettings.Instance.Cameraspread)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, bodypos - new Vector2(transform.position.x, transform.position.y),Mathf.Infinity, mask);
            //Debug.Log("Spread = true");
            if (ray.collider.CompareTag("Photographable"))
            {
                //Debug.Log("Sight = true");
                if (Vector2.Distance(transform.position, bodypos) <= PlayerSettings.Instance.Camerarange)
                {
                    return true; 
                }
            }
            return false;
        }
        else
            return false;
    }
}
