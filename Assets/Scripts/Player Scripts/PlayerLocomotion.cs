using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputHandler input;
    public Animator anim;

    public LayerMask Ground;

    [SerializeField] public Camera cam; // Camera Reference
    [SerializeField] public bool rotateTowardsMouse; // Enable or Disable Rotation Based on Mouse Input.

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed; // Player Move Speed
    [SerializeField] public float rotateSpeed; // Player Rotate Speed

    [Header("Dodge Settings")]
    [SerializeField] public float dodgeSpeedMultiplier; // Dodge Speed
    [SerializeField] public float dodgeLengthTime; // Length of time the dodge will last
    [SerializeField] public float dodgeCooldown; // Length of time between dodges
    [SerializeField] public bool isDodging; // Self-explanatory
    [SerializeField] public bool readyToDodge; // Whether or not the player is allowed to dodge
    private float dodgeSpeed;
    private float dodgeFinalDistance;
    private Vector3 dodgeFinalPosition;

    private float startTime;

    public enum PlayerState // List of states the player is able to be in, add to it as we add more functionality to the player
    {
        Moving,
        Dodging,
    }
    public PlayerState state; // Controls the state the player is in


    private void Awake()
    {
        input = GetComponent<InputHandler>(); // Grab input Component and assign to variable.
        anim = GetComponentInChildren<Animator>(); // Grab animator and assign that shit.
    }

    private void Start()
    {
        readyToDodge = true; // Allows the player to dodge for the first time
    }

    void Update()
    {
        MyInput(); // Always holds the targetVector (combined hor. and vert. inputs) and handles the inputs of different abilities
        StateHandler(); // Handles the various states the player can be in, and the transitions to and from each
    }

    private void MyInput()
    {
        var targetVector = new Vector3(input.inputVector.x, 0, input.inputVector.y); // Create Target Vector based on our input vector from InputHandler script.

        // Calls Dodge() using the combined hor. and vert. inputs once per dodge attempt, and will end the dodge after a set amount of time
        if (input.dodgeKey && readyToDodge && !isDodging)
        {
            readyToDodge = false;
            Dodge(targetVector);
            Invoke("EndDodge", dodgeLengthTime);
        }

        // Calls Dodge() each subsequent frame until the dodge is finished executing
        else if (isDodging)
        {
            Dodge(targetVector);
        }

        // Enables player movement in all cases EXCEPT FOR if the player is dodging
        else if (!isDodging)
        {
            Movement(targetVector);
        }
    }

    private void StateHandler()
    {
        // Mode - Dodging
        if (isDodging)
        {
            state = PlayerState.Dodging;
        }

        // Mode - Moving
        else if (!isDodging)
        {
            state = PlayerState.Moving;
        }
    }

    private void Movement(Vector3 targetVector)
    {
        var movementVector = MoveTowardTarget(targetVector); // Generate movementVector by calling MoveTowardTarget which returns a movementVector.

        if (!rotateTowardsMouse) // If we are not rotating with mouse,
            RotateTowardMovementVector(movementVector); // Rotate manually.
        else
            RotateTowardMouseVector(movementVector); // Rotate with mouse.

        CalculateAnimation(movementVector);
    }

    private void Dodge(Vector3 targetVector)
    {
        if (!isDodging)
        {
            startTime = Time.time;

            var rotation = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * 1000);

            // Next, find the distance from the player to where they will end up at the end of the dodge
            // by multiplying targetVector by speed and time set in inspector
            dodgeSpeed = moveSpeed * dodgeSpeedMultiplier;
            var time = dodgeLengthTime;
            targetVector = Vector3.Normalize(targetVector);

            //dodgeFinalPosition = transform.position + targetVector * dodgeSpeed;
            dodgeFinalDistance = dodgeSpeed * time;
            dodgeFinalPosition = transform.position + targetVector * dodgeSpeed * time;
            //dodgeFinalDistance = Vector3.Distance(transform.position, dodgeFinalPosition);
            Debug.Log("Final Distance: " + dodgeFinalDistance);

            anim.SetFloat("dodgeAnimSpeed", dodgeLengthTime * 1.867f + 0.1f);
            anim.SetTrigger("Dodging");
        }

        isDodging = true;        

        float distCovered = (Time.time - startTime) * dodgeSpeed;
        float fractionOfDodge = distCovered / dodgeFinalDistance;
        Debug.Log("frac of Dodge: " + fractionOfDodge);
        transform.position = Vector3.Lerp(transform.position, dodgeFinalPosition, fractionOfDodge);
    }

    private void EndDodge()
    {
        Invoke(nameof(ResetDodge), dodgeCooldown);
        isDodging = false;
        readyToDodge = false;
    }

    private void ResetDodge()
    {
        readyToDodge = true;
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

    private void CalculateAnimation(Vector3 movementVector)
    {
        Ray ray = cam.ScreenPointToRay(input.mousePosition); // New ray from camera using mousePosition from InputHandler script

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f, Ground)) // If the ray hits a collider within 300 units with the Ground layermask
        {
            Vector3 target = hitInfo.point; // target created from the position of the RaycastHit
            Vector3 orientation = target - transform.position; // orientation gets the difference from target to transform positions
            orientation = orientation.normalized; // Normalizes this Vector3

            //If z orientation is within range
            if (orientation.z > 0.5 || orientation.z < -0.5)
            {
                //Sets character animations
                anim.SetFloat("veloX", input.inputVector.x, 0.2f, Time.deltaTime);
                anim.SetFloat("veloY", input.inputVector.y, 0.2f, Time.deltaTime);
            }

            //If z orientation is within range, flip animator values
            if (orientation.z < 0.5 && orientation.z > -0.5)
            {
                //Sets flipped input to blend tree;
                anim.SetFloat("veloY", input.inputVector.x, 0.2f, Time.deltaTime);
                anim.SetFloat("veloX", input.inputVector.y, 0.2f, Time.deltaTime);
            }
        }
    }
}
