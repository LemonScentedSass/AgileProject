using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSwordCombat : MonoBehaviour
{
    private Animator anim;
    private InputHandler input;

    public bool canAttack;

    [Range(0f, 1f)] public float comboAdvanceStart = .5f;
    [Range(0f, 1f)] public float comboAdvanceEnd = 1f;

    public bool comboAdvance;
    public bool comboEnded;

    public float cooldownTime = 1f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        input = GetComponent<InputHandler>();
    }
}
