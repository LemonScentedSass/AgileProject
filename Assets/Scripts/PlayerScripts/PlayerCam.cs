using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform camHolder; // Transform of the Camera Holder object   
    public Transform playerModel;

    float xRotation;
    float yRotation; // floats storing the current x and y rotation of the camera

    private void Start()
    {
        // Locks and hides the cursor on game start, supposedly
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camHolder = GetComponentInParent<Transform>();
        playerModel = Hotbar.instance.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        // Assign Rotation Variables Based On Mouse Input
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate Cam and Orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerModel.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
