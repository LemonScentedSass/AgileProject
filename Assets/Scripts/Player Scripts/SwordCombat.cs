using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCombat : MonoBehaviour
{
    private Animator anim;
    private InputHandler input;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedFrame = 0;
    float maxComboDelay = 1;


    void Start()
    {
        anim = GetComponent<Animator>();
        input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.swordKey)
        {
            lastClickedFrame = Time.time;
            noOfClicks++;
            if(noOfClicks == 1)
            {

            }
        }
    }
}
