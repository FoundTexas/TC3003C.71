using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejercicio : MonoBehaviour
{
    Vector4 A, B, C;
    // Start is called before the first frame update
    void Start()
    {
        A = new Vector4(1, 2, 3, 1);

        Matrix4x4 t = Transformaciones.translate(0, -3, 0);
        Matrix4x4 r = Transformaciones.RotateX(30);

        B = t * r * A;
        C = r * t * A;

        Debug.Log(B.ToString("F5"));
        Debug.Log(C.ToString("F5"));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, A, Color.red);
        Debug.DrawLine(Vector3.zero, B, Color.blue);
        Debug.DrawLine(Vector3.zero, C, Color.green);
    }
}
