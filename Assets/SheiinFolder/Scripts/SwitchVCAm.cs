using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCAm : MonoBehaviour
{
    [SerializeField] private PlayerInput pInput;
    [SerializeField] private int priorityBoost = 10;

    private InputAction aimAction;
    private CinemachineVirtualCamera vCamera;

    private void Awake()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = pInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void StartAim()
    {
        vCamera.Priority += priorityBoost;
    }

    private void CancelAim()
    {
        vCamera.Priority -= priorityBoost;
    }
}
