using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentsUi : MonoBehaviour
{    
    public UIManager um;
    public GameManager gm;
    public TileMap tm;

    [Header("UiManager")]
    //Canvas
    public Canvas UIunitCanvas, canvasPlayers,canvasPlayer1, canvasPlayer2, canvasWait, canvasLoading; //Canvas online

    //Texto UI
    public Text day, prevClime, clime, nextClime, //Days
        moneyP1, moneyP2, incomeTextP1, incomeTextP2, //Money
        unit1, unit2, //Units
        battle01, battle02, commander, faction; //Battle
    public GameObject unitP1, unitP2; //???

    //Sprites UI
    public Sprite mSoldier, pSoldier, rSoldier; //???
    public GameObject bottonPrefab, UI; //???

    public Image heart1, heart2, sword1, sword2, face1, face2, unitsCount01, unitsCount02;

    //public Color selectColor1, selectColor2;

    //Buttons UI
    public Button purple, pink, intensePink, yellow, lightBlue, naranjo;

    //Commanders gameObject
    public GameObject sniperCommander, mechanicCommander;
    
    //[Header("GameManager")]
    public GameObject containerPlayer1, containerPlayer2;

    public Text UIUnitCurrentHealth, UIUnitPower;

    [Header("TilemapManager")]
    public GameObject unitsOnBoard;
    public GameObject position;

    public void SetDependences()
    {
        um = FindObjectOfType<UIManager>();
        gm = FindObjectOfType<GameManager>();
        tm = FindObjectOfType<TileMap>();
    }

    public void StartComponents()
    {
        //Ui
        //Canvas
        um.UIunitCanvas = UIunitCanvas;
        um.canvasPlayers = canvasPlayers;
        um.canvasPlayer1 = canvasPlayer1;
        um.canvasPlayer2 = canvasPlayer2;
        um.canvasWait = canvasWait;
        um.canvasLoading = canvasLoading;

        //Text
        //Days
        um.day = day;
        um.prevClime = prevClime;
        um.clime = clime;
        um.nextClime = nextClime; 
        //Money
        um.moneyP1 = moneyP1;
        um.moneyP2 = moneyP2;
        um.incomeTextP1 = incomeTextP1;
        um.incomeTextP2 = incomeTextP2;
        //Units
        um.unit1 = unit1;
        um.unit2 = unit2;
        //Battle
        um.battle01 = battle01;
        um.battle02 = battle02;
        um.commander = commander;
        um.faction = faction;

        //Sprites
        um.heart1 = heart1;
        um.heart2 = heart2;
        um.sword1 = sword1;
        um.sword2 = sword2;
        um.face1 = face1;
        um.face2 = face2;
        um.unitsCount01 = unitsCount01;
        um.unitsCount02 = unitsCount02;

        //Buttons
        um.purple = purple;
        um.pink = pink;
        um.intensePink = intensePink;
        um.yellow = yellow;
        um.lightBlue = lightBlue;
        um.naranjo = naranjo;

        //Commander
        um.sniperCommander = sniperCommander;
        um.mechanicCommander = mechanicCommander;

        //GameManager
        //Container
        gm.containerPlayer1 = containerPlayer1;
        gm.containerPlayer2 = containerPlayer2;

        //UI healt and power
        gm.UIUnitCurrentHealth = UIUnitCurrentHealth;
        gm.UIUnitPower = UIUnitPower;

        gm.UpdateColors();

        //Tilemap
        tm.unitsOnBoard = unitsOnBoard;
    }

    //Confirm and back
    public void Confirm()
    {
        um.Confirm();
    }

    public void BackButton()
    {
        um.BackButton();
    }

    //Commanders
    //Military
    public void SniperButton()
    {
        um.SniperButton();
    }

    //Povery
    public void MechanicButton()
    {
        um.MechanicButton();
    }

    //Colors
    public void ColorPurple()
    {
        um.ColorPurple();
    }

    public void ColorPink()
    {
        um.ColorPink();
    }

    public void ColorIntensePink()
    {
        um.ColorIntensePink();
    }

    public void ColorYellow()
    {
        um.ColorYellow();
    }

    public void ColorLightBlue()
    {
        um.ColorLightBlue();
    }

    public void ColorOrange()
    {
        um.ColorOrange();
    }
}

