using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject balaPrefab;
    public Transform spawnBala;
    private void Start()
    {
        if (!IsOwner)
        {
            return;
        }

        GetComponent<MeshRenderer>().material.color = Color.green;
        Vector3 spawn = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Quaternion rot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        gameObject.transform.position = spawn;
        gameObject.transform.rotation = rot;
    }
    void Update()
    {
        if (!IsOwner) //IsLocalPlayer)
        {
            return;
        }

        float r = Input.GetAxis("Horizontal") * Time.deltaTime * 100;
        float t = Input.GetAxis("Vertical") * Time.deltaTime * 10;

        transform.Rotate(0, r, 0);
        transform.Translate(0, 0, t);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisparoServerRpc();
        }
    }

    [ServerRpc]
    void DisparoServerRpc()
    {
        GameObject bala = GameObject.Instantiate(balaPrefab, spawnBala.position, spawnBala.rotation);

        bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 5;

        //bala.GetComponent<NetworkObject>().Spawn();
        //NetworkObject.Spawn(bala);

        Destroy(bala, 2);
    }
}
