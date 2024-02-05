using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class AnimationControllerJP : MonoBehaviour
{

    public void Move(CallbackContext context)
    {
        Vector2 motionValue = context.ReadValue<Vector2>();
        Debug.Log(motionValue);
    }

}
