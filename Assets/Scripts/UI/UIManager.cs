using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine.SocialPlatforms;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Diagnostics;
using Unity.VisualScripting;
using static NetworkManager;
using System.ComponentModel;

public class UIManager : MonoBehaviourPun
{
    string mapName;

    public NetworkManager nm;
    public GameManager gm;

    //Canvas
    public Canvas UIunitCanvas;
    public Canvas canvasPlayers;
    public Canvas canvasPlayer1;
    public Canvas canvasPlayer2;
    public Canvas canvasWait; //Canvas online
    public TMP_Dropdown gameMapSelection;
    public Canvas canvasLoading;

    //Texto UI
    public Text day, prevClime, clime, nextClime, //Days
        moneyP1, moneyP2, incomeTextP1, incomeTextP2, //Money
        unit1, unit2, //Units
        battle01, battle02, commander, faction; //Battle
    public GameObject unitP1, unitP2;

    //Sprites UI
    public Sprite mSoldier, pSoldier, rSoldier;
    public GameObject bottonPrefab, UI;

    public Image heart1, heart2, sword1, sword2, face1, face2, unitsCount01, unitsCount02;

    public Color selectColor1, selectColor2;

    //Colors 
    public Color cPurple = new Color(.2627451f, .01568628f, .345098f);
    public Color cIntensePink = new Color(.7490196f, .01176471f, 2901961f);
    public Color cPink = new Color(.7490196f, .01176471f, .2901961f);
    public Color cLightBlue = new Color(.2627451f, 1, 1);
    public Color cYellow = new Color(.8431373f, .8117648f, .1333333f);
    public Color cOrange = new Color(.8584906f, .1727545f, 0);

    //Buttons UI
    public Button purple, pink, intensePink, yellow, lightBlue, naranjo;

    //Commanders Buttons
    public GameObject sniperCommander;
    public GameObject mechanicCommander;

    //Sync of ready
    private const byte MarkObjectEvent = 1;

    //Positions
    GameObject[] positions;

    private void Awake()
    {
        nm = FindObjectOfType<NetworkManager>();

        gameMapSelection.AddOptions(Enum.GetNames(typeof(MapLevel)).ToList());
        Screen.SetResolution(683, 384, false);

        //Multiplayer canvas
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void Update()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectOfType<GameManager>() != null && gm == null)
        {
            gm = FindObjectOfType<GameManager>();
            selectColor1 = cPurple;
            positions = GameObject.FindGameObjectsWithTag("Position");
            foreach (GameObject pos in positions)
            {
                if (pos.name == "PositionPlayer1Commander")
                    pos.GetComponent<SpriteRenderer>().color = cPurple;
                if (pos.name == "PositionPlayer2Commander")
                    pos.GetComponent<SpriteRenderer>().color = cPink;
            }
            selectColor2 = cPink;
            gm.UpdateColors();
        }

