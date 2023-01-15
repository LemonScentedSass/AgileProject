using UnityEngine;

public class FPPlayerLocomotion : MonoBehaviour
{
    private InputHandlerFirstPerson input;

    [SerializeField] public Transform orientation;

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed; // Player Move Speed

    private void Awake()
    {
        input = GetComponent<InputHandlerFirstPerson>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        var targetVector = new Vector3(input.inputVector.x, 0, input.inputVector.y);
        Debug.Log(targetVector);

        Movement(targetVector);
    }

    private void Movement(Vector3 targetVector)
    {
        var movementVector = MoveTowardTarget(targetVector);
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
}
