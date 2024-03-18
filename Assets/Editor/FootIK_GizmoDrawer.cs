using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class FootIK_GizmoDrawer
{

    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected, typeof(ProceduralWalkingIK))]
    public static void DrawGizmos(Component componet, GizmoType gismoType)
    {
        ProceduralWalkingIK target = componet as ProceduralWalkingIK;
        if(target == null) return; 
        Gizmos.color = target.HasTarget ? Color.red : Color.green;

        Vector3 detecStartPos = target.GetDetectionStartPosition();

        Gizmos.DrawSphere(detecStartPos, 0.05f);
        Handles.Label(detecStartPos, "Punto de detección");
        Gizmos.DrawLine(detecStartPos, detecStartPos - target.DetectionReference.up * target.MaxDetectionDistance);
    }

    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected, typeof(FootIkRootSolver))]
    public static void DrawGizmoForRoot(Component component, GizmoType gizmoType)
    {
        FootIkRootSolver target = component as FootIkRootSolver;
        if(target == null ) return;

        Handles.DrawWireDisc(target.transform.position, target.transform.up, 0.7f);
        Handles.color = new Color(0, 1, 1, 0.5f);
        Handles.DrawWireDisc(target.RootTarget, target.transform.up, 0.7f);

    }

}
