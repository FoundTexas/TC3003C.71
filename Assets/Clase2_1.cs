using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clase2_1 : MonoBehaviour
{
    Vector3 rotation;
    Transform Cubo;
    // Start is called before the first frame update
    void Start()
    {
        Cubo = GameObject.Find("Cubo").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += 2 * Time.deltaTime;
        rotation.y += 2 * Time.deltaTime;
        rotation.z += 2 * Time.deltaTime;

        Cubo.localRotation = Quaternion.Euler(rotation);
    }
}
