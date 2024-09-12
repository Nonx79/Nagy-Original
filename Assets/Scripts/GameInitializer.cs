using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Game dependent objects")]
    [SerializeField] private MultiplayerGameManger mgmp;

    [Header("Scene references")]
    [SerializeField] private NetworkManager nm;
    [SerializeField] private GameManager gm;
    [SerializeField] private Transform boardAnchor;

    public void SetDependences()
    {
        nm = FindObjectOfType<NetworkManager>();
        gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {            
            boardAnchor = gm.gameObject.transform;
        }
    }

    public void CreateMultiplayerBoard()
    {
        if (!nm.IsRoomFull())
        {
            PhotonNetwork.Instantiate(mgmp.name, boardAnchor.position, boardAnchor.rotation);
        }
    }

    /*
    public void InitializeMultiplaterController()
    {
        SinglePlayerBoard board = FindObjectOfType<SinglePlayerBoard>();

        if (board)
        {
            insta
        }
    }
    */
}
