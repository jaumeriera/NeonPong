using AOT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingToServer : MonoBehaviour
{
    private void Start() {
        PhotonNetwork.ConnectUsingSettings("Foo");
    }
}
