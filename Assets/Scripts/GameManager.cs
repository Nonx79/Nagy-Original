using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Photon.Pun;
using static NetworkManager;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.ComponentModel;

public class GameManager : MonoBehaviourPun
{
    //Players
    public int currPlayer = 1;
    public int totalPlayers = 2;
    public GameObject containerPlayer1;
    public GameObject containerPlayer2;
    
    //Player Money
    public int moneyPlayer1;
    public int moneyPlayer2;
    public int incomePlayer1;
    public int incomePlayer2;

    //Player Units
    public int unitsPlayer1;
    public int unitsWaitPlayer1;
    public int unitsPlayer2;
    public int unitsWaitPlayer2;
    public List<Unit> Players = new List<Unit>();
    
    //Player Faction
    public int factionChoose1;
    public int factionChoose2;   
    
    //References
    public TileMap map;
    public IA ia;
    public UIManager um;

    //Raycast
    public Ray ray;
    public RaycastHit hit;

    //Cursor Info for tileMapScript
    public int cursorX;
    public int cursorY;

    //currentTileBeingMousedOver
    public int selectedXTile;
    public int selectedYTile;

    //Ui Unit
    public GameObject unitBeingDisplayed;
    public GameObject structureBeingDisplayed;
    public GameObject tileBeingDisplayed;
    public bool displayingUnitInfo = false;
    public bool battleInfo = false;

    //UnitUI
    public Text UIUnitCurrentHealth;
    public Text UIUnitPower;
    public Image UIunitSprite;

    //Int ui units count
    public int intUnits01;
    public int intUnits02;

    int actualDay = 1;

    public string colorUnit;
    //int colorUnit;

    public GameObject player1Commander;
    public GameObject player2Commander;



    string[] climes = new string[5] { "Sunny", "Sunny", "Sunny", "Storm", "Rain" };

    //Ia
    public bool IAPlayer1 = false;
    public bool IAPlayer2 = false;

    //Music
    public GameObject track1;
    public GameObject track2;

    //Fire
    new GameObject[] fires;

    //Create
    public bool creation;

    //Camera
    public Camera c;

    //Online
    public NetworkManager nm;
    public bool multiplayer = false;
    public bool player1Ready = false;
    public bool player2Ready = false;
    //public bool colorOnline = false;
    public bool playerEnter = false;

    //chatgpt
    private bool objectUsed = false;
   
    private void Awake()
    {
        um = FindObjectOfType<UIManager>();
        nm = FindObjectOfType<NetworkManager>();
        c = FindObjectOfType<Camera>();
        IncomeUpdate();
        MoneyUpdate();
        UpdateColors();
        DayUpdate(actualDay);

        um.prevClime.text = climes[0];
        um.clime.text = climes[0];
        PhotonNetwork.AutomaticallySyncScene = true;
        
        if (nm.multiplayer == true)
            multiplayer = true;

        //Colors button
        /*
        purple.interactable = false;
        pink.interactable = false;
        */
    }

    public void Update()
    {
        Debug.Log("penes");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            CursorUIUpdate();
            UnitUIUpdate();
        }

        if (containerPlayer1.transform.childCount != intUnits01)
        {
            UpdateUnits();
        }

