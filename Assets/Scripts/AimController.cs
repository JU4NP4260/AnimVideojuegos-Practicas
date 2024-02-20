using System.Collections;
using System.Collections.Generic;
using ThirdPersonShooter;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimController : ThirdPersonShooterPlayerScript
{
    [SerializeField] Transform cameraRotation;

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (cameraRotation != null) return;

        cameraRotation.RotateAround(transform.position, transform.up, inputValue.x);
    }

}
