using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clase2 : MonoBehaviour
{
    [SerializeField] Vector3 maxScale, minScale;
    bool isGrowing = true;
    Vector3 curScale = new Vector3(1f,1f,1f);
    Transform sphere;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.Find("Sphere").transform;
    }

    void Update()
    {
        CheckIfGrowing();
        curScale += new Vector3(0.1f, 0.1f, 0.1f) * (isGrowing ? 1 : -1);
        sphere.localScale = curScale;
    }

    public void CheckIfGrowing()
    {
        if (curScale.x >= maxScale.x)
        {
            isGrowing = false;
        }
        else if (curScale.x <= minScale.x)
        {
            isGrowing = true;
        }
    }
}
