using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FootIkRootSolver : MonoBehaviour
{
    [SerializeField] private Transform characterRoot;

    private List<float> heightOffsets = new List<float>();

    [SerializeField] private float readjustmentThreshold;

    private Vector3 rootTarget;

    private void OnAnimatorMove()
    {
        //characterRoot.Translate(Vector3.up * heightOffset);

        float minimumOffset = heightOffsets.Min();
        if(minimumOffset > readjustmentThreshold)
        {
            rootTarget = characterRoot.position + characterRoot.up * minimumOffset;
        }
        else
        {
            rootTarget = characterRoot.position;
        }
    }

    public void UpdateTargetOffset(float heightValue)
    {
        heightOffsets.Add(heightValue);
    }

    public Vector3 RootTarget => rootTarget;
}
