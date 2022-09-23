using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    private void Start()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        float r = Input.GetAxis("Horizontal") * Time.deltaTime * 100;
        float t = Input.GetAxis("Vertical") * Time.deltaTime * 10;

        transform.Rotate(0, r, 0);
        transform.Translate(0, 0, t);
    }
}
