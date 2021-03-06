﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveGizmo : MonoBehaviour
{
    [SerializeField] float t;
    [SerializeField] Transform[] controlPoints;

    private void OnDrawGizmos()
    {
        for(t = 0; t <= 1; t += .05f)
        {
            Vector2 gizmoPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmoPosition, .25f);
        }
    }
}
