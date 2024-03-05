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

}
