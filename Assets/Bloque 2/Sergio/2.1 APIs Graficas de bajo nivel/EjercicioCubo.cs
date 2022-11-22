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
        float side = 3.5f;
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

        Matrix4x4 rz = Transformaciones.RotateZ(-15.03f);
        Matrix4x4 tz = Transformaciones.translate(0, 0, 12.77f);
        Matrix4x4 ry = Transformaciones.RotateY(2.48f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
