using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(AimConstraint))]
public class AimConstraintAnimationApplier : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private AimConstraint constraint;
    private int aimWeightID;
    private void Awake()
    {
        constraint = GetComponent<AimConstraint>();
        aimWeightID = Animator.StringToHash("AimWeight");
    }

    private void LateUpdate()
    {
        constraint.weight = animator.GetFloat(aimWeightID);
    }
}
