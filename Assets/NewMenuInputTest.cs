using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMenuInputTest : MonoBehaviour
{
    public Animator HandsAnimator;
    public Animator MenuBookAnimator;

    public bool Mirrored;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mirrored == true)
        {
            HandsAnimator.SetBool("Mirrored", true);
        }
        else
        {
            HandsAnimator.SetBool("Mirrored", false);
        }

        if(Input.GetKeyDown(KeyCode.Tab) == true)
        {
            MenuBookAnimator.Play("BookOpening");
            HandsAnimator.Play("OpenBookMenu", 2);
        }
        if(Mirrored == false && Input.GetKeyDown(KeyCode.Escape) == true)
        {
            MenuBookAnimator.Play("New State");
            HandsAnimator.Play("LeftArmMagicIdle", 2);
        }

        if(Mirrored == false && Input.GetKeyDown(KeyCode.M))
        {
            Mirrored = true;
        }
        else if( Mirrored == true == Input.GetKeyDown(KeyCode.M))
        {
            Mirrored = false;
        }

        if(Mirrored == false && Input.GetMouseButtonDown(1) == true)
        {
            HandsAnimator.Play("LeftArmMagicShoot", 2);
        }
        else if(Mirrored == true && Input.GetMouseButtonDown(0)==true)
        {
            HandsAnimator.Play("RightArmMagicShoot", 1);
        }


        //Right Hand weapon swings on click
        if(Input.GetMouseButtonDown(0) == true && Mirrored == false)
        {
            //Checks to see if the player is in idle; Plays attack 1
            if(HandsAnimator.GetCurrentAnimatorStateInfo(1).IsName("RightArmWeaponLeftNothing-Idle"))
            {
                HandsAnimator.Play("RightArmWeaponLeftNothing-Attack1", 1);
            }

            //Checks to see if player is in attack1 then activates next attack
            if (HandsAnimator.GetCurrentAnimatorStateInfo(1).IsName("RightArmWeaponLeftNothing-Attack1"))
            {
                HandsAnimator.SetBool("isAttacking", true);

                //Resets the bool that transitioned the first attack to the second
                StartCoroutine(AttackWait(.5f));
            }

            //Checks to see if in attack2; Set bool to start final hit
            if (HandsAnimator.GetCurrentAnimatorStateInfo(1).IsName("RightArmWeaponLeftNothing-Attack2"))
            {
                HandsAnimator.SetBool("Final", true);
            }
        }

        //Being in idle and attacking again makes final hit bool reset
        if (HandsAnimator.GetCurrentAnimatorStateInfo(1).IsName("RightArmWeaponLeftNothing-Idle"))
        {
            HandsAnimator.SetBool("Final", false);
        }

        //Mirrored input
        if (Input.GetMouseButtonDown(1) == true && Mirrored == true)
        {
            //Checks to see if the player is in idle; Plays attack 1
            if (HandsAnimator.GetCurrentAnimatorStateInfo(2).IsName("LeftArmWeaponRightNothing-Idle"))
            {
                HandsAnimator.Play("LeftArmWeaponRightNothing-Attack1", 2);
            }

            //Checks to see if player is in attack1 then activates next attack
            if (HandsAnimator.GetCurrentAnimatorStateInfo(2).IsName("LeftArmWeaponRightNothing-Attack1"))
            {
                HandsAnimator.SetBool("isAttacking", true);

                //Resets the bool that transitioned the first attack to the second
                StartCoroutine(AttackWait(.5f));
            }

            //Checks to see if in attack2; Set bool to start final hit
            if (HandsAnimator.GetCurrentAnimatorStateInfo(2).IsName("LeftArmWeaponRightNothing-Attack2"))
            {
                HandsAnimator.SetBool("Final", true);
            }

        }

        //Being in idle and attacking again makes final hit bool reset
        if (HandsAnimator.GetCurrentAnimatorStateInfo(2).IsName("LeftArmWeaponRightNothing-Attack3"))
        {
            HandsAnimator.SetBool("Final", false);
        }

    }

    //Time waited before turning off isAttacking bool in HandAnimator
    IEnumerator AttackWait(float timewait)
    {
        yield return new WaitForSeconds(0.5f);
        if(Input.GetMouseButtonDown(0) == false)
        {
            HandsAnimator.SetBool("isAttacking", false);
        }

    }

}
