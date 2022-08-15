using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clase3 : MonoBehaviour
{
    GameObject Sphere;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <100; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(0.1f, 10f),
                Random.Range(0.1f, 10f),
                Random.Range(0.1f, 10f)
            );

            Color c = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
                );
            createSphere(pos,c);
        }
    }
    void createSphere(Vector3 Pos, Color color)
    {
        GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tmp.transform.position = Pos;

        var rend = tmp.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Standard"));
        rend.material.SetColor("_Color", color);

        tmp.AddComponent<Rigidbody>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
