using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshScript2 : MonoBehaviour
{

    void Start()
    {
        Shape i = new Shape();
        i.topology = new int[3] { 0, 1, 2 };
        i.geometry = new Vector3[3]
        {
            new Vector3(0,0,0),
             new Vector3(13,0,0),
              new Vector3(5,12,0)
        };
        //Shape o = Tessellate(i);


        Shape o = SimsFormPro();
        AddMeshFilter(o.geometry, o.topology);
        AddMeshRenderer();
    }
    class Shape
    {
        public Vector3[] geometry;
        public int[] topology;

        public Shape(Vector3[] geometry,int[] topology)
        {
            this.geometry = geometry;
            this.topology = topology;
        }
        public Shape() { }
    }

    private void AddMeshRenderer()
    {
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
    }
    private void AddMeshFilter(Vector3[] geometry, int[] topology)
    {
        Mesh myMesh = new Mesh();
        myMesh.vertices = geometry;
        myMesh.triangles = topology;
        myMesh.RecalculateNormals();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = myMesh;
    }

    Shape Tessellate(Shape input, int add, List<Vector3> refer)
    {
        Vector3[] originalG = input.geometry;
        int[] originalT = input.topology;
        Vector3 A = originalG[0];
        Vector3 B = originalG[1];
        Vector3 C = originalG[2];
        Vector3 o = (A + B) / 2;
        Vector3 p = (B + C) / 2;
        Vector3 q = (A + C) / 2;

        Vector3[] resultG = new Vector3[6] { A, B, C, o, p, q };
        int i = 0;
        int[] resultT = new int[12] {
            refer.Contains(A) ? refer.IndexOf(A) :  refer.Count > 0 ? refer.Count-1-1:0,
            refer.Contains(o) ? refer.IndexOf(o) :  refer.Count > 0 ? refer.Count-1+2:3,
            refer.Contains(q) ? refer.IndexOf(q) :  refer.Count > 0 ? refer.Count-1+4:5,

            refer.Contains(o) ? refer.IndexOf(o) :  refer.Count > 0 ? refer.Count-2+2:3,
            refer.Contains(B) ? refer.IndexOf(B) :  refer.Count > 0 ? refer.Count:1,
            refer.Contains(p) ? refer.IndexOf(p) :  refer.Count > 0 ? refer.Count-1+3:4,

            refer.Contains(q) ? refer.IndexOf(q) :  refer.Count > 0 ? refer.Count-1+4:5,
            refer.Contains(p) ? refer.IndexOf(p) :  refer.Count > 0 ? refer.Count-1+3:4,
            refer.Contains(C) ? refer.IndexOf(C) :  refer.Count > 0 ? refer.Count-1+1:2,

            refer.Contains(o) ? refer.IndexOf(o) :  refer.Count > 0 ? refer.Count-1+2:3,
            refer.Contains(p) ? refer.IndexOf(p) :  refer.Count > 0 ? refer.Count-1+3:4,
            refer.Contains(q) ? refer.IndexOf(q) :  refer.Count > 0 ? refer.Count-1+4:5,
        };
        foreach (var g in resultT)
        {
            Debug.Log(g);
        }
        Shape r = new Shape(resultG, resultT);

        return r;

    }

    public void SimsForm()
    {
        Vector3[] vertices = new Vector3[]{
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),

            new Vector3(-1, 0, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, -1)
        };
        int[] tris = new int[] {
            0,1,2, 0,2,4,
            0,5,1, 0,4,5,
            5,3,1, 3,5,4,
            3,2,1, 3,4,2
        };
    }
    Shape SimsFormPro()
    {
        Vector3[] vertices = new Vector3[]{
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(-1,0,0),
            new Vector3(0,0,-1),
            new Vector3(0,1,0),
            new Vector3(0,-1,0)

        };

        int[] tris = new int[24] { 
            1, 0, 4,
            0, 3, 4,
            3, 2, 4, 
            2, 1, 4, 
            5, 0, 1, 
            5, 3, 0, 
            5, 2, 3, 
            5, 1, 2 
        };

        List<int> resultT = new List<int>();
        List<Vector3> resultG = new List<Vector3>();
        int addition = 0;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Shape tmp = Tessellate(
                new Shape(
                    new Vector3[3] {
                        vertices[tris[i]],
                        vertices[tris[i+1]],
                        vertices[tris[i+2]]
                    },
                    new int[3] {
                        tris[i],
                        tris[i+1],
                        tris[i+2]
                    }
                    ),
                addition,
                resultG
                );
            resultT.AddRange(new List<int>(tmp.topology));
            resultG.AddRange(new List<Vector3>(tmp.geometry));
            resultG = resultG.Distinct().ToList();

            addition += 3;//(resultT.Count / 3)-1;
            Debug.Log("/////////    Vertices " + resultG.Count+ "   ///////   Caras " + resultT.Count/3);
        }
        
        Shape r = new Shape(
            resultG .ToArray(), resultT.ToArray());

        return r;
    }

}
