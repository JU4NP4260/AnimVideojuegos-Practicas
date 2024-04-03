using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FootIKRootSolver : MonoBehaviour
{
    [SerializeField] private Transform characterRoot;
    [SerializeField] private float readjustmentThreshold;
    [SerializeField] private float readjustmentSpeed = 15f;
    [SerializeField] private Rigidbody rigidbody;
    private List<float> heightOffsets = new List<float>();
    private Vector3 rootTarget;
    public Vector3 currentRootPosition;
    private void OnAnimatorMove()
    {
        //characterRoot.Translate(Vector3.up * heightOffSet);
        if (heightOffsets.Count >= 2)
        {
            float minimumOffset = Mathf.Min(heightOffsets[0], heightOffsets[1]);
            if (minimumOffset > readjustmentThreshold)
            {
                rootTarget = characterRoot.TransformPoint(new Vector3(0, minimumOffset, 0));
                rigidbody.isKinematic = true;
            }
            else
            {
                rigidbody.isKinematic = false;
                rootTarget = characterRoot.position;
            }
        }
        else
        {
            rigidbody.isKinematic = false;
            rootTarget = characterRoot.position;
        }

        currentRootPosition = Vector3.Lerp(currentRootPosition, rootTarget, Time.deltaTime * readjustmentSpeed);
            characterRoot.position = currentRootPosition;
            heightOffsets.Clear();
        
    }

    public void UpdateTargetOffset(float heightValue)
    {
        heightOffsets.Add(heightValue);
    }

    public Vector3 RootTarget => rootTarget;
}
