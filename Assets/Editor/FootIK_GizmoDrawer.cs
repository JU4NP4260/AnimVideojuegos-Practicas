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
        Gizmos.DrawSphere(componet.transform.position, 1);
    }
}
