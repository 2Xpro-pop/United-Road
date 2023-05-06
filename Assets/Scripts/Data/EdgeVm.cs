using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EdgeVm
{
    public string name;
    public Point[] points;
}

[Serializable]
public class Point
{
    public float x;
    public float y;
}
