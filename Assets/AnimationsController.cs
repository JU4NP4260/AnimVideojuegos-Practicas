using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
public class AnimationsController : MonoBehaviour
{
    enum MotionState
    {
        NotInCombat,
        InCombat
    }
    private Vector2 currentInput;
    private Vector2 nextInput;
    private Vector2 inputVelocity;
    private Animator animator;
    private int motionXId, motionYId, rootMotionId;
    private MotionState motionState = MotionState.NotInCombat;
    private Vector3 proyectedVector;

    private Quaternion desiredRotation;
    private Quaternion currentRotation;
    private float rotationSpeed;

    private bool moving;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        motionXId = Animator.StringToHash("MotionX");
        motionYId = Animator.StringToHash("MotionY");
        rootMotionId = Animator.StringToHash("MotionRoot");
    }

    public void Move(CallbackContext context)
    {
        if (context.canceled)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        Vector2 motionValue = context.ReadValue<Vector2>();
        Debug.Log(motionValue);
        nextInput = motionValue;
  
    }

    private void Update()
    {
        currentInput = Vector2.SmoothDamp(currentInput, nextInput, ref inputVelocity, 0.5f);
        if (motionState == MotionState.NotInCombat)
        {
            //Calcular Direccion de movimiento
            Transform cameraTransform = Camera.main.transform;
            Vector3 cameraForward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up,
                Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)));
            
            Vector3 cameraRight = cameraTransform.right;
            proyectedVector = Vector3.ProjectOnPlane(cameraForward, transform.up).normalized * currentInput.y + cameraRight * currentInput.x;
            proyectedVector = proyectedVector.normalized;
            //Rotar en la Direccion de movimiento
            currentRotation = Quaternion.LookRotation(proyectedVector, transform.up);

            transform.rotation = currentRotation;
            //Setear la animacion
            
            
            animator.SetFloat(motionYId, proyectedVector.magnitude);
            
            animator.SetFloat(rootMotionId, 0.05f);
   

        }

        
        //Si no se esta moviendo, limitar la cantidad que se resta el input
        if (!moving)
        {
            nextInput = Vector2.zero;
            nextInput.Set(Mathf.Clamp(nextInput.x, -0.1f,0.1f),Mathf.Clamp(nextInput.y,-0.1f,0.1f));
            animator.SetFloat(rootMotionId, 0);
        }

    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position + proyectedVector);
    }
#endif
}
