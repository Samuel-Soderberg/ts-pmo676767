using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TryTakePhoto();
    }
    void TryTakePhoto()
    {
        foreach (Body body in BodyRegistry.allBodies)
        {
            if (body.photographed)
                continue;
            else if (IsInCameraCone(body.transform.position))
            {
                body.photographed = true;
                Debug.Log("Photographed: " + body.name);
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
            RaycastHit2D ray = Physics2D.Raycast(transform.position, bodypos - new Vector2(transform.position.x, transform.position.y));
            //Debug.Log("Spread = true");
            if (ray.collider.CompareTag("Photographable"))
            {
                //Debug.Log("Sight = true");
                if (Vector2.Distance(transform.position, bodypos) <= PlayerSettings.Instance.Camerarange)
                    return true;
            }

            return false;
        }
        else
            return false;
    }
}
