using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
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

    //Rotation
    private Quaternion desiredRotation;
    private Quaternion currentRotation;
    private float rotationSpeed;

    private bool moving;

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
                Transform cameraTransform = Camera.main.transform;
                Vector3 camFoward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up, Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)));

                Vector3 cameraRight = cameraTransform.right;
                Vector3 projectionVector = Vector3.ProjectOnPlane(camFoward, transform.up).normalized * currentInput.y + cameraRight * currentInput.x;
                projectionVector = projectionVector.normalized;


                currentRotation = Quaternion.LookRotation(projectionVector, transform.up);

                transform.rotation = currentRotation;


                //Set animation
                animator.SetFloat(motionYID, projectionVector.magnitude);               
            }

            if (!moving)
            {
                nextInput = Vector2.zero;
                nextInput.Set(Mathf.Clamp(nextInput.x, 0.1f, 0.1f), Mathf.Clamp(nextInput.y, 0.1f, 0.1f));
            }
        }
    }

    public void Move(CallbackContext context)
    {
        if (context.performed)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        Vector2 motionValue = context.ReadValue<Vector2>();
        Debug.Log(motionValue);
        nextInput = motionValue;

        
    }

}
