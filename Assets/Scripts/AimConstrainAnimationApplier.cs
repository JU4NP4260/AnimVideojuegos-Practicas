using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[RequireComponent(typeof(AimConstraint))]
public class AimConstrainAnimationApplier : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AimConstraint constraint;

    private int aimWeight;
    private void Awake()
    {
        constraint = GetComponent<AimConstraint>();
        aimWeight = Animator.StringToHash("AimWeight");
    }

    private void LateUpdate()
    {
        constraint.weight = animator.GetFloat(aimWeight);
    }
}
