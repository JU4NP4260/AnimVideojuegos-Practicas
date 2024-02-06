using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class AnimationControllerJP : MonoBehaviour
{
    enum MotionStates
    {
        lockedIn,
        freelook
    }

    private Animator animator;
    private Vector2 currentInput;
    private Vector2 nextInput;
    private Vector2 inputVelocity;
    private MotionStates motionState;

    private int motionXID, motionYID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        motionXID = Animator.StringToHash("MoveX");
        motionYID = Animator.StringToHash("MoveY");
    }

    private void Update()
    {
        if (animator != null)
        {
            currentInput = Vector2.SmoothDamp(currentInput, nextInput, ref inputVelocity, 0.5f);
            if(motionState == MotionStates.freelook)
            {

            }
            animator.SetFloat(motionXID, currentInput.x);
            animator.SetFloat(motionYID, currentInput.y);
        }
    }

    public void Move(CallbackContext context)
    {

        Vector2 motionValue = context.ReadValue<Vector2>();
        Debug.Log(motionValue);
        nextInput = motionValue;
    }

}
