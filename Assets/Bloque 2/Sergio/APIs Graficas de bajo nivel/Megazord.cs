using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megazord : MonoBehaviour
{
    enum PARTS
    {
        RP_HEAP, RP_TORSO, RP_CHEST
    }
    List<GameObject> go_parts = new List<GameObject>();
    List<Matrix4x4> m_locations = new List<Matrix4x4>();
    List<Matrix4x4> m_scales = new List<Matrix4x4>();
    List<Matrix4x4> m_rotations = new List<Matrix4x4>();
    Vector3[] v3_originales;

    [SerializeField]
    PARTS parts = PARTS.RP_HEAP;

    void Start()
    {
        //HEAP
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        v3_originales = go_parts[0].GetComponent<MeshFilter>().mesh.vertices;
        m_scales.Add(Transformaciones.scale(1, 0.5f, 1));
        m_locations.Add(Transformaciones.translate(0, 0, 0));
        m_rotations.Add(Transformaciones.RotateY(45));
        
        //TORSO
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.scale(1, 0.75f, 1));
        m_locations.Add(Transformaciones.translate(0, 0.5f/2 + 0.75f / 2, 0));
        m_rotations.Add(Transformaciones.RotateY(45));

        //CHEST
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.scale(1.2f, 0.4f, 1.2f));
        m_locations.Add(Transformaciones.translate(0, 0.75f/2 + 0.4f/ 2, 0));
        m_rotations.Add(Transformaciones.RotateY(45));

        //NECK
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.scale(0.25f, 0.2f, 0.25f));
        m_locations.Add(Transformaciones.translate(0, 0.4f/2 + 0.2f/2, 0));
        m_rotations.Add(Transformaciones.RotateY(45));

        //HEAD
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.scale(1f, 1f, 1f));
        m_locations.Add(Transformaciones.translate(0, 0.2f/2 + 1/2, 0));
        m_rotations.Add(Transformaciones.RotateY(45));


    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 account = Matrix4x4.identity;
        for (int i = 0; i < go_parts.Count; i++)
        {
            Matrix4x4 m = account * m_locations[i] * m_rotations[i] * m_scales[i];
            account *= m_locations[i];

            go_parts[i].GetComponent<MeshFilter>().mesh.vertices =
            Transformaciones.Transform(m, v3_originales);
        }
    }
}
