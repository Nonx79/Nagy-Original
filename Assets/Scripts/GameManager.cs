using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Photon.Pun;
using UnityEditor.U2D.Path.GUIFramework;
using static NetworkManager;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Players
    public int currPlayer = 1;
    public int totalPlayers = 2;
    public GameObject player1;
    public GameObject player2;
    
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

    public TileMap map;
    public IA ia;

    //Raycast
    private Ray ray;
    private RaycastHit hit;

    //Cursor Info for tileMapScript
    public int cursorX;
    public int cursorY;

    //currentTileBeingMousedOver
    public int selectedXTile;
    public int selectedYTile;

    //Canvas
    public Canvas UIunitCanvas;
    public Canvas canvasPlayers;
    public Canvas canvasPlayer1;
    public Canvas canvasPlayer2;

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

    //Texto UI
    public Text day;
    public Text moneyP1;
    public Text moneyP2;
    public Text incomeTextP1;
    public Text incomeTextP2;
    public Text unit1;
    public Text unit2;
    public GameObject unitP1;
    public GameObject unitP2;
    public Text prevClime;
    public Text clime;
    public Text nextClime;
    public Text battle01;
    public Text battle02;
    public Text commander;
    public Text faction;

    //Int ui units count
    public int intUnits01;
    public int intUnits02;

    int actualDay = 1;

    //Sprites UI
    public Sprite mSoldier, pSoldier, rSoldier;
    public GameObject bottonPrefab;

    public Image heart1, heart2, sword1, sword2, face1, face2, unitsCount01, unitsCount02;

    public GameObject UI;

    public Color selectColor1;
    public Color selectColor2;

    public Color morado = new Color(.2627451f, .01568628f, .345098f);
    public Color rosaIntenso = new Color(.7490196f, .01176471f, 2901961f);
    public Color rosa = new Color(.7490196f, .01176471f, .2901961f);
    public Color azulClaro = new Color(.2627451f, 1, 1);
    public Color yellow = new Color(.8431373f, .8117648f, .1333333f);
    public Color orange = new Color(.8584906f, .1727545f, 0);

    public string colorUnit;
    //int colorUnit;

    public GameObject player1Commander;
    public GameObject player2Commander;

    //Commanders
    public GameObject sniperCommander;
    public GameObject mechanicCommander;

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

    //Buttons
    public Button purple, pink, intensePink, amarillo, lightBlue, naranjo;

    //Online
    NetworkManager nm;

    private void Awake()
    {
        selectColor1 = morado;
        map.positionCommander1.GetComponent<SpriteRenderer>().color = morado;
        selectColor2 = rosa;
        map.positionCommander2.GetComponent<SpriteRenderer>().color = rosa;

        IncomeUpdate();
        MoneyUpdate();
        UpdateColors();
        DayUpdate(actualDay);

        prevClime.text = climes[0];
        clime.text = climes[0];
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            CursorUIUpdate();
            UnitUIUpdate();
        }                

        if(player1.transform.childCount != intUnits01)
        {
            UpdateUnits();
        }

        if (player2.transform.childCount != intUnits02)
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

                            c.transform.position = player1.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                        case 1:
                            track1.GetComponent<AudioSource>().enabled = false;
                            track2.GetComponent<AudioSource>().enabled = true;

                            c.transform.position = player1.transform.GetChild(0).transform.position;
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

                            c.transform.position = player2.transform.GetChild(0).transform.position;
                            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, -9.530531f);
                            break;
                        case 1:
                            track2.GetComponent<AudioSource>().enabled = true;
                            track1.GetComponent<AudioSource>().enabled = false;

                            c.transform.position = player2.transform.GetChild(0).transform.position;
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
            teamToReturn = player1;
        }
        else if (i == 2)
        {
            teamToReturn = player2;
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
        moneyP1.text = moneyPlayer1.ToString();
        moneyP2.text = moneyPlayer2.ToString();
        incomeTextP1.text = "+" + incomePlayer1.ToString();
        incomeTextP2.text = "+" + incomePlayer2.ToString();
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
                     
                    battle01.text = "";
                    battle02.text = "";
                    if ((unitBeingDisplayed != null && unitBeingDisplayed.GetComponent<Unit>().playerNum == 1) || (structureBeingDisplayed != null && structureBeingDisplayed.GetComponent<Structure>().playerNum == 1))
                    {
                        heart1.color = selectColor1;
                        heart2.color = selectColor1;
                        face1.color = selectColor1;
                        face2.color = selectColor1;
                        sword1.color = selectColor1;
                        sword2.color = selectColor2;
                    }
                    else if ((unitBeingDisplayed != null && unitBeingDisplayed.GetComponent<Unit>().playerNum == 2) || (structureBeingDisplayed != null && structureBeingDisplayed.GetComponent<Structure>().playerNum == 2))
                    {
                        heart1.color = selectColor2;
                        heart2.color = selectColor2;
                        face1.color = selectColor2;
                        face2.color = selectColor2;
                        sword1.color = selectColor2;
                        sword2.color = selectColor1;
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
                            heart1.color = selectColor1;
                            heart2.color = selectColor1;
                            face1.color = selectColor1;
                            face2.color = selectColor1;
                            sword1.color = selectColor2;
                            sword2.color = selectColor1;
                        }
                        else if (unitBeingDisplayed.GetComponent<Unit>().playerNum == 2)
                        {
                            heart1.color = selectColor2;
                            heart2.color = selectColor2;
                            face1.color = selectColor2;
                            face2.color = selectColor2;
                            sword1.color = selectColor1;
                            sword2.color = selectColor2;
                        }
                    }
                    else if (hit.transform.GetComponent<ClickableTile>().structureOnTile != null)
                    {
                        structureBeingDisplayed = hit.transform.GetComponent<ClickableTile>().structureOnTile;
                        iniDmg = map.selectedUnit.GetComponent<Unit>().dmgStructure;
                        recDmg = structureBeingDisplayed.GetComponent<Structure>().dmg;

                        if (structureBeingDisplayed.GetComponent<Structure>().playerNum == 1)
                        {
                            heart1.color = selectColor1;
                            heart2.color = selectColor1;
                            face1.color = selectColor1;
                            face2.color = selectColor1;
                            sword1.color = selectColor2;
                            sword2.color = selectColor1;
                        }
                        else if (structureBeingDisplayed.GetComponent<Structure>().playerNum == 2)
                        {
                            heart1.color = selectColor2;
                            heart2.color = selectColor2;
                            face1.color = selectColor2;
                            face2.color = selectColor2;
                            sword1.color = selectColor1;
                            sword2.color = selectColor2;
                        }
                    }                            

                    if (iniDmg - 9 <= 0)
                        battle01.text = "1-" + iniDmg.ToString();
                    else
                        battle01.text = (iniDmg - 9).ToString() + "-" + iniDmg.ToString();

                    if (recDmg - 9 <= 0)
                        battle02.text = "1-" + recDmg.ToString();
                    else
                        battle02.text = (recDmg - 9).ToString() + "-" + recDmg.ToString();                 
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
                battle01.text = "";
                battle02.text = "";
            }
            else if (hit.transform.GetComponent<ClickableTile>().unitOnTile != unitBeingDisplayed)
            {
                displayingUnitInfo = false;
                UIUnitCurrentHealth.text = "";
                UIUnitPower.text = "";
                battle01.text = "";
                battle02.text = "";
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
                    unit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().color = selectColor1;
                unit.GetComponent<Unit>().minimap.GetComponent<SpriteRenderer>().color = selectColor1;
            }

            if (unit.GetComponent<Unit>().playerNum == 2)
            {
                if (unit.GetComponent<Unit>().theColor != null)
                    unit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().color = selectColor2;
                unit.GetComponent<Unit>().minimap.GetComponent<SpriteRenderer>().color = selectColor2;
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
                structure.GetComponent<Structure>().minimap.GetComponent<SpriteRenderer>().color = selectColor1;
                if (structure.GetComponent<Structure>().theColor != null)
                    structure.GetComponent<Structure>().theColor.GetComponent<SpriteRenderer>().color = selectColor1;
            }

            else if (structure.GetComponent<Structure>().playerNum == 2)
            {
                structure.GetComponent<Structure>().minimap.GetComponent<SpriteRenderer>().color = selectColor2;
                if (structure.GetComponent<Structure>().theColor != null)
                    structure.GetComponent<Structure>().theColor.GetComponent<SpriteRenderer>().color = selectColor2;
            }
        }
    }

    public void IAPlayer()
    {
        if (canvasPlayer1.enabled == true)
        {
            IAPlayer1 = true;
        }
        else if (canvasPlayer2.enabled == true)
        {
            IAPlayer2 = true;
        }
    }

    public void HumanPlayer()
    {
        if (canvasPlayer1.enabled == true)
        {
            IAPlayer1 = false;
        }
        else if (canvasPlayer2.enabled == true)
        {
            IAPlayer2 = false;
        }
    }

    void ClimesUpdate()
    {
        int r = Random.Range(0, climes.Length);
        if(currPlayer == 1)
        {
            prevClime.text = clime.text;
            clime.text = nextClime.text;
            nextClime.text = climes[r];          
        }
    }

    void DayUpdate(int x)
    {
        if (currPlayer == 1)
        {
            day.text = "Day" + " " + x;
            ClimesUpdate();
        }
    }

    void ClimesDoSomething()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        
        switch (clime.text)
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

    public void SniperButton()
    {
        commander.text = "Sniper: Cuenta con un daño menor que los demas comandantes de su faccion, pero tiene 2 de alcance. Su concentración es un ataque con gran daño y 7 de alcance.";
        faction.text = "Military: Facción con unidades más fuertes en ataque y defensa.";
        if (canvasPlayer1.enabled == true)
        {
            player1Commander = sniperCommander;            
        }
        else if (canvasPlayer2.enabled == true)
            player2Commander = sniperCommander;
    }

    public void MechanicButton()
    {
        commander.text = "Mechanic: Su grito de batalla invoca un robor listo para el ataque.";
        faction.text = "Povery: Facción con unidades más debiles en ataque y defensa, pero pueden invocar 2 unidades en un edificio.";
        if (canvasPlayer1.enabled == true)
            player1Commander = mechanicCommander;
        else if (canvasPlayer2.enabled == true)
            player2Commander = mechanicCommander;
    }

    public void ColorPurple()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = morado;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = morado;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = morado;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != morado)
        {
            selectColor2 = morado;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = morado;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = morado;
            }
        }
        colorUnit = "Purple";
        UpdateColors();
    }

    public void ColorPink()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = rosa;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = rosa;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = rosa;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != rosa)
        {
            selectColor2 = rosa;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = rosa;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = rosa;
            }
        }
        colorUnit = "Pink";
        UpdateColors();
    }

    public void ColorIntensePink()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = rosaIntenso;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = rosaIntenso;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = rosaIntenso;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != rosaIntenso)
        {
            selectColor2 = rosaIntenso;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = rosaIntenso;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = rosaIntenso;
            }
        }
        colorUnit = "IntensePink";
        UpdateColors();
    }

    public void ColorYellow()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = yellow;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = yellow;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = yellow;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != yellow)
        {
            selectColor2 = yellow;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = yellow;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = yellow;
            }
        }
        colorUnit = "Yellow";
        UpdateColors();
    }

    public void ColorLightBlue()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = azulClaro;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = azulClaro;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = azulClaro;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != azulClaro)
        {
            selectColor2 = azulClaro;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = azulClaro;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = azulClaro;
            }
        }
        colorUnit = "LightBlue";
        UpdateColors();
    }

    public void ColorOrange()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = orange;
            map.positionCommander1.GetComponent<SpriteRenderer>().color = orange;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = orange;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != orange)
        {
            selectColor2 = orange;
            map.positionCommander2.GetComponent<SpriteRenderer>().color = orange;
            foreach (GameObject pUnit in map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = orange;
            }
        }
        colorUnit = "Orange";
        UpdateColors();
    }

    public void Confirm()
    {
        if (canvasPlayer1.enabled != false && player1Commander != null)
        {
            commander.text = "";
            faction.text = "";
            canvasPlayer1.enabled = false;
            canvasPlayer2.enabled = true;
            colorUnit = "Purple";
            
        }
        else if (canvasPlayer2.enabled == true && totalPlayers >= 3)
        {

        }
        else if (player2Commander != null)
        {
            Destroy(canvasPlayer1.transform.gameObject);
            Destroy(canvasPlayer2.transform.gameObject);
            Destroy(canvasPlayers.transform.gameObject);

            GameObject obj1 = Instantiate(player1Commander, map.positionCommander1.transform.position, Quaternion.identity);            
            GameObject obj2 = Instantiate(player2Commander, map.positionCommander2.transform.position, Quaternion.identity);

            Destroy(map.positionCommander1);
            Destroy(map.positionCommander2);            

            obj1.GetComponent<Unit>().playerNum = 1;
            if (IAPlayer1 == true)
            {
                obj1.GetComponent<Unit>().ia = true;
            }
            obj1.transform.SetParent(player1.transform);
            obj2.GetComponent<Unit>().playerNum = 2;
            if (IAPlayer2 == true)
            {     
                obj2.GetComponent<Unit>().ia = true;
            }
            obj2.transform.SetParent(player2.transform);

            GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
            foreach (GameObject s in structures)
            {
                if (s.GetComponent<Structure>().playerNum == 1)
                    s.GetComponent<Structure>().faction = player1Commander.GetComponent<Unit>().faction;
                else if (s.GetComponent<Structure>().playerNum == 2)
                    s.GetComponent<Structure>().faction = player2Commander.GetComponent<Unit>().faction;
            }

            creation = true;
            UpdateColors();
            heart1.color = selectColor1;
            heart2.color = selectColor1;
            face1.color = selectColor1;
            face2.color = selectColor1;
            sword1.color = selectColor1;
            sword2.color = selectColor2;
            unitsCount01.color = selectColor1;
            unitsCount02.color = selectColor2;

            if (obj1.GetComponent<Unit>().ia == true)
            {
                ia.StartTurn();
            }

            switch(player1Commander.GetComponent<Unit>().faction)
            {
                case 0:track1.GetComponent<AudioSource>().enabled = true;
                    break;
                case 1: track2.GetComponent<AudioSource>().enabled = true;
                    break;
            }            
        }        
    }

    public void BackButton()
    {
        if (canvasPlayer2.enabled == true)
        {
            canvasPlayer2.enabled = false;
            canvasPlayer1.enabled = true;
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
        unit1.text = intUnits01.ToString();
        unit2.text = intUnits02.ToString();
    }

    public void ShowTeamSelectionScreen()
    {

    }

    public void SelectCmd()
    {

    }

    public void RestricPositionChoise(ColorOfPlayer occupiedPosition)
    {
        var buttonToDesactivate = occupiedPosition == ColorOfPlayer.purple ? purple : pink;
        buttonToDesactivate.interactable = false;
    }
}