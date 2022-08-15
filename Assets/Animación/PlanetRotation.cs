using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    Vector3 rotation;
    // Update is called once per frame
    void Update()
    {
        //rotation.x += 2 * Time.deltaTime;
        rotation.y += 2 * Time.deltaTime;
        //rotation.z += 2 * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(rotation);
    }
}
