using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private const string MAP = "map";
    private const string POSITION = "position";
    private const int MAXPLAYER = 2;
    public GameManager gm;
    private MapLevel ml;

    public bool multiplayer = false;

    bool playerEnter = false;

    public enum ColorOfPlayer
    {
        purple = 0,
        pink = 1,
        intensePink = 2,
        yellow = 3,
        lightBlue = 4,
        orange = 5
    }


    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (FindObjectOfType<GameManager>() != null && gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable() { { MAP, ml } }, MAXPLAYER );
        }
        else
            PhotonNetwork.ConnectUsingSettings();

        multiplayer = true;
        SceneManager.LoadScene("Map02 1");
    }

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.LogError($"Connecting");
        PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable() { { MAP, ml } }, MAXPLAYER);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Connection fail");
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            CustomRoomPropertiesForLobby = new string[] { MAP },
            MaxPlayers = MAXPLAYER,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MAP, ml } },
        });
    }

    public override void OnJoinedRoom()
    {
        Debug.LogError($"Join player " + PhotonNetwork.LocalPlayer.ActorNumber);
        PreparePositionSelectionoptions();
        if (PhotonNetwork.CurrentRoom.GetPlayer(1).IsLocal)
        {
            gm.ShowTeamSelectionScreenFirst();
            Debug.Log("1 player");
            playerEnter = true;

        }
        else if (PhotonNetwork.CurrentRoom.GetPlayer(2).IsLocal)
        {
            gm.ShowTeamSelectionScreenSecond();
            playerEnter = true;
        }
    }

    public void PreparePositionSelectionoptions()
    {
        if (playerEnter == true)
        {
            Debug.Log("Estoy en nm entro el jugador");
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            { 
                //Primer jugador elige color
                var firstPlayer = PhotonNetwork.CurrentRoom.GetPlayer(1);

                if (firstPlayer.CustomProperties.ContainsKey(POSITION))
                {
                    Debug.Log("Estoy en nm color jugador 1");
                    var occupiedPosition = firstPlayer.CustomProperties[POSITION];
                    gm.RestricPositionChoise((ColorOfPlayer)occupiedPosition);
                }

                //Segundo jugador elige color
                var secondPlayer = PhotonNetwork.CurrentRoom.GetPlayer(2);

                if (secondPlayer.CustomProperties.ContainsKey(POSITION))
                {
                    Debug.Log("Estoy en nm color jugador 1");
                    var occupiedPosition = secondPlayer.CustomProperties[POSITION];
                    gm.RestricPositionChoise((ColorOfPlayer)occupiedPosition);
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogError($"Enter player " + newPlayer.ActorNumber);

    }

    public void SetPlayerLevel(MapLevel map)
    {
        ml = map;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { {  MAP, ml } } );
    }

    public void SelectPosition(int num)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { POSITION, num } });
    }
    #endregion
}
