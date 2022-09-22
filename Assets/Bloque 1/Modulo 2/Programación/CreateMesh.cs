using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mymesh = new Mesh();
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(3, 0, 0),
            new Vector3(3, 0, 3),
            new Vector3(0, 0, 3),

            new Vector3(0, 3, 0),
            new Vector3(3, 3, 0),
            new Vector3(3, 3, 3),
            new Vector3(0, 3, 3)
        };

        mymesh.vertices = vertices;
        int[] tris = new int[] {
            0, 1, 3,
            1, 2, 3,
            0, 4, 1,
            1, 4, 5,
            2,5,1,
            2,5,7,
            2,6,3,
            2,6,7,
            0,3,4,
            3,4,6,
            4,5,6,
            6,5,7
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
