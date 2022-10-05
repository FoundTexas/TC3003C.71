using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pieza
{
    public string Name;
    GameObject go_parts;
    public Vector3 l;
    public Vector3 r;
    public Vector3 s;

    Matrix4x4 m_locations;
    Matrix4x4 m_rotations;
    Matrix4x4 m_scales;
    Vector3[] v3_originales;

    public Pieza[] next;

    public void Set()
    {
        go_parts = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go_parts.name = Name;
        v3_originales = go_parts.GetComponent<MeshFilter>().mesh.vertices;
        m_locations = Transformaciones.translate(l.x, l.y, l.z);
        m_rotations = Transformaciones.RotateX(r.x) * Transformaciones.RotateY(r.y) * Transformaciones.RotateZ(r.z);
        m_scales = Transformaciones.scale(s.x, s.y, s.z);

        foreach (var piezas in next)
        {
            piezas.Set();
        }

    }

    public void SetPieza()
    {
        Matrix4x4 val = m_locations * m_rotations;
        go_parts.GetComponent<MeshFilter>().mesh.vertices =
        Transformaciones.Transform(val * m_scales, v3_originales);
        foreach (var piezas in next)
        {
            piezas.SetPieza(val);
        }
    }
    public void SetPieza(Matrix4x4 m)
    {
        Matrix4x4 val = m * m_locations * m_rotations;
        go_parts.GetComponent<MeshFilter>().mesh.vertices =
        Transformaciones.Transform(val * m_scales, v3_originales);
        foreach (var piezas in next)
        {
            piezas.SetPieza(val);
        }
    }

}
