using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject golpe = collision.gameObject;
        Vidas vidaactial = golpe.GetComponent<Vidas>();
        if (vidaactial)
        {
            vidaactial.Daniar(20);
        }
        Destroy(this.gameObject);
    }
}
