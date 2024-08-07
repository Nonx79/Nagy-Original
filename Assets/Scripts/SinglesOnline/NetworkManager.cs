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

    public enum ColorOfPlayer
    {
        purple = 1,
        pink = 2,
        intensePink = 3,
        yellow = 4,
        lightBlue = 5,
        orange = 6
    }


    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom(new ExitGames.Client.Photon.Hashtable() { { MAP, ml } }, MAXPLAYER );
        }
        else
            PhotonNetwork.ConnectUsingSettings();

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
        gm.ShowTeamSelectionScreen();
    }

    private void PreparePositionSelectionoptions()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            var firstPlayer = PhotonNetwork.CurrentRoom.GetPlayer(1);
            if (firstPlayer.CustomProperties.ContainsKey(POSITION))
            {
                var occupiedPosition = firstPlayer.CustomProperties[POSITION];
                gm.RestricPositionChoise((ColorOfPlayer)occupiedPosition);
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
