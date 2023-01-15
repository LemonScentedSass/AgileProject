using UnityEngine;

public class FPPlayerLocomotion : MonoBehaviour
{
    private InputHandlerFirstPerson input; // Input Handler script that is used for first person inputs
    private Animator anim;

    [SerializeField] public Transform orientation; // New orientation object's transform, child of the Player prefab parent


    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed; // Player Move Speed

    private void Awake()
    {
        input = GetComponent<InputHandlerFirstPerson>();
        anim = GetComponentInChildren<Animator>();
    }

    // Everything below here operates exactly like the original movement, but is now relative
    // to the Orientation object's transform. The transform's rotation is set to the y rotation
    // of the camera, in the PlayerCam script. If we go with this camera angle, the orientation
    // object will be a one-stop shop for finding relative angles from the player.

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        var targetVector = new Vector3(input.inputVector.x, 0, input.inputVector.y); 

        Movement(targetVector);
    }

    private void Movement(Vector3 targetVector)
    {
        var movementVector = MoveTowardTarget(targetVector);

        AnimatePlayer(movementVector);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, orientation.eulerAngles.y, 0) * targetVector;
        targetVector = Vector3.Normalize(targetVector);
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void AnimatePlayer(Vector3 movementVector)
    {
        anim.SetFloat("veloX", input.inputVector.x, 0.2f, Time.deltaTime);
        anim.SetFloat("veloY", input.inputVector.y, 0.2f, Time.deltaTime);
    }
}
