using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerFirstPerson : MonoBehaviour
{
    // Mostly identical to the original Input Handler, but with unused features stripped away for now.

    public Vector2 inputVector { get; private set; }

    void Update()
    {
        // Movement
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        inputVector = new Vector2(h, v);
    }
}
