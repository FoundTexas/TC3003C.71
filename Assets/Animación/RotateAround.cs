using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject target;
    public float speed;

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, Random.RandomRange(1,speed) * Time.deltaTime);
    }
}
