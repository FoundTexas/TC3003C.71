using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformaciones
{
    public static Matrix4x4 translate(float x,float y,float z)
    {
        Matrix4x4 t = Matrix4x4.identity;
        t[0, 3] = x;
        t[1, 3] = y;
        t[2, 3] = z;
        return t;
    }
    public static Matrix4x4 scale(float x, float y, float z)
    {
        Matrix4x4 t = Matrix4x4.identity;
        t[0, 0] = x;
        t[1, 1] = y;
        t[2, 2] = z;
        return t;
    }
    public static Matrix4x4 RotateX(float a)
    {
        float rad = Mathf.Deg2Rad * a;
        Matrix4x4 t = Matrix4x4.identity;
        t[1, 1] = Mathf.Cos(rad);
        t[1, 2] = -Mathf.Sin(rad);
        t[2, 1] = Mathf.Sin(rad);
        t[2, 2] = Mathf.Cos(rad);
        return t;
    }
    public static Matrix4x4 RotateY(float a)
    {
        float rad = Mathf.Deg2Rad * a;
        Matrix4x4 t = Matrix4x4.identity;
        t[0, 0] = Mathf.Cos(rad);
        t[0, 2] = Mathf.Sin(rad);
        t[2, 0] = -Mathf.Sin(rad);
        t[2, 2] = Mathf.Cos(rad);
        return t;
    }
    public static Matrix4x4 RotateZ(float a)
    {
        float rad = Mathf.Deg2Rad * a;
        Matrix4x4 t = Matrix4x4.identity;
        t[0, 0] = Mathf.Cos(rad);
        t[0, 1] = -Mathf.Sin(rad);
        t[1, 0] = Mathf.Sin(rad);
        t[1, 1] = Mathf.Cos(rad);
        return t;
    }
}