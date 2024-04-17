using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementChar : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private int motionXId, motionYId;
    //private Vector2 inputVelocity;
    private Vector3 projectedVector;
    private Quaternion currentRotation;
    //private Vector2 nextInput, currentInput;

    [SerializeField] float MoveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        motionXId = Animator.StringToHash("MotionX");
        motionYId = Animator.StringToHash("MotionY");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //currentInput = Vector2.SmoothDamp(currentInput, nextInput, ref inputVelocity, 0.5f);

        Transform cameraTransform = Camera.main.transform;
        Vector3 cameraForward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up,
            Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)));

        Vector3 cameraRight = cameraTransform.right;

        projectedVector = Vector3.ProjectOnPlane(cameraForward, transform.up).normalized /** verticalInput + cameraRight * horizontalInput*/;
        projectedVector = projectedVector.normalized;

        currentRotation = Quaternion.LookRotation(projectedVector, transform.up);

        transform.rotation = currentRotation;

        animator.SetFloat("MotionX", horizontalInput);
        animator.SetFloat("MotionY", verticalInput);
    }
}
