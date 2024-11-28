using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLight : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Animator lightAnimator;

    public void Activate() {
        muzzleFlash.Play();
        lightAnimator.Play("LightFlash", -1, 0f);
        
    }
}
