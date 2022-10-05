using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputHandler input;
    public Animator anim;

    [SerializeField] public Camera cam; // Camera Reference
    [SerializeField] public bool rotateTowardsMouse; // Enable or Disable Rotation Based on Mouse Input.

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed; // Player Move Speed
    [SerializeField] public float rotateSpeed; // Player Rotate Speed

    private void Awake()
    {
        input = GetComponent<InputHandler>(); // Grab input Component and assign to variable.
        anim = GetComponentInChildren<Animator>(); // Grab animator and assign that shit.
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(input.inputVector.x, 0, input.inputVector.y); // Create Target Vector based on our input vector from InputHandler script.

        var movementVector = MoveTowardTarget(targetVector); // Generate movementVector by calling MoveTowardTarget which returns a movementVector.

        if (!rotateTowardsMouse) // If we are not rotating with mouse,
            RotateTowardMovementVector(movementVector); // Rotate manually.
        else
            RotateTowardMouseVector(movementVector); // Rotate with mouse.

        //CalculateAnimation(targetVector);
    }

    // Rotate with Mouse Function
    private void RotateTowardMouseVector(Vector3 movementVector)
    {
        Ray ray = cam.ScreenPointToRay(input.mousePosition); // Generate raycast from camera to terrain.

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f)) // Limit distance and grab hitInfo from whatever the ray hits.
        {
            var target = hitInfo.point; // Assign target varable to the point at which hitInfo intersected with ground.
            target.y = transform.position.y; // Lock target's y position to player y position in order to prevent awkward backwards lean near a wall or object.
            transform.LookAt(target); // Rotate toward target.
        }
    }

    // Rotate without Mouse Function
    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) { return; } // If player is not moving, return. We do not want to rotate when we are not moving.

        var rotation = Quaternion.LookRotation(movementVector); // Calculate rotation degree
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed); // Rotate player towards movement direction at a declared rotation speed.
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime; // Set speed scaled by Time.deltaTime.

        targetVector = Quaternion.Euler(0, cam.gameObject.transform.eulerAngles.y, 0) * targetVector; // Create target rotation vector using euler angles and multiply by our targetVector.
        targetVector = Vector3.Normalize(targetVector);
        var targetPosition = transform.position + targetVector * speed; // targetPosition is where we want to be and at what speed we want to get there.
        transform.position = targetPosition; // set our transform to the targetPosition.
        return targetVector; // Return our movement vector.
    }

    /*
    private void CalculateAnimation(Vector3 targetVector)
    {
        Vector3 orientation = transform.forward - targetVector;
        orientation = Vector3.Normalize(orientation);
        
        if (orientation.z > 0.5 || orientation.z < -0.5)
        {
            //Sets character animations
            anim.SetFloat("veloX", targetVector.x);
            anim.SetFloat("veloY", targetVector.z);
        }

        if (orientation.z < 0.5 || orientation.z > -0.5)
        {
            //Sets flipped input to blend tree;
            anim.SetFloat("veloY", targetVector.x);
            anim.SetFloat("veloX", targetVector.z);
        }
    
    }
    */
}
