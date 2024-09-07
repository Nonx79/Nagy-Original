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
    bool playerEnter = false;

    //Color
    bool firstTimePlayerOne = false;
    bool firstTimePlayerTwo = false;

    public bool multiplayer = false;

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
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                var firstPlayer = PhotonNetwork.CurrentRoom.GetPlayer(1);
                var secondPlayer = PhotonNetwork.CurrentRoom.GetPlayer(2);
                
                //Primer jugador elige color
                if (!firstPlayer.IsLocal)
                {
                    if (firstTimePlayerOne == false)
                    {
                        firstPlayer.CustomProperties[POSITION] = 0;
                        firstTimePlayerOne = true;
                    }
                    var occupiedPosition = firstPlayer.CustomProperties[POSITION];
                    gm.RestricPositionChoise((ColorOfPlayer)occupiedPosition);
                }

                //Segundo jugador elige color               
                if (!secondPlayer.IsLocal)
                {
                    if (firstTimePlayerTwo == false)
                    {
                        secondPlayer.CustomProperties[POSITION] = 1;
                        firstTimePlayerTwo = true;
                    }
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

    /*
    public void ChooseColor(ColorOfPlayer chosenColor)
    {
        // Actualizar las propiedades personalizadas del jugador
        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
    {
        { "POSITION", chosenColor } // Actualizamos la clave "POSITION" con el color elegido
    };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps); // Sincronizar con Photon
    }
    */

    #endregion
}
