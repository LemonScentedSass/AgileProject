using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip footstep1;
    public AudioClip footstep2;

    private AudioManager _am;

    private void Start()
    {
        _am = AudioManager.instance;
    }

    public void PlayFootstepOne()
    {
        _am.PlaySFX(footstep1, 0.3f);
    }

    public void PlayFootstepTwo()
    {
        _am.PlaySFX(footstep2, 0.3f);
    }    
}