        if (containerPlayer2.transform.childCount != intUnits02)
        {
            UpdateUnits();
        }
    }

    public void EndTurn()
    {
        if (map.selectedUnit == null)
        {
            map.disableHighlightUnitRange();
            ResetUnitsMovements(returnTeam(currPlayer));
            ResetStructuresAndIncome();
            currPlayer++;
            MoneyUpdate();
            DamageFire();
            TimerFire();

            if (currPlayer > totalPlayers)
            {
                currPlayer = 1;
                actualDay = actualDay + 1;
                DayUpdate(actualDay);
                Debug.Log(actualDay);
            }
            ClimesDoSomething();
            Debug.Log("El jugador es " + currPlayer);

            if (IAPlayer1 == true && currPlayer == 1)
            {
                ia.StartTurn();
            }
            if (IAPlayer2 == true && currPlayer == 2)
            {
                ia.StartTurn();
            }

            switch (currPlayer)
            {
                case 1:
                    switch (player1Commander.GetComponent<Unit>().faction)
                    {
                        case 0:
                            track2.GetComponent<AudioSource>().enabled = false;
                            track1.GetComponent<AudioSource>().enabled = true;

                            c.transform.position = containerPlayer1.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                        case 1:
                            track1.GetComponent<AudioSource>().enabled = false;
                            track2.GetComponent<AudioSource>().enabled = true;

                            c.transform.position = containerPlayer1.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                    }
                    break;
                case 2:
                    switch (player2Commander.GetComponent<Unit>().faction)
                    {
                        case 0:
                            track2.GetComponent<AudioSource>().enabled = false;
                            track1.GetComponent<AudioSource>().enabled = true;

                            c.transform.position = containerPlayer2.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                        case 1:
                            track2.GetComponent<AudioSource>().enabled = true;
                            track1.GetComponent<AudioSource>().enabled = false;

                            c.transform.position = containerPlayer2.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                    }
                            break;
            }
        }
    }

    public void ResetStructuresAndIncome()
    {
        GameObject[] structuresOnGame;
        structuresOnGame = GameObject.FindGameObjectsWithTag("Structure");
        foreach (GameObject s in structuresOnGame)
        {
            if (currPlayer == s.GetComponent<Structure>().playerNum)
            {
                s.GetComponent<Structure>().waiting = false;
                s.GetComponent<Structure>().usage = 2;
                s.GetComponent<SpriteRenderer>().color = Color.white;
                s.GetComponent<Structure>().Income();
                if (s.GetComponent<Structure>().currHp < 100)
                {
                    s.GetComponent<Structure>().currHp = s.GetComponent<Structure>().currHp + 10;
                    s.GetComponent<Structure>().UpdateHpUI();
                    if (s.GetComponent<Structure>().currHp > 100)
                    {
                        s.GetComponent<Structure>().currHp = 100;
                        s.GetComponent<Structure>().UpdateHpUI();
                    }
                }
            }
        }
    }

    public void ResetUnitsMovements(GameObject teamToReset)
    {

        foreach (Transform unit in teamToReset.transform)
        {
            unit.GetComponent<Unit>().moveAgain();
            if (unit.name == "mSniperCommander(Clone)" || unit.name == "mSniperCommander")
                unit.GetComponent<Powers>().endTurn = true;
            if (unit.GetComponent<Unit>().lider == true)
            {
                unit.GetComponent<Unit>().currHp = unit.GetComponent<Unit>().currHp + 5;
                unit.GetComponent<Unit>().UpdateHpUI();
                if (unit.GetComponent<Unit>().currHp > 100)
                {
                    unit.GetComponent<Unit>().currHp = 100;
                    unit.GetComponent<Unit>().UpdateHpUI();
                }
            }
        }
    }

    public GameObject returnTeam(int i)
    {
        GameObject teamToReturn = null;
        if (i == 1)
        {
            teamToReturn = containerPlayer1;
        }
        else if (i == 2)
        {
            teamToReturn = containerPlayer2;
        }
        return teamToReturn;
    }

    public void GameOver()
    {
        transform.GetChild(0).GetComponent<Canvas>().enabled = true;
    }

    public void CursorUIUpdate()
    {
        //If we are mousing over a tile, highlight it
        if (hit.transform.CompareTag("Tile"))
        {
            if (tileBeingDisplayed == null)
            {
                selectedXTile = hit.transform.gameObject.GetComponent<ClickableTile>().tileX;
                selectedYTile = hit.transform.gameObject.GetComponent<ClickableTile>().tileY;
                cursorX = selectedXTile;
                cursorY = selectedYTile;
                map.quadOnMapSelected[selectedXTile, selectedYTile].GetComponent<SpriteRenderer>().enabled = true;
                tileBeingDisplayed = hit.transform.gameObject;

            }
            else if (tileBeingDisplayed != hit.transform.gameObject)
            {
                selectedXTile = tileBeingDisplayed.GetComponent<ClickableTile>().tileX;
                selectedYTile = tileBeingDisplayed.GetComponent<ClickableTile>().tileY;
                map.quadOnMapSelected[selectedXTile, selectedYTile].GetComponent<SpriteRenderer>().enabled = false;

                selectedXTile = hit.transform.gameObject.GetComponent<ClickableTile>().tileX;
                selectedYTile = hit.transform.gameObject.GetComponent<ClickableTile>().tileY;
                cursorX = selectedXTile;
                cursorY = selectedYTile;
                map.quadOnMapSelected[selectedXTile, selectedYTile].GetComponent<SpriteRenderer>().enabled = true;
                tileBeingDisplayed = hit.transform.gameObject;

            }

        }       
        //We aren't pointing at anything no cursor.
        else
        {
            map.quadOnMapSelected[selectedXTile, selectedYTile].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void IncomeUpdate()
    {
        GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
        foreach (GameObject s in structures)
        {
            if (s.name == "Mine")
            {
                if (s.GetComponent<Structure>().playerNum == 1)
                    incomePlayer1 = 100 + incomePlayer1;
                if (s.GetComponent<Structure>().playerNum == 2)
                    incomePlayer2 = 100 + incomePlayer2;
            }
        }
    }

    public void MoneyUpdate()
    {
        um.moneyP1.text = moneyPlayer1.ToString();
        um.moneyP2.text = moneyPlayer2.ToString();
        um.incomeTextP1.text = "+" + incomePlayer1.ToString();
        um.incomeTextP2.text = "+" + incomePlayer2.ToString();
    }

    public void UnitUIUpdate()
    {
        if (!displayingUnitInfo)
        {
            if (hit.transform.CompareTag("Tile"))
            {
                if ((hit.transform.GetComponent<ClickableTile>().unitOnTile != null || hit.transform.GetComponent<ClickableTile>().structureOnTile != null)
                    && map.selectedUnit == null)
                {
                    displayingUnitInfo = true;

                    if (hit.transform.GetComponent<ClickableTile>().unitOnTile != null)
                    {
                        unitBeingDisplayed = hit.transform.GetComponent<ClickableTile>().unitOnTile;
                        var highlightedUnitScript = unitBeingDisplayed.GetComponent<Unit>();
                        UIUnitCurrentHealth.text = highlightedUnitScript.currHp.ToString();

                        if (unitBeingDisplayed.GetComponent<Unit>().maxPower != 0)
                        {
                            if (highlightedUnitScript.power > highlightedUnitScript.maxPower)
                            {
                                UIUnitPower.text = highlightedUnitScript.maxPower.ToString() + "/" + highlightedUnitScript.maxPower.ToString();
                            }
                            else
                                UIUnitPower.text = highlightedUnitScript.power.ToString() + "/" + highlightedUnitScript.maxPower.ToString();
                        }
                    }
                    else
                    {
                        structureBeingDisplayed = hit.transform.GetComponent<ClickableTile>().structureOnTile;
                        UIUnitCurrentHealth.text = structureBeingDisplayed.GetComponent<Structure>().currHp.ToString();
                    }
                     
                    um.battle01.text = "";
                    um.battle02.text = "";
                    if ((unitBeingDisplayed != null && unitBeingDisplayed.GetComponent<Unit>().playerNum == 1) || (structureBeingDisplayed != null && structureBeingDisplayed.GetComponent<Structure>().playerNum == 1))
                    {
                        um.heart1.color = um.selectColor1;
                        um.heart2.color = um.selectColor1;
                        um.face1.color = um.selectColor1;
                        um.face2.color = um.selectColor1;
                        um.sword1.color = um.selectColor1;
                        um.sword2.color = um.selectColor2;
                    }
                    else if ((unitBeingDisplayed != null && unitBeingDisplayed.GetComponent<Unit>().playerNum == 2) || (structureBeingDisplayed != null && structureBeingDisplayed.GetComponent<Structure>().playerNum == 2))
                    {
                        um.heart1.color = um.selectColor2;
                        um.heart2.color = um.selectColor2;
                        um.face1.color = um.selectColor2;
                        um.face2.color = um.selectColor2;
                        um.sword1.color = um.selectColor2;
                        um.sword2.color = um.selectColor1;
                    }
                }

                else if ((hit.transform.GetComponent<ClickableTile>().unitOnTile != null 
                    || hit.transform.GetComponent<ClickableTile>().structureOnTile != null)
                    && hit.transform.GetComponent<ClickableTile>().unitOnTile != map.selectedUnit 
                    && map.selectedUnit != null)
                {
                    displayingUnitInfo = true;

                    int iniDmg = 0;
                    int recDmg = 0;
                    int iniRest = 0;

                    if (hit.transform.GetComponent<ClickableTile>().unitOnTile != null)
                    {
                        unitBeingDisplayed = hit.transform.GetComponent<ClickableTile>().unitOnTile;

                        iniDmg = map.selectedUnit.GetComponent<Unit>().weaponDamage[unitBeingDisplayed.GetComponent<Unit>().weaponID];
                        recDmg = unitBeingDisplayed.GetComponent<Unit>().weaponDamage[map.selectedUnit.GetComponent<Unit>().weaponID];
                        int recHp = unitBeingDisplayed.GetComponent<Unit>().currHp;
                        var highlightedUnitScript = unitBeingDisplayed.GetComponent<Unit>();

                        if (map.selectedUnit.GetComponent<Unit>().currHp != 100)
                        {
                            iniDmg = map.selectedUnit.GetComponent<Unit>().currHp * iniDmg / 100;
                        }
                        recHp -= iniDmg;

                        if (recHp != 100)
                        {
                            recDmg = recHp * recDmg / 100;
                        }

                        if (unitBeingDisplayed.GetComponent<Unit>().playerNum == 1)
                        { 
                            um.heart1.color = um.selectColor1;
                            um.heart2.color = um.selectColor1;
                            um.face1.color = um.selectColor1;
                            um.face2.color = um.selectColor1;
                            um.sword1.color = um.selectColor2;
                            um.sword2.color = um.selectColor1;
                        }
                        else if (unitBeingDisplayed.GetComponent<Unit>().playerNum == 2)
                        {
                            um.heart1.color = um.selectColor2;
                            um.heart2.color = um.selectColor2;
                            um.face1.color = um.selectColor2;
                            um.face2.color = um.selectColor2;
                            um.sword1.color = um.selectColor1;
                            um.sword2.color = um.selectColor2;
                        }
                    }
                    else if (hit.transform.GetComponent<ClickableTile>().structureOnTile != null)
                    {
                        structureBeingDisplayed = hit.transform.GetComponent<ClickableTile>().structureOnTile;
                        iniDmg = map.selectedUnit.GetComponent<Unit>().dmgStructure;
                        recDmg = structureBeingDisplayed.GetComponent<Structure>().dmg;

                        if (structureBeingDisplayed.GetComponent<Structure>().playerNum == 1)
                        {
                            um.heart1.color = um.selectColor1;
                            um.heart2.color = um.selectColor1;
                            um.face1.color = um.selectColor1;
                            um.face2.color = um.selectColor1;
                            um.sword1.color = um.selectColor2;
                            um.sword2.color = um.selectColor1;
                        }
                        else if (structureBeingDisplayed.GetComponent<Structure>().playerNum == 2)
                        {
                            um.heart1.color = um.selectColor2;
                            um.heart2.color = um.selectColor2;
                            um.face1.color = um.selectColor2;
                            um.face2.color = um.selectColor2;
                            um.sword1.color = um.selectColor1;
                            um.sword2.color = um.selectColor2;
                        }
                    }                            

                    if (iniDmg - 9 <= 0)
                        um.battle01.text = "1-" + iniDmg.ToString();
                    else
                        um.battle01.text = (iniDmg - 9).ToString() + "-" + iniDmg.ToString();

                    if (recDmg - 9 <= 0)
                        um.battle02.text = "1-" + recDmg.ToString();
                    else
                        um.battle02.text = (recDmg - 9).ToString() + "-" + recDmg.ToString();                 
                }
            }
        }

        else if (hit.transform.gameObject.CompareTag("Tile"))
        {
            if (hit.transform.GetComponent<ClickableTile>().unitOnTile == null)
            {
                displayingUnitInfo = false;
                UIUnitCurrentHealth.text = "";
                UIUnitPower.text = "";
                um.battle01.text = "";
                um.battle02.text = "";
            }
            else if (hit.transform.GetComponent<ClickableTile>().unitOnTile != unitBeingDisplayed)
            {
                displayingUnitInfo = false;
                UIUnitCurrentHealth.text = "";
                UIUnitPower.text = "";
                um.battle01.text = "";
                um.battle02.text = "";
            }
        }
    }

    public void UpdateColors()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        GameObject[] structuresOnGame = GameObject.FindGameObjectsWithTag("Structure");        

        foreach (GameObject unit in units)
        {
            if (unit.GetComponent<Unit>().playerNum == 1)
            {
                if (unit.GetComponent<Unit>().theColor != null)
                    unit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().color = um.selectColor1;
                unit.GetComponent<Unit>().minimap.GetComponent<SpriteRenderer>().color = um.selectColor1;
            }

            if (unit.GetComponent<Unit>().playerNum == 2)
            {
                if (unit.GetComponent<Unit>().theColor != null)
                    unit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().color = um.selectColor2;
                unit.GetComponent<Unit>().minimap.GetComponent<SpriteRenderer>().color = um.selectColor2;
            }
        }

        foreach (GameObject structure in structuresOnGame)
        {
            if (structure.GetComponent<Structure>().playerNum == 0)
            {
                structure.GetComponent<Structure>().minimap.GetComponent<SpriteRenderer>().color = Color.white;
                if (structure.GetComponent<Structure>().theColor != null)
                    structure.GetComponent<Structure>().theColor.GetComponent<SpriteRenderer>().color = Color.white;
            }

            else if (structure.GetComponent<Structure>().playerNum == 1)
            {
                structure.GetComponent<Structure>().minimap.GetComponent<SpriteRenderer>().color = um.selectColor1;
                if (structure.GetComponent<Structure>().theColor != null)
                    structure.GetComponent<Structure>().theColor.GetComponent<SpriteRenderer>().color = um.selectColor1;
            }

            else if (structure.GetComponent<Structure>().playerNum == 2)
            {
                structure.GetComponent<Structure>().minimap.GetComponent<SpriteRenderer>().color = um.selectColor2;
                if (structure.GetComponent<Structure>().theColor != null)
                    structure.GetComponent<Structure>().theColor.GetComponent<SpriteRenderer>().color = um.selectColor2;
            }
        }
    }

    void DayUpdate(int x)
    {
        if (currPlayer == 1)
        {
            um.day.text = "Day" + " " + x;
            ClimesUpdate();
        }
    }

    //climas
    void ClimesDoSomething()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        
        switch (um.clime.text)
        {
            case "Storm":
                foreach (GameObject u in units)
                {
                    u.GetComponent<Unit>().atkRange = u.GetComponent<Unit>().maxAtkRange;
                    u.GetComponent<Unit>().minAtkRange = u.GetComponent<Unit>().maxAtkRange;
                    if (u.GetComponent<Unit>().atkRange > 1 || u.GetComponent<Unit>().maxAtkRange > 1 || u.GetComponent<Unit>().minAtkRange > 1)
                    {
                        u.GetComponent<Unit>().atkRange = u.GetComponent<Unit>().atkRange - 1;
                        u.GetComponent<Unit>().minAtkRange = u.GetComponent<Unit>().minAtkRange - 1;
                    }
                }
                break;
            case "Rain":
                foreach (GameObject u in units)
                {
                    u.GetComponent<Unit>().atkRange = u.GetComponent<Unit>().maxAtkRange;
                    u.GetComponent<Unit>().minAtkRange = u.GetComponent<Unit>().maxAtkRange;
                    if (u.GetComponent<Unit>().playerNum == currPlayer)
                    {
                        if (u.GetComponent<Unit>().currHp - 10 <= 0)
                            u.GetComponent<Unit>().currHp = 1;
                        else
                            u.GetComponent<Unit>().currHp = u.GetComponent<Unit>().currHp - 10;
                        u.GetComponent<Unit>().UpdateHpUI();
                    }
                }
                break;
            default:
                foreach (GameObject u in units)
                {
                    u.GetComponent<Unit>().atkRange = u.GetComponent<Unit>().maxAtkRange;
                    u.GetComponent<Unit>().minAtkRange = u.GetComponent<Unit>().maxAtkRange;
                }
                break;
        }
    }

    void ClimesUpdate()
    {
        int r = Random.Range(0, climes.Length);
        if (currPlayer == 1)
        {
            um.prevClime.text = um.clime.text;
            um.clime.text = um.nextClime.text;
            um.nextClime.text = climes[r];
        }
    }
     
    //Fire
    void DamageFire()
    {  
        fires = GameObject.FindGameObjectsWithTag("Tile"); 
        foreach (GameObject fire in fires)
        {
            if(fire.GetComponent<ClickableTile>().fireOn == true)
            {
                if (fire.GetComponent<ClickableTile>().unitOnTile != null)                
                    fire.GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp -= 10;
            }
        }
    }

    void TimerFire()
    {
        fires = GameObject.FindGameObjectsWithTag("Tile");

        //Destoy trees
        int tX;
        int tY;

        foreach (GameObject fire in fires)
        {
            if (fire.GetComponent<ClickableTile>().fireOn == true)
            {
                fire.GetComponent<ClickableTile>().fireTime++;
                if (fire.GetComponent<ClickableTile>().fireTime == 3)
                {
                    //Destroy trees
                    tX = fire.GetComponent<ClickableTile>().tileX;
                    tY = fire.GetComponent<ClickableTile>().tileY;
                    if (fire.name == "Tree(Clone)" && fire.name == "Tree" && fire.transform.position.x == tX && fire.transform.position.y == tY)
                    {
                        fire.GetComponent<TileType>().name = "grass";
                    }

                    fire.GetComponent<ClickableTile>().fireOn = false;
                    fire.GetComponent<ClickableTile>().fireTime = 0;
                    fire.GetComponent<ClickableTile>().FireOnTile();
                }
            }
        }
    }

    public void UpdateUnits()
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        intUnits01 = 0;
        intUnits02 = 0;
        foreach (Unit unit in units)
        {
            if (unit.playerNum == 1)
            {
                intUnits01 += 1;
            }
            else if (unit.playerNum == 2)
            {
                intUnits02 += 1;
            }
        }
        Debug.Log("UpdateUnits 1 " + intUnits01);
        Debug.Log("UpdateUnits 2 " + intUnits02);
        um.unit1.text = intUnits01.ToString();
        um.unit2.text = intUnits02.ToString();
    }

    public void SelectColor(int color)
    {
        nm.SelectPosition(color);
    }
}