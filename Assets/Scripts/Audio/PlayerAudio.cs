using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip footstep1;
    public AudioClip footstep2;

    private InputHandlerFirstPerson input;

    private AudioManager _am;

    private void Start()
    {
        _am = AudioManager.instance;
        input = GetComponentInParent<InputHandlerFirstPerson>();
    }

    public void PlayFootstepOne()
    {
        if (input.inputVector.x != 0 || input.inputVector.y != 0)
            _am.PlaySFX(footstep1, 0.3f);
    }

    public void PlayFootstepTwo()
    {
        if (input.inputVector.x != 0 || input.inputVector.y != 0)
            _am.PlaySFX(footstep2, 0.3f);
    }    
}
