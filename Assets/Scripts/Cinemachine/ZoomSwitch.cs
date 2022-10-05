using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSwitch : MonoBehaviour
{
    private Animator anim;
    private bool zoomed = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if (zoomed)
            anim.Play("Zoomed Out");
        else
            anim.Play("Zoomed In");

        zoomed = !zoomed;
    }
}

