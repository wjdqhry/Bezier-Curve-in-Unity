using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform player;
    public int pointNum = 50;
    public Transform p0;
    public Transform p1;
    public Transform p2;

    private Vector3[] points = new Vector3[50];
    private float extendStep = 5f;

    void Start()
    {
        lineRenderer.positionCount = pointNum;
    }

    void Update()
    {
        if (Pos_Init())
        {
            DrawCurve();
            lineRenderer.enabled = true;
        }
        else
            lineRenderer.enabled = false;
    }
    
    private void DrawCurve()
    {
        for (int i = 1; i <= pointNum; i++)
        {
            float t = i / (float)pointNum;
            points[i - 1] = CalBezierPoint(t, p0.position, p1.position, p2.position);
        }
            lineRenderer.SetPositions(points);
    }

    bool Pos_Init()
    {
        RaycastHit hit;
        Debug.DrawRay(player.transform.position, (player.transform.forward - player.transform.up).normalized, Color.green);
        if (Physics.Raycast(player.transform.position, (player.transform.forward - player.transform.up).normalized, out hit, 30.0F))
        {
            if (hit.collider.tag == "Floor")
            {
                p0.position = player.position;
                p2.position = hit.point;
                p1.position = (p2.position / 2) + transform.up * (hit.distance/2);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    
    private Vector3 CalBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
