using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MultiplayerGameManger : GameManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            CursorUIUpdate();
            UnitUIUpdate();
        }

        if (player1.transform.childCount != intUnits01)
        {
            UpdateUnits();
        }

        if (player2.transform.childCount != intUnits02)
        {
            UpdateUnits();
        }

        if (purple != null)
        {
            nm.PreparePositionSelectionoptions();
        }
        //Online Colors
        if ((player2Commander == null && player1Commander == null) && canvasWait.enabled == false)
        {

        }
    }
}
