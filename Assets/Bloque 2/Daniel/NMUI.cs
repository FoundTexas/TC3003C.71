using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NMUI : MonoBehaviour
{

    public NetworkManager net;
    public NetworkTransport transport;

    public void managerstartHost()
    {
        net.StartHost();
    }
    public void managerstartClient()
    {
        //transport.StartClient();
        net.StartClient();
    }
}
