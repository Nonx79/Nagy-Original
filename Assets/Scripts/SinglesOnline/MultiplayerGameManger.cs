using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameManger : GameManager, IOnEventCallback
{
    //Reference
    

    //Player
    private int currentPlayer;

    //Photon
    protected const byte SET_GAME_STATE_EVENT_CODE = 1;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override bool CanPerformMove()
    {
        if (!IsGameInProgress() || !ILocalPlayersTurn())
            return false;
        return true;
    }

    private bool ILocalPlayersTurn()
    {
        return currentPlayer == currPlayer;
    }

    public void SetLocalPlayer(int num)
    {
        currentPlayer = num;
    }

    public override void TryToStartCurrentGame()
    {
        if (nm.IsRoomFull())
        {
            SetGameState(GameState.Play);
        }
    }

    protected override void SetGameState(GameState state)
    {
        object[] content = new object[] { (int)state };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SET_GAME_STATE_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    /*

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == SET_GAME_STATE_EVENT_CODE)
        {
            object[] data = (object[])photonEvent.CustomData;
            GameState state = (GameState)data[0];
            this.state = state;
        }
    }
    */

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == SET_GAME_STATE_EVENT_CODE)
        {
            // Verificamos si los datos son un array de object[]
            if (photonEvent.CustomData is object[] data)
            {
                // Si los datos son del tipo esperado, procedemos con la lógica
                GameState state = (GameState)data[0];
                this.state = state;
            }
            else
            {
                // Si los datos no son del tipo esperado, mostramos un error
                Debug.LogError("CustomData no es un array de object[], tipo actual: " + photonEvent.CustomData.GetType());
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //nm = FindObjectOfType<NetworkManager>();
    }

    public override void Update()
    {
       base.Update();
    }
}
