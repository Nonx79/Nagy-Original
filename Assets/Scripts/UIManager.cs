using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    string mapName;

    //Online
    public NetworkManager nm;

    public TMP_Dropdown gameMapSelection;

    private void Awake()
    {
        nm = FindObjectOfType<NetworkManager>();
        gameMapSelection.AddOptions(Enum.GetNames(typeof(MapLevel)).ToList());
        Screen.SetResolution(683, 384, false);
        
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
}