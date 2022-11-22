using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pieza
{
    public Color color;
    public string Name;
    GameObject go_parts;
    public Vector3 l;

    public Vector3 moveAmount;
    Vector3 curPos;
    public Vector3 posDelta;

    public Vector3 delta;
    public Vector3 rot = Vector3.zero;
    public Vector3 max, min;

    public Vector3 s;
    Matrix4x4 m_locations;
    Matrix4x4 m_rotations;
    Matrix4x4 m_scales;
    Vector3[] v3_originales;

    public Pieza[] next;

    public void Set()
    {
        curPos = l;
        go_parts = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go_parts.name = Name;
        v3_originales = go_parts.GetComponent<MeshFilter>().mesh.vertices;
        m_locations = Transformaciones.translate(curPos.x, curPos.y, curPos.z);
        m_rotations = Transformaciones.RotateX(rot.x) * Transformaciones.RotateY(rot.y) * Transformaciones.RotateZ(rot.z);
        m_scales = Transformaciones.scale(s.x, s.y, s.z);

        go_parts.GetComponent<MeshRenderer>().material.color = color;
        foreach (var piezas in next)
        {
            piezas.Set();
        }

    }

    public void SetPieza()
    {
        rot.x += delta.x;
        rot.y += delta.y;
        rot.z += delta.z;
        if (rot.x <= min.x || rot.x >= max.x) delta.x = -delta.x;
        if (rot.y <= min.y || rot.y >= max.y) delta.y = -delta.y;
        if (rot.z <= min.z || rot.z >= max.z) delta.z = -delta.z;

        if (posDelta.x + posDelta.y + posDelta.z != 0)
        {
            if (curPos.x <= -moveAmount.x || curPos.x >= moveAmount.x) posDelta.x = -posDelta.x;
            if (curPos.y <= -moveAmount.y || curPos.y >= moveAmount.y) posDelta.y = -posDelta.y;
            if (curPos.z <= -moveAmount.z || curPos.z >= moveAmount.z) posDelta.z = -posDelta.z;

            curPos += posDelta;
        }

        m_locations = Transformaciones.translate(curPos.x, curPos.y, curPos.z);

        m_rotations = Transformaciones.RotateY(rot.y) * Transformaciones.RotateX(rot.x) * Transformaciones.RotateZ(rot.z);

        Matrix4x4 val = m_rotations * m_locations;
        go_parts.GetComponent<MeshFilter>().mesh.vertices =
        Transformaciones.Transform(val * m_scales, v3_originales);
        foreach (var piezas in next)
        {
            piezas.SetPieza(val);
        }
    }
    public void SetPieza(Matrix4x4 m)
    {
        rot.x += delta.x;
        rot.y += delta.y;
        rot.z += delta.z;
        if (rot.x <= min.x || rot.x >= max.x) delta.x = -delta.x;
        if (rot.y <= min.y || rot.y >= max.y) delta.y = -delta.y;
        if (rot.z <= min.z || rot.z >= max.z) delta.z = -delta.z;

        if (posDelta.x + posDelta.y + posDelta.z > 0)
        {
            if (curPos.x <= moveAmount.x || curPos.x >= -moveAmount.x) posDelta.x = -posDelta.x;
            if (curPos.y <= moveAmount.y || curPos.y >= -moveAmount.y) posDelta.y = -posDelta.y;
            if (curPos.z <= moveAmount.z || curPos.z >= -moveAmount.z) posDelta.z = -posDelta.z;

            curPos += posDelta;
        }
         m_locations = Transformaciones.translate(curPos.x, curPos.y, curPos.z);

        m_rotations = Transformaciones.RotateY(rot.y) * Transformaciones.RotateX(rot.x) * Transformaciones.RotateZ(rot.z);

        Matrix4x4 val = m * m_rotations * m_locations;
        go_parts.GetComponent<MeshFilter>().mesh.vertices =
        Transformaciones.Transform(val * m_scales, v3_originales);
        foreach (var piezas in next)
        {
            piezas.SetPieza(val);
        }
    }

}
