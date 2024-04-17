using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionApplier : MonoBehaviour
{
    private void OnAnimatorMove()
    {
        //This function is called by the Animator Controller each frame to apply root motion, have to be careful when the character stops the rootmotion must stop too.
        Animator anim = GetComponent<Animator>();
        float motionMagnitude = anim.GetFloat("MotionRoot");
        //transform.position += transform.forward * motionMagnitude;
        
        transform.Translate(Vector3.forward * motionMagnitude, Space.Self);
        
    }
}
