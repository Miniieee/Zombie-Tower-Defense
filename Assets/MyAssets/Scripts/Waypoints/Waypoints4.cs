﻿using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class Waypoints4 : MonoBehaviour
{
    public static Transform[] points;
    public void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
