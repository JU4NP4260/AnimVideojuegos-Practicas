using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalCombatSystemPlayerState : MonoBehaviour
{
    [SerializeField] public float maxBaseStamina;

    [SerializeField] public float currentStamina;

    public FloatEvent onStaminaModified;

    public void ModifyStamina(float modifyValue)
    {
        //if (currentStamina < maxBaseStamina)
        //{
            currentStamina += modifyValue;
            onStaminaModified?.Invoke(currentStamina / maxBaseStamina);
        //}
        
    }
    
    private void Awake()
    {
        ModifyStamina(maxBaseStamina);
    }

    public float CurrentStamina => currentStamina;
}
