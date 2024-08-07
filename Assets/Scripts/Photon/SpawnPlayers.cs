using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject cam;

    private void Start()
    {
        PhotonNetwork.Instantiate(cam.name, new Vector2(0,0), Quaternion.identity);
    }
}
