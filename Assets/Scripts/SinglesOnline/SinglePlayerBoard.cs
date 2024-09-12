using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]

public class SinglePlayerBoard : TileMap
{
    //Reference
    PhotonView pv;

    protected override void Awake()
    {
        base.Awake();

        pv = FindObjectOfType<PhotonView>();
    }

    // Start is called before the first frame update
    public override void SelectUnitMove(Vector2 coords)
    {
        pv.RPC(nameof(RPC_OnSelectedPieceMoved), RpcTarget.AllBuffered, new object[] { coords });    
    }

    public override void SetSelectedUnit(Vector2 coords)
    {
        pv.RPC(nameof(RPC_OnSetSelectedPiece), RpcTarget.AllBuffered, new object[] { coords });
    }

    [PunRPC]
    private void RPC_OnSelectedPieceMoved(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        mouseClickToSelectUnitV2(intCoords);
    }
    [PunRPC]
    private void RPC_OnSetSelectedPiece(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        OnSetSelectedUnit(intCoords);
    }

    
}
