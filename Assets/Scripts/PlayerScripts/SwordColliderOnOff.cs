using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderOnOff : MonoBehaviour
{
    public MeshCollider swordCollider;
    public BoxCollider swordColliderBox;

    public bool useBoxCollider;

    public void EnableSwordCollider()
    {
        if (useBoxCollider)
        {
            Debug.Log("Box Collider On");
            swordColliderBox.enabled = true;
        }
        else
        {
            Debug.Log("Mesh Collider On");
            swordCollider.enabled = true;
        }
    }

    public void DisableSwordCollider()
    {
        if (useBoxCollider)
        {
            Debug.Log("Box Collider Off");
            swordColliderBox.enabled = false;
        }
        else
        {
            Debug.Log("Mesh Collider Off");
            swordCollider.enabled = false;
        }
    }
}
