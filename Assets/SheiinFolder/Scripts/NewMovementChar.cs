using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class NewMovementChar : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private int motionXId, motionYId;
    private Vector2 inputVelocity;
    private Vector3 projectedVector;
    private Quaternion currentRotation;
    private Quaternion lastRotation;
    private Vector2 currentInput = new Vector2(0, 0);

    [SerializeField] float MoveSpeed = 10;

    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField] private float smoothInputSpeed = 0.3f;

    private bool targetLocked = false;
    [SerializeField] private GameObject targetGameObject;

    [SerializeField] private GameObject virtualCamera1, virtualCamera2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        motionXId = Animator.StringToHash("MotionX");
        motionYId = Animator.StringToHash("MotionY");

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void MovePlayer_Locked()
    {
        Vector2 inputMove = moveAction.ReadValue<Vector2>();
        currentInput = Vector2.SmoothDamp(currentInput, inputMove, ref inputVelocity, smoothInputSpeed);

        animator.SetFloat("MotionX", currentInput.x);
        animator.SetFloat("MotionY", currentInput.y);
    }

    private void MovePlayer_Free()
    {
        Vector2 inputMove = moveAction.ReadValue<Vector2>();
        currentInput = Vector2.SmoothDamp(currentInput, inputMove, ref inputVelocity, smoothInputSpeed);

        animator.SetFloat("MotionY", currentInput.y * 2);
        animator.SetFloat("MotionX", 0);        
    }

    void Update()
    {
        if (targetLocked)
        {
            MovePlayer_Locked();
            gameObject.transform.LookAt(targetGameObject.transform.position);
            
            virtualCamera1.SetActive(false);
            virtualCamera2.SetActive(true);

        }
        else
        {
            MovePlayer_Free();

            Transform cameraTransform = Camera.main.transform;
            Vector3 cameraForward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up,
                Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)));

            projectedVector = Vector3.ProjectOnPlane(cameraForward, transform.up).normalized /** verticalInput + cameraRight * horizontalInput*/;
            projectedVector = projectedVector.normalized;

            currentRotation = Quaternion.LookRotation(projectedVector, transform.up);

            if(currentInput.y > 0.2f)
            {
                
                gameObject.transform.rotation = Quaternion.Slerp(lastRotation, currentRotation, 2);
            }
            else
            {
                lastRotation = gameObject.transform.rotation;
            }
        }

        LockOnTarget(targetLocked);
        
        
        //currentInput = Vector2.SmoothDamp(currentInput, nextInput, ref inputVelocity, 0.5f);

        
    }

    private void LockOnTarget(bool logic)
    {
        if(logic)
        {
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Locking on target");

                animator.SetBool("TargetLocked", false);
                targetLocked = false;
            }
        }
        else if(!logic)
        {
            Debug.Log("is Locked on? " + logic);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Freeing camera");

                animator.SetBool("TargetLocked", true);
                targetLocked = true;
            }
        }
    }

}
