using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonShooter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerState = ThirdPersonShooter.ThirdPersonShooterPlayerData.PlayerState;


public class AimController : ThirdPersonShooterPlayerScript
{
    [SerializeField] Transform cameraRotation;
    [SerializeField] float lookSpeed = 30;
    [SerializeField] CinemachineVirtualCamera aimingCamera;
    [SerializeField] Transform aimTarget;


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (cameraRotation != null) return;

        cameraRotation.RotateAround(transform.position, transform.up, inputValue.x);
        Vector3 rightOffset = aimingCamera.transform.right * inputValue.x;
        Vector3 upOffset = aimingCamera.transform.up * inputValue.y;
        Vector3 planeNormal = aimingCamera.transform.forward * 8;
        aimTarget.position = Vector3.ProjectOnPlane(aimTarget.position + rightOffset + upOffset, planeNormal);
    }


    public void ToggleAim(InputAction.CallbackContext context)
    {
        
        float inputValue = context.ReadValue<float>();
        animator.SetBool("ToggleAim", inputValue > 0);
        aimingCamera.gameObject.SetActive(inputValue > 0);

        //playerData.State = inputValue > 0
        //    ? PlayerState.AimingMode
        //    : PlayerState.NormalMode;
    }

    //protected override void OnStateChanged(PlayerState state)
    //{
    //    animator.SetBool("ToggleAim", state == PlayerState.AimingMode);
    //}
}
