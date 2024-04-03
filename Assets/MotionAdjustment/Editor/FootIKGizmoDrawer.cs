using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class FootIKGizmoDrawer
{
   [DrawGizmo(GizmoType.Active | GizmoType.NonSelected, typeof(FootIK))]
   public static void DrawGizmosForFeet(Component component, GizmoType gizmoType)
   {
      FootIK target = component as FootIK;
      if (target == null) return;
      Gizmos.color = target.HasTarget ? Color.green : Color.red;
      Vector3 detectionStartPosition = target.GetDetectionStartPosition();
      Gizmos.DrawSphere(detectionStartPosition, 0.05f);
      Handles.Label(detectionStartPosition,"Punto de deteccion");
      Gizmos.DrawLine(detectionStartPosition,detectionStartPosition - target.DetectionReference.up * target.MaxDetectionDistance);
   }
   [DrawGizmo(GizmoType.Active | GizmoType.NonSelected, typeof(FootIKRootSolver))]
   public static void DrawGizmosForRoot(Component component, GizmoType gizmoType)
   {
      FootIKRootSolver target = component as FootIKRootSolver;
      if (target == null) return;
      
      Handles.DrawWireDisc(target.transform.position,target.transform.up,0.7f);
      Handles.color = new Color(0, 1, 1, 0.5f);
      Handles.DrawWireDisc(target.RootTarget,target.transform.up,0.7f);
   }
}
