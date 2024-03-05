using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProceduralWalkingIK : MonoBehaviour
{
    [SerializeField] private Transform detectionReference;
    [SerializeField] private Transform foot;
    [SerializeField][Range(0,1)] private float detectionRange;
    [SerializeField] private float maxDetectionDistance;

    private bool hasTarget;
    private RaycastHit ikTarget;

    public Transform DetectionReference { get => detectionReference; set => detectionReference = value; }
    public float MaxDetectionDistance { get => maxDetectionDistance; set => maxDetectionDistance = value; }
    public bool HasTarget { get => hasTarget; set => hasTarget = value; }

    public Vector3 GetDetectionStartPosition()
    {
        Vector3 referenceSpacePosition = detectionReference.InverseTransformPoint(foot.position);
        Vector3 ret = new Vector3(referenceSpacePosition.x, referenceSpacePosition.y * detectionRange, referenceSpacePosition.z);
        return detectionReference.TransformPoint(ret);
    }

    private void GetTargetPosition()
    {
        Physics.Raycast(GetDetectionStartPosition(), -detectionReference.up, out ikTarget, maxDetectionDistance);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        GetTargetPosition();
    }

}
