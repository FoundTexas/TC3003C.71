using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Vidas : NetworkBehaviour
{
    public const int vidaTotal = 100;
    //[SyncVar (hook = "CambioVida")] public int vidaActual = vidaTotal;

    public NetworkVariable<int> vidaActual = new NetworkVariable<int>(vidaTotal);
    public Image barraVida;

    private void Start()
    {
        CambioDeVida();
    }
    public void Daniar(int cantidad)
    {

        if (!IsServer) return;

        vidaActual.Value -= cantidad;
        CambioDeVida();

        if (vidaActual.Value <= 0)
        {

            vidaActual.Value = vidaTotal;
            RespawnClientRpc();
        }

    }

    void CambioDeVida() 
    {
        float val = vidaActual.Value;
        Debug.Log(val);
        Debug.Log(val/100f);
        barraVida.fillAmount = val / 100f;
    }
    [ClientRpc]
    void RespawnClientRpc()
    {
        if (IsOwner)
        {
            transform.position = Vector3.zero;
            CambioDeVida();
        }

    }
}
