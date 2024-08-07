using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    GameManager gm;
    TileMap map;
    public bool endTurn = false;

    private void Awake()
    {
        map = FindObjectOfType<TileMap>();
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<Unit>().powerBool == true && (this.gameObject.name == "pMechanicCommander" || this.gameObject.name == "pMechanicCommander(Clone)"))
        {

            if (Input.GetMouseButtonDown(1))
            {
                this.gameObject.GetComponent<Unit>().powerBool = false;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                CreateRobot();
            }
        }
        else if ((this.name == "mSniperCommander" || this.name == "mSniperCommander(Clone)") && this.gameObject.GetComponent<Unit>().waiting == true && this.gameObject.GetComponent<Unit>().powerBool == true)
        {
            Concentration();
        }

        else if (this.gameObject.GetComponent<Unit>().powerBool == true && (this.gameObject.name == "mInvokeMachine" || this.gameObject.name == "mInvokeMachine(Clone)" 
            || this.gameObject.name == "pInvokeMachine" || this.gameObject.name == "pInvokeMachine(Clone)" || this.gameObject.name == "rInvokeMachine" || this.gameObject.name == "rInvokeMachine(Clone)"))
        {
            if (this.gameObject.GetComponent<Unit>().powerBool == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InvokeMachine();
                }

                else if (Input.GetMouseButtonDown(1))
                {
                    map.deselectUnit();
                    this.gameObject.GetComponent<Unit>().powerBool = false;
                }
            }
        }
    }

    public void Concentration()
    {
        gm = FindObjectOfType<GameManager>();
        map = FindObjectOfType<TileMap>();
        switch (this.gameObject.name)
        {
            case "mSniperCommander":
            case "mSniperCommander(Clone)":
                if (this.gameObject.GetComponent<Unit>().power >= this.gameObject.GetComponent<Unit>().maxPower)
                {
                    if (this.gameObject.GetComponent<Unit>().waiting == true && this.gameObject.GetComponent<Unit>().powerBool == true)
                    {
                        this.gameObject.GetComponent<Unit>().powerBool = false;
                        this.gameObject.GetComponent<Unit>().atkRange = this.gameObject.GetComponent<Unit>().maxAtkRange;
                        this.gameObject.GetComponent<Unit>().minAtkRange = this.gameObject.GetComponent<Unit>().maxAtkRange;
                        this.gameObject.GetComponent<Unit>().power = 0;
                    }
                    else
                    {            
                        this.gameObject.GetComponent<Unit>().powerBool = true;
                        this.gameObject.GetComponent<Unit>().atkRange = 7;
                        this.gameObject.GetComponent<Unit>().minAtkRange = 7;
                        map.disableHighlightUnitRange();
                        map.finalizeMovementPosition();
                    }
                }
                break;
            case "pMechanicCommander":
            case "pMechanicCommander(Clone)":
                if (this.gameObject.GetComponent<Unit>().power >= this.gameObject.GetComponent<Unit>().maxPower)
                {
                    this.gameObject.GetComponent<Unit>().setMovementState(2);
                    this.gameObject.GetComponent<Unit>().powerBool = true;
                    map.disableHighlightUnitRange();
                    map.finalizeMovementPositionStructure();                  
                }
                    break;
        }
    }

    void CreateRobot()
    {
        ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
        if (ct.moveGrid.GetComponent<SpriteRenderer>().enabled == true)
        {            
            int boardX = (int)gm.tileBeingDisplayed.transform.position.x;
            int boardY = (int)gm.tileBeingDisplayed.transform.position.y;
            GameObject newUnit;
            newUnit = Instantiate(this.gameObject.GetComponent<Unit>().robotPrefab, new Vector3(boardX, boardY, 0), Quaternion.identity);
            if (this.gameObject.GetComponent<Unit>().playerNum == 1)
                newUnit.transform.SetParent(gm.GetComponent<GameManager>().player1.transform);
            else
                newUnit.transform.SetParent(gm.GetComponent<GameManager>().player2.transform);
            newUnit.GetComponent<Unit>().tileX = boardX;
            newUnit.GetComponent<Unit>().tileY = boardY;
            newUnit.GetComponent<Unit>().tileBeingOccupied = map.tilesOnMap[boardX, boardY];
            ct.unitOnTile = newUnit;
            newUnit.GetComponent<Unit>().playerNum = gm.GetComponent<GameManager>().currPlayer;

            this.gameObject.GetComponent<Unit>().unitPreviousX = this.gameObject.GetComponent<Unit>().tileX;
            this.gameObject.GetComponent<Unit>().unitPreviousY = this.gameObject.GetComponent<Unit>().tileY;
            this.gameObject.GetComponent<Unit>().wait();
            this.gameObject.GetComponent<Unit>().setMovementState(3);
            map.deselectUnit();
            Debug.Log("Creado 1");
            this.gameObject.GetComponent<Unit>().powerBool = false;
            this.gameObject.GetComponent<Unit>().power = 0;
            gm.UpdateColors();
        }
    }

    void InvokeMachine()
    {
        ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
        if (ct.moveGrid.GetComponent<SpriteRenderer>().enabled == true)
        {
            int boardX = (int)gm.tileBeingDisplayed.transform.position.x;
            int boardY = (int)gm.tileBeingDisplayed.transform.position.y;
            GameObject newUnit;
            newUnit = Instantiate(this.gameObject.GetComponent<Structure>().unitToInvoke, new Vector3(boardX, boardY, 0), Quaternion.identity);
            if (this.gameObject.GetComponent<Unit>().playerNum == 1)
            {
                newUnit.transform.SetParent(gm.GetComponent<GameManager>().player1.transform);
                gm.moneyPlayer1 = gm.moneyPlayer1 - this.gameObject.GetComponent<Structure>().unitToInvoke.GetComponent<Unit>().cost;
            }
            else
            {
                newUnit.transform.SetParent(gm.GetComponent<GameManager>().player2.transform);
                gm.moneyPlayer2 = gm.moneyPlayer2 - this.gameObject.GetComponent<Structure>().unitToInvoke.GetComponent<Unit>().cost;
            }
            newUnit.GetComponent<Unit>().tileX = boardX;
            newUnit.GetComponent<Unit>().tileY = boardY;
            newUnit.GetComponent<Unit>().tileBeingOccupied = map.tilesOnMap[boardX, boardY];
            newUnit.GetComponent<Unit>().wait();
            newUnit.GetComponent<Unit>().setMovementState(3);
            ct.unitOnTile = newUnit;
            newUnit.GetComponent<Unit>().playerNum = gm.GetComponent<GameManager>().currPlayer;

            this.gameObject.GetComponent<Unit>().unitPreviousX = this.gameObject.GetComponent<Unit>().tileX;
            this.gameObject.GetComponent<Unit>().unitPreviousY = this.gameObject.GetComponent<Unit>().tileY;
            this.gameObject.GetComponent<Unit>().wait();
            this.gameObject.GetComponent<Unit>().setMovementState(3);
            map.deselectUnit();
            Debug.Log("Creado 1");
            this.gameObject.GetComponent<Unit>().powerBool = false;
            gm.UpdateColors();
            gm.MoneyUpdate();
        }
    }

    public void InvokeMachineButton()
    {
        map.DisableColliders();
        map.disableHighlightUnitRange();
        this.gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        this.gameObject.GetComponent<Structure>().UpdateMoney();
        this.gameObject.GetComponent<Unit>().setMovementState(2);
    }

    public void InvokeButton()
    {
        if (gm.GetComponent<GameManager>().moneyPlayer1 >= this.gameObject.GetComponent<Structure>().unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 1)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;            
            map.finalizeMovementPositionStructure();
            this.gameObject.GetComponent<Unit>().powerBool = true;
            this.gameObject.GetComponent<Structure>().ActivateCollider();            
        }
        else if (gm.GetComponent<GameManager>().moneyPlayer2 >= this.gameObject.GetComponent<Structure>().unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 2)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
            map.finalizeMovementPositionStructure();
            this.gameObject.GetComponent<Unit>().powerBool = true;
            this.gameObject.GetComponent<Structure>().ActivateCollider();
        }
        else
            Debug.Log("Dinero insuficiente");
    }

    public void CancelBotton()
    {
        transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        this.gameObject.GetComponent<Unit>().powerBool = false;
        this.gameObject.GetComponent<Structure>().ActivateCollider();
        map.deselectUnit();
    }
}
