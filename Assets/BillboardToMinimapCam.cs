using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToMinimapCam : MonoBehaviour
{
    public Transform minimapCam;

    private void LateUpdate()
    {
        if (minimapCam != null)
        {
            transform.forward = minimapCam.forward;
        }
    }
}
