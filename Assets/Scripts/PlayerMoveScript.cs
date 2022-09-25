using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float PLAYERSPEED = 0.05f;
    public float STOPDASHTIME = 0.5f;
    public float DASHMULTIPLIER = 5f;
    public float DASHCOOLDOWN = 3f;
    public Rigidbody rb;

    public bool isDashing;

    public LayerMask Ground;

    public GameObject PlayerBody;
    public GameObject mouseLocation;
    public GameObject PlayerParent;

    public CharacterController controller;
    public CharacterController cameraController;

    private float gravity = -9.81f;
    
    private bool CanDash = true;
    private Animator anim;
    private Vector3 gravityVelocity;
    private Vector3 PlayerVect;
    private Vector3 orientation;
    public float velocity;

    public Transform defaultCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = controller.velocity.magnitude;
        //Adds Gravity
        gravityVelocity.y += gravity * Time.deltaTime;
        controller.Move(gravityVelocity * Time.deltaTime);
        cameraController.Move(gravityVelocity * Time.deltaTime);

        Move();
        Dash();
        AttackCombo();
         
    }
    //Top Down Camera
    private void FixedUpdate()
    {
        //Casts ray to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f, Ground))
        {
            //Raycasts to get point on Ground
            mouseLocation.transform.position = ray.GetPoint(hit.distance);
            //Draws ray to location
            Debug.DrawRay(ray.origin, ray.direction * 1000);
            //Make sure there is a playerbody set
            if (PlayerBody != null)
            { 
                //Player look at mouseLocation
                 PlayerBody.transform.LookAt((mouseLocation.transform.position + new Vector3(mouseLocation.transform.position.x, 0f ,mouseLocation.transform.position.z)), Vector3.up * 0.05f * Time.deltaTime);
            }
        }
    }

    private void Dash()
    {
        //Checks for player's Input, if the player is dashing, and if player is able to dash
        if (Input.GetMouseButtonDown(1) && isDashing == false && CanDash == true)
        {
            defaultCam = Camera.main.transform;
            isDashing = true;
        }

        if (isDashing == true && CanDash == true)
        {
            //Plays Dodge animation
            anim.Play("Dodge", 0);

            //Moves controller and camera
            controller.Move(orientation * PLAYERSPEED * DASHMULTIPLIER);
            cameraController.Move((orientation * PLAYERSPEED * DASHMULTIPLIER));

            //Stops the dash
            StartCoroutine(StopDashing(STOPDASHTIME));
        }
    }
    //Player Attacking Animation
    private void AttackCombo()
    {
        //If left click
        if (Input.GetMouseButton(0))
        {
            //Set attack trigger
            anim.SetTrigger("Attack");
        }
        else { anim.ResetTrigger("Attack"); }

        //If current state's name is combo
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("combo"))
        {
            //and if left click again during combo
            if (Input.GetMouseButton(0))
            {
                //Set bool and trigger to perform second attack (if animation)
                anim.SetBool("Attacking2", true);
                anim.SetTrigger("Attack2");
            }
        }
        else
        {
            //Reset
            anim.ResetTrigger("Attack2");
            anim.SetBool("Attacking2", false);
        }
    }
    //Player Movement WASD
    private void Move()
    {
        //Gets Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        PlayerVect = new Vector3(0, 0, 0);

        //Checks if player is dashing
        if (isDashing == false)
        {
            //Gets Player control input ( A and D), moves player horizontally
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //Moves Player left and right
                PlayerVect = PlayerParent.transform.right * x;
                controller.Move(PlayerVect * PLAYERSPEED);

                //Moves camera with player
                cameraController.Move(PlayerVect * PLAYERSPEED);
            }

            //Gets Player control input ( W and S), moves player vertically
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                //Moves Player forward and back based on stationary parent transform
                PlayerVect = PlayerParent.transform.forward * z;
                controller.Move(PlayerVect * PLAYERSPEED);

                //Moves camera with player
                cameraController.Move(PlayerVect * PLAYERSPEED);

            }
        }

        //Orientation of the player
        orientation = mouseLocation.transform.position - gameObject.transform.position;
        orientation = orientation.normalized;

        //If z orientation is within range
        if (orientation.z > 0.5 || orientation.z < -0.5)
        {
            //Sets character animations
            anim.SetFloat("veloX", x);
            anim.SetFloat("veloY", z);
        }

        //If z orientation is within range, flip animator values
        if (orientation.z < 0.5 || orientation.z > -0.5)
        {
            //Sets flipped input to blend tree;
            anim.SetFloat("veloY", x, 0.2f, Time.deltaTime);
            anim.SetFloat("veloX", z, 0.2f, Time.deltaTime);
        }
    }
    //Stops dashing and calls cooldown coroutine
    private IEnumerator StopDashing(float dashTime)
    {
        yield return new WaitForSeconds(dashTime);
        CanDash = false;
        isDashing = false;
        StartCoroutine(DashCooldown(DASHCOOLDOWN));
    }

    //Wait cooldown time to dash again
    private IEnumerator DashCooldown(float cooldownTime)
    {
      
        yield return new WaitForSeconds(cooldownTime);
        CanDash = true;
    }


}
