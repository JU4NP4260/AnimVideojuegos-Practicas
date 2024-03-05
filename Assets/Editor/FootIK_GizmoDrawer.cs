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
        Gizmos.DrawSphere(target.GetDetectionStartPosition(), 0.05f);
    }

}
