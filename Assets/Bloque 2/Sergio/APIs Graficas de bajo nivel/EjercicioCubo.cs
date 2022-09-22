using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioCubo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Considera Un cubo de lado 3.5 centrado en el origen
        //1. Encuentra las posiciones de sus 8 vertices
        cube();



    }

    void cube()
    {
        Mesh mymesh = new Mesh();
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1.75f, -1.75f, -1.75f),
            new Vector3(-1.75f, 1.75f, -1.75f),
            new Vector3(1.75f, -1.75f, -1.75f),
            new Vector3(1.75f, 1.75f, -1.75f),

            new Vector3(-1.75f, -1.75f, 1.75f),
            new Vector3(-1.75f, 1.75f, 1.75f),
            new Vector3(1.75f, -1.75f, 1.75f),
            new Vector3(1.75f, 1.75f, 1.75f),
        };

        mymesh.vertices = vertices;
        int[] tris = new int[] {
            1, 0, 4,
            5, 1, 4


        };
        mymesh.triangles = tris;
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = mymesh;
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
