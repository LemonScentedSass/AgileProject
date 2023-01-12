using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderOnOff : MonoBehaviour
{
    public MeshCollider swordCollider;

    public void EnableSwordCollider()
    {
        if (swordCollider != null)
        {
            //Debug.Log("Collider enabled");
            swordCollider.enabled = true;
        }
        else
        {
            Debug.Log("swordCollider not set");            
        }
    }

    public void DisableSwordCollider()
    {
        if (swordCollider != null)
        {
            //Debug.Log("Collider disabled");
            swordCollider.enabled = false;
        }
        else
        {
            Debug.Log("swordCollider not set");
        }
    }
}
