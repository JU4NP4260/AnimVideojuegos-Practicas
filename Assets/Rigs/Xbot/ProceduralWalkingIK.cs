using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProceduralWalkingIK : MonoBehaviour
{
    [SerializeField] private Transform detectionReference;
    [SerializeField] private Transform foot;
    [SerializeField][Range(0,1)] private float detectionRange;

    public Vector3 GetDetectionStartPosition()
    {
        Vector3 referenceSpacePosition = detectionReference.InverseTransformPoint(foot.position);
        Vector3 ret = new Vector3(referenceSpacePosition.x, referenceSpacePosition.y * detectionRange, referenceSpacePosition.z);

        return detectionReference.TransformPoint(ret);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
