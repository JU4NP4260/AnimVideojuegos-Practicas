using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickUpController))]
public class PickUpControllerEditor : Editor
{
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      PickUpController tg = (PickUpController)target;
      if (GUILayout.Button("Update Quadrants"))
      {
        // tg.UpdateQuadrantData();
      }
   }
   
   [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(PickUpController))]
   public static void DrawGizmos(Component component, GizmoType gizmoType)
   {
      string[] quadrantNames = new []{"Upper Left", "Upper Right", "Lower Left", "Lower Right"};  

      PickUpController tg = (PickUpController)component;
      Gizmos.matrix = tg.TargetReference.localToWorldMatrix;
      Handles.matrix = tg.TargetReference.localToWorldMatrix;
      Gizmos.color = Color.cyan;
      Gizmos.DrawLine(Vector3.zero, Vector3.forward * tg.MaxReachingDistance);   
      
      Gizmos.color = Color.red;
      for(int i=0; i<tg.Quadrants.Count; i++)
      {
         Vector3 localDirection = tg.Quadrants[i].localDirection;
         Gizmos.DrawLine(Vector3.zero, localDirection);
         Handles.Label(localDirection,quadrantNames[i]);
      }
   }
}