        if (gm != null)
        {
            if (gm.player2Ready == true && gm.player1Ready == true)
            {
                canvasWait.enabled = false;
                InitializeGame();
                gm.player1Ready = false;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Map01()
    {
        mapName = "Map01";
    }

    public void Map02()
    {
        mapName = "Loading";
    }

    public void ConfirmMapButton()
    {
        LoadScene(mapName);
    }

    public void Connected()
    {
        nm.SetPlayerLevel((MapLevel)gameMapSelection.value);
        nm.Connect();
    }

    public void SelectPosition(int num)
    {
        nm.SelectPosition(num);
    }

    //Color Button
    public void ColorPurple()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cPurple;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cPurple;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cPurple;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cPurple)
        {
            selectColor2 = cPurple;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cPurple;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cPurple;
            }
        }
        gm.colorUnit = "Purple";
        gm.UpdateColors();
        gm.SelectColor(0);
        nm.PreparePositionSelectionoptions();
    }

    public void ColorPink()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cPink;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cPink;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cPink;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cPink)
        {
            selectColor2 = cPink;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cPink;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cPink;
            }
        }
        gm.colorUnit = "Pink";
        gm.UpdateColors();
        gm.SelectColor(1);
        nm.PreparePositionSelectionoptions();
    }

    public void ColorIntensePink()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cIntensePink;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cIntensePink;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cIntensePink;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cIntensePink)
        {
            selectColor2 = cIntensePink;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cIntensePink;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cIntensePink;
            }
        }
        gm.colorUnit = "IntensePink";
        gm.UpdateColors();
        gm.SelectColor(2);
        nm.PreparePositionSelectionoptions();
    }

    public void ColorYellow()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cYellow;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cYellow;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cYellow;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cYellow)
        {
            selectColor2 = cYellow;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cYellow;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cYellow;
            }
        }
        gm.colorUnit = "Yellow";
        gm.UpdateColors();
        gm.SelectColor(3);
        nm.PreparePositionSelectionoptions();
    }

    public void ColorLightBlue()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cLightBlue;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cLightBlue;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cLightBlue;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cLightBlue)
        {
            selectColor2 = cLightBlue;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cLightBlue;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cLightBlue;
            }
        }
        gm.colorUnit = "LightBlue";
        gm.UpdateColors();
        gm.SelectColor(4);
        nm.PreparePositionSelectionoptions();
    }

    public void ColorOrange()
    {
        if (canvasPlayer1.enabled != false)
        {
            selectColor1 = cOrange;
            gm.map.positionCommander1.GetComponent<SpriteRenderer>().color = cOrange;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "1")
                    pUnit.GetComponent<SpriteRenderer>().color = cOrange;
            }
        }
        else if (canvasPlayer2.enabled != false && selectColor1 != cOrange)
        {
            selectColor2 = cOrange;
            gm.map.positionCommander2.GetComponent<SpriteRenderer>().color = cOrange;
            foreach (GameObject pUnit in gm.map.positionSoldiers)
            {
                if (pUnit.name == "2")
                    pUnit.GetComponent<SpriteRenderer>().color = cOrange;
            }
        }
        gm.colorUnit = "Orange";
        gm.UpdateColors();
        gm.SelectColor(5);
        nm.PreparePositionSelectionoptions();
    }

    //Ia or Human
    public void IAPlayer()
    {
        if (canvasPlayer1.enabled == true)
        {
            gm.IAPlayer1 = true;
        }
        else if (canvasPlayer2.enabled == true)
        {
            gm.IAPlayer2 = true;
        }
    }

    public void HumanPlayer()
    {
        if (canvasPlayer1.enabled == true)
        {
            gm.IAPlayer1 = false;
        }
        else if (canvasPlayer2.enabled == true)
        {
            gm.IAPlayer2 = false;
        }
    }

    //Commander
    //Military
    public void SniperButton()
    {
        commander.text = "Sniper: Cuenta con un daño menor que los demas comandantes de su faccion, pero tiene 2 de alcance. Su concentración es un ataque con gran daño y 7 de alcance.";
        faction.text = "Military: Facción con unidades más fuertes en ataque y defensa.";
        if (canvasPlayer1.enabled == true)
        {
            gm.player1Commander = sniperCommander;
        }
        else if (canvasPlayer2.enabled == true)
            gm.player2Commander = sniperCommander;
    }

    //Povery
    public void MechanicButton()
    {
        commander.text = "Mechanic: Su grito de batalla invoca un robor listo para el ataque.";
        faction.text = "Povery: Facción con unidades más debiles en ataque y defensa, pero pueden invocar 2 unidades en un edificio.";
        if (canvasPlayer1.enabled == true)
            gm.player1Commander = mechanicCommander;
        else if (canvasPlayer2.enabled == true)
            gm.player2Commander = mechanicCommander;
    }

    //Canvas players

    //Confirm/Back
    public void Confirm()
    {
        if (canvasPlayer1.enabled != false && gm.player1Commander != null)
        {
            commander.text = "";
            faction.text = "";
            canvasPlayer1.enabled = false;
            canvasWait.enabled = true;
            if (gm.multiplayer != true)
                canvasPlayer2.enabled = true;
            else
            {
                gm.player1Ready = true;
            }
            gm.colorUnit = "Purple";

            PhotonNetwork.RaiseEvent(MarkObjectEvent, gm.player1Ready, RaiseEventOptions.Default, SendOptions.SendReliable);

            UnityEngine.Debug.Log("Player 2 ready: " + gm.player2Ready);


            if (gm.player2Ready == true && gm.player1Ready == true)
            {
                canvasWait.enabled = false;
            }
            gm.IncomeUpdate();
            gm.DayUpdate(gm.actualDay);
            gm.MoneyUpdate();
            prevClime.text = gm.climes[0];
            clime.text = gm.climes[0];
        }
        else if (canvasPlayer2.enabled != false && gm.player2Commander != null)
        {
            commander.text = "";
            faction.text = "";
            canvasPlayer2.enabled = false;
            gm.player2Ready = true;
            canvasWait.enabled = true;

            PhotonNetwork.RaiseEvent(MarkObjectEvent, gm.player2Ready, RaiseEventOptions.Default, SendOptions.SendReliable);

            UnityEngine.Debug.Log("Player 1 ready: " + gm.player1Ready);

            if (gm.player2Ready == true && gm.player1Ready == true)
            {
                canvasWait.enabled = false;
            }
            gm.IncomeUpdate();
            gm.DayUpdate(gm.actualDay);
            gm.MoneyUpdate();            
            prevClime.text = gm.climes[0];
            clime.text = gm.climes[0];
        }
        if ((gm.player2Commander != null && gm.player1Commander != null)
            && canvasWait.enabled == true
            )
        {            
        }
    }

    public void InitializeGame()
    {
        Destroy(canvasPlayer1.transform.gameObject);
        Destroy(canvasPlayer2.transform.gameObject);
        Destroy(canvasPlayers.transform.gameObject);
        Destroy(canvasWait.transform.gameObject);

        GameObject obj1 = Instantiate(gm.player1Commander, gm.map.positionCommander1.transform.position, Quaternion.identity);
        GameObject obj2 = Instantiate(gm.player2Commander, gm.map.positionCommander2.transform.position, Quaternion.identity);

        UnityEngine.Debug.Log("Crea unidades! " + gm.player1Commander + "" + gm.player2Commander);

        Destroy(gm.map.positionCommander1);
        Destroy(gm.map.positionCommander2);

        obj1.GetComponent<Unit>().playerNum = 1;
        if (gm.IAPlayer1 == true)
        {
            obj1.GetComponent<Unit>().ia = true;
        }
        obj1.transform.SetParent(gm.containerPlayer1.transform);
        obj2.GetComponent<Unit>().playerNum = 2;
        if (gm.IAPlayer2 == true)
        {
            obj2.GetComponent<Unit>().ia = true;
        }
        obj2.transform.SetParent(gm.containerPlayer2.transform);

        GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
        foreach (GameObject s in structures)
        {
            if (s.GetComponent<Structure>().playerNum == 1)
                s.GetComponent<Structure>().faction = gm.player1Commander.GetComponent<Unit>().faction;
            else if (s.GetComponent<Structure>().playerNum == 2)
                s.GetComponent<Structure>().faction = gm.player2Commander.GetComponent<Unit>().faction;
        }

        gm.creation = true;
        gm.UpdateColors();
        heart1.color = selectColor1;
        heart2.color = selectColor1;
        face1.color = selectColor1;
        face2.color = selectColor1;
        sword1.color = selectColor1;
        sword2.color = selectColor2;
        unitsCount01.color = selectColor1;
        unitsCount02.color = selectColor2;

        gm.map.SetIfTileIsOccupied();

        if (obj1.GetComponent<Unit>().ia == true)
        {
            gm.ia.StartTurn();
        }

        switch (gm.player1Commander.GetComponent<Unit>().faction)
        {
            case 0:
                gm.track1.GetComponent<AudioSource>().enabled = true;
                break;
            case 1:
                gm.track2.GetComponent<AudioSource>().enabled = true;
                break;
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

    //Online canvas 
    public void ShowTeamSelectionScreenFirst()
    {
        canvasPlayer1.enabled = true;
        canvasPlayer2.enabled = false;
    }

    public void ShowTeamSelectionScreenSecond()
    {
        canvasPlayer2.enabled = true;
        canvasPlayer1.enabled = false;
    }
    //Online ready real time
    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == MarkObjectEvent)
        {

            if (gm != null)
            {
                // Lógica para manejar el evento
                gm.player1Ready = (bool)photonEvent.CustomData;
                gm.player2Ready = (bool)photonEvent.CustomData;
            }
        }
    }
    public void RestricPositionChoise(ColorOfPlayer occupiedPosition)
    {
        if (occupiedPosition == ColorOfPlayer.purple)
        {
            purple.interactable = false;
        }
        else
        {
            purple.interactable = true;
        }
        if (occupiedPosition == ColorOfPlayer.pink)
        {
            pink.interactable = false;
        }
        else
        {
            pink.interactable = true;
        }
        if (occupiedPosition == ColorOfPlayer.intensePink)
        {
            intensePink.interactable = false;
        }
        else
        {
            intensePink.interactable = true;
        }
        if (occupiedPosition == ColorOfPlayer.yellow)
        {
            yellow.interactable = false;
        }
        else
        {
            yellow.interactable = true;
        }
        if (occupiedPosition == ColorOfPlayer.lightBlue)
        {
            lightBlue.interactable = false;
        }
        else
        {
            lightBlue.interactable = true;
        }
        if (occupiedPosition == ColorOfPlayer.orange)
        {
            naranjo.interactable = false;
        }
        else
        {
            naranjo.interactable = true;
        }
    }
}