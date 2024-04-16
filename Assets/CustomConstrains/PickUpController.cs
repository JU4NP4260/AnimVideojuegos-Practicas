using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PickUpController : MonoBehaviour
{
    public struct QuadrantData
    {
        public Vector3 localDirection;
        public Vector2 animationDirection;
        public int handIdex;
    }
    
    [SerializeField] private float maxReachingDistance;
    [SerializeField]float perQuadrantAngle;
    [SerializeField] private Transform targetReference;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform[] ikHands;
    [SerializeField] private TwoBoneIKConstraint[] hands;
    
    public List<Transform> availableItems = new List<Transform>();
    private List<QuadrantData> quadrants = new List<QuadrantData>();
    private int ikHandsIndex = 0;

    private void UpdateQuadrantData()
    {
        quadrants = new List<QuadrantData>()
        {
            new QuadrantData()
            {
                //Upper Left
                localDirection = Quaternion.Euler(-perQuadrantAngle*0.5f, -perQuadrantAngle*0.5f, 0) * Vector3.forward * maxReachingDistance,
                animationDirection = new Vector2(-1,1),
                handIdex = 0
                
            },
            new QuadrantData()
            {
                //Upper Right
                localDirection = Quaternion.Euler(-perQuadrantAngle*0.5f, perQuadrantAngle*0.5f, 0) * Vector3.forward * maxReachingDistance,
                animationDirection = new Vector2(1,1),
                handIdex = 1
            },
            new QuadrantData()
            {
                //Lower Left
                localDirection = Quaternion.Euler(perQuadrantAngle*0.5f, -perQuadrantAngle*0.5f, 0) * Vector3.forward * maxReachingDistance,
                animationDirection = new Vector2(-1,-1),
                handIdex = 0
            },
            new QuadrantData()
            {
                //Lower Right
                localDirection = Quaternion.Euler(perQuadrantAngle*0.5f, perQuadrantAngle*0.5f, 0) * Vector3.forward * maxReachingDistance,
                animationDirection = new Vector2(1,-1),
                handIdex = 1
            }
        };
    }

    private void SetUpIkConstraint(int id,Vector3 pickUpPosition)
    {
        ikHands[id].position = pickUpPosition;
        ikHandsIndex = id;
    }
    
    public void PickUpNearestObject()
    {
        Transform nearestItem =
            availableItems.OrderBy(item =>
            {
                Vector3 itemDir = targetReference.position - item.position;
                float sqrMagnitude = Vector3.SqrMagnitude(itemDir);
                float dot = Mathf.Abs(Vector3.Dot(itemDir.normalized, TargetReference.forward));
                return sqrMagnitude * dot;
            }).FirstOrDefault();
        if(nearestItem == default) return;
        
        Vector3 localItemPosition = targetReference.InverseTransformPoint(nearestItem.position);
        int nearestQuadrantId = 0;
        Vector3 normalizedLocalItemPosition = localItemPosition.normalized;
        for (int i = 0; i < quadrants.Count; i++)
        {
            Vector3 currentNearest = quadrants[nearestQuadrantId].localDirection;
            float dot = Vector3.Dot(currentNearest, normalizedLocalItemPosition);
            if (Vector3.Dot(normalizedLocalItemPosition, quadrants[i].localDirection) < dot)
            {
                nearestQuadrantId = i;
            }
        }
        QuadrantData quadrant = quadrants[nearestQuadrantId];
        anim.SetFloat("PuckupX",quadrant.animationDirection.x);
        anim.SetFloat("PuckupY",quadrant.animationDirection.y);
        anim.SetTrigger("PickUp");
        SetUpIkConstraint(quadrant.handIdex,nearestItem.position);
    }
    private void OnValidate()
    {
        UpdateQuadrantData();
    }

    private void Update()
    {
        hands[ikHandsIndex].weight = anim.GetFloat("IkPickupWeight2");
    }

    public Transform TargetReference => targetReference == null ? transform : targetReference;
    public float MaxReachingDistance => maxReachingDistance;
    
    public List<QuadrantData> Quadrants => quadrants;
}
