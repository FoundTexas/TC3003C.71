using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh myMesh = new Mesh();
        Vector3[] vertices = new Vector3[ ]{
            new Vector3(-1, -1, 1),
            new Vector3(1, -1, 1),
            new Vector3(1, 1, 1),
            new Vector3(-1, 1, 1),
            new Vector3(1, -1, -1),
            new Vector3(1, 1, -1),
            new Vector3(-1, -1, -1),
            new Vector3(-1, 1, -1)
        };
        myMesh.vertices = vertices;
        int[] tris = new int[]{0, 1, 2, 0, 2, 3, 1, 4, 5, 1, 5, 2, 4, 6, 7, 4, 7, 5, 6, 0, 3, 6, 3, 7, 3, 2, 5, 3, 5, 7, 1, 0, 6, 1, 6, 4};
        myMesh.triangles = tris;
        myMesh.RecalculateNormals();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = myMesh;
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
