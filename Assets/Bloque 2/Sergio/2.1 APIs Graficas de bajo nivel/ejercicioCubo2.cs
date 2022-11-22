using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioTransformacion : MonoBehaviour
{
    GameObject sun;
    Vector3[] vSun;

    GameObject mer;
    Vector3[] vMer;

    GameObject ven;
    Vector3[] vVen;

    GameObject ear;
    Vector3[] vEar;

    GameObject moo;
    Vector3[] vMoo;

    Matrix4x4 tr1;
    Matrix4x4 tr2;
    Matrix4x4 tr3;
    Matrix4x4 tr4;
    Matrix4x4 tr5;

    Matrix4x4 tm1;
    Matrix4x4 tm2;
    Matrix4x4 tm3;
    Matrix4x4 tm4;

    Matrix4x4 ts1;
    Matrix4x4 ts2;
    Matrix4x4 ts3;
    Matrix4x4 ts4;
    Matrix4x4 moon;


    float rotY;

    // Start is called before the first frame update
    void Start()
    {
        rotY = 0;

        sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
        vSun = sun.GetComponent<MeshFilter>().mesh.vertices;

        mer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        mer.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        vMer = mer.GetComponent<MeshFilter>().mesh.vertices;

        ven = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ven.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.grey);
        vVen = ven.GetComponent<MeshFilter>().mesh.vertices;

        ear = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ear.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        vEar = ear.GetComponent<MeshFilter>().mesh.vertices;

        moo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        moo.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        vMoo = moo.GetComponent<MeshFilter>().mesh.vertices;

    }

    // Update is called once per frame
    void Update()
    {
        rotY += 0.5f;
        tr1 = Transformaciones.RotateY(rotY);
        sun.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(tr1, vSun);
        sun.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        tr2 = Transformaciones.RotateY(rotY * 1.1f);
        tm1 = Transformaciones.translate(1.0f, 0, 0);
        ts1 = Transformaciones.scale(0.15f, 0.15f, 0.15f);
        mer.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(tr2 * tm1 * ts1, vMer);
        mer.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        tr3 = Transformaciones.RotateY(rotY * 0.7f);
        tm2 = Transformaciones.translate(1.5f, 0, 0);
        ts2 = Transformaciones.scale(0.2f, 0.2f, 0.2f);
        ven.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(tr3 * tm2 * ts2, vVen);
        ven.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        tr4 = Transformaciones.RotateY(rotY * 1.3f);
        tm3 = Transformaciones.translate(2.0f, 0, 0);
        ts3 = Transformaciones.scale(0.3f, 0.3f, 0.3f);
        ear.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(tr4 * tm3 * ts3, vEar);
        ear.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        tr5 = Transformaciones.RotateY(rotY * 3.6f);
        tm4 = Transformaciones.translate(0.3f, 0, 0);
        ts4 = Transformaciones.scale(0.1f, 0.1f, 0.1f);

        moon = tr4 * tm3 * tr5 * tm4 * ts4;
        moo.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(moon, vMoo);
        moo.GetComponent<MeshFilter>().mesh.RecalculateNormals();

    }
}