using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIconToPosition : MonoBehaviour
{
    public Transform objectToMoveTo;

    private void LateUpdate()
    {
        transform.position = new Vector3(objectToMoveTo.position.x, 50, objectToMoveTo.position.z);
    }
}
