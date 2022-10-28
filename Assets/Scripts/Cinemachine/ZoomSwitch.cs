using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSwitch : MonoBehaviour
{
    private Animator anim;
    private bool zoomed = true;
    public Camera miniMapCam;
    public float zoomedInMinimapCamSize = 15f;
    public float zoomedOutMinimapCamSize = 30f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (miniMapCam != null)
        {
            miniMapCam.GetComponent<Camera>().orthographicSize = zoomedInMinimapCamSize;
        }        
    }

    public void SwitchState()
    {
        if (zoomed)
        {
            anim.Play("Zoomed Out");
            miniMapCam.GetComponent<Camera>().orthographicSize = zoomedOutMinimapCamSize;
        }

        else
        {
            anim.Play("Zoomed In");
            miniMapCam.GetComponent<Camera>().orthographicSize = zoomedInMinimapCamSize;
        }

        zoomed = !zoomed;
    }
}

