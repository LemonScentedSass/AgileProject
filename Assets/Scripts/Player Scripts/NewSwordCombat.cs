using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSwordCombat : MonoBehaviour
{
    private Animator anim;
    private InputHandler input;

    //public bool canAttack;
    //public bool comboAdvance;
    //public bool comboEnded;

    [Range(0f, .99f)] public float comboAdvancementWindowStart = .5f;
    [Range(0f, .99f)] public float comboAdvancementWindowEnd = 1f;
    public float cooldownTime = 1f;
    float tempCooldownTime;

    // Animator Bools:
    // canAttack, isAttcking, comboAdvance, comboBreak, endCombo playerClick

    // Animator Floats:
    // curAnimTime;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        input = GetComponent<InputHandler>();

        anim.SetBool("canAttack", true);
        anim.SetFloat("cooldown", cooldownTime);

        if (comboAdvancementWindowStart > comboAdvancementWindowEnd)
        {
            anim.SetBool("canAttack", false);
            Debug.Log("Fuckin dummy, you filthy rat. You thought you could get away with it, didn't you?" +
                "You really thought I wouldn't put a check in that makes sure the start is lower than the end" +
                "of the advancement window. Fuck you + ligma");
        }
    }

    

    private void Update()
    {
        if (input.swordKey)
        {
            anim.SetBool("playerClick", true);
        }
        else
        {
            anim.SetBool("playerClick", false);
        }

        if (anim.GetBool("isAttacking"))
        {
            anim.SetFloat("curAnimTime", anim.GetCurrentAnimatorStateInfo(1).normalizedTime);
            tempCooldownTime = cooldownTime;
            anim.SetFloat("cooldown", cooldownTime);


            // If outside the combo advancement window
            if (anim.GetFloat("curAnimTime") < comboAdvancementWindowStart ||
                anim.GetFloat("curAnimTime") >= comboAdvancementWindowEnd)
            {
                anim.SetBool("comboWindow", false);
            }
            // If inside the combo advancement window
            else if (anim.GetFloat("curAnimTime") >= comboAdvancementWindowStart &&
                anim.GetFloat("curAnimTime") < comboAdvancementWindowEnd)
            {
                anim.SetBool("comboWindow", true);
            }            
        }

        

        // If the player can't attack, make it able to after the cooldown elapses.
        if (!anim.GetBool("canAttack"))
        {
            tempCooldownTime = cooldownTime;
            anim.SetFloat("cooldown", tempCooldownTime -= Time.deltaTime);
            if (anim.GetFloat("cooldown") <= 0.01f)
            {
                anim.SetBool("canAttack", true);
            }
        }
    }
}
