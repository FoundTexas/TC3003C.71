using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformacionesQuiz : MonoBehaviour
{
    public List<MeshFilter> Cubos = new List<MeshFilter>();
    public List<Vector3> positions = new List<Vector3>();

    Vector3[] originales;
    Matrix4x4 mOriginal;
    public List<Matrix4x4> matrices = new List<Matrix4x4>();

    float rotZ;

    bool goingUP = true;

    void Start()
    {
        mOriginal = Transformaciones.translate(0.5f, 0, 0);
         for(int i = 0; i < 3; i++){

            GameObject Cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cubos.Add(Cubo.GetComponent<MeshFilter>());

            matrices.Add(mOriginal);
        }

        originales = Cubos[0].mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotZ > 45.0f || rotZ < -45.0f){
            goingUP = !goingUP;
        }

        rotZ += goingUP? 0.1f : -0.1f;

        for(int i = 0; i < Cubos.Count; i++){
                matrices[i] = Transformaciones.RotateZ(rotZ)*mOriginal;
            }
            
        Cubos[0].mesh.vertices = Transformaciones.Transform(matrices[0] * Transformaciones.scale(1,0.5f,0.5f),originales);
        Cubos[1].mesh.vertices = Transformaciones.Transform(matrices[0]*mOriginal*matrices[1]*Transformaciones.scale(1,0.5f,0.5f),originales);
        Cubos[2].mesh.vertices = Transformaciones.Transform(matrices[0]*mOriginal*matrices[1]*mOriginal*matrices[2]*Transformaciones.scale(1,0.5f,0.5f),originales);
    }
}
