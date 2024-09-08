using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    GameObject[] units;
    GameObject[] structures;
    GameObject uu;
    public GameObject structure;
    public TileMap map;
    public GameManager gm;
    public BattleManager bm;
    public GameObject[] unitsToAttack;
    public GameObject unitToAttack;

    int boardX;
    int boardY;

    public bool attack = false;

    int x = 1;
    int y;
    int z;
    public void StartTurn()
    {
        if (map.selectedUnit != null)
        {
            map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().unitPreviousX, map.selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
            map.tilesOnMap[(int)map.selectedUnit.transform.position.x, (int)map.selectedUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile = map.selectedUnit;
            map.selectedUnit.GetComponent<Unit>().unitPreviousX = map.selectedUnit.GetComponent<Unit>().tileX;
            map.selectedUnit.GetComponent<Unit>().unitPreviousY = map.selectedUnit.GetComponent<Unit>().tileY;
            map.selectedUnit = null;
        }
        Debug.Log("Existo");
        if (gm.IAPlayer1)
        {
            structures = GameObject.FindGameObjectsWithTag("Structure");
            units = GameObject.FindGameObjectsWithTag("Unit");
            if (gm.IAPlayer2 == true)
            {

                y = gm.containerPlayer2.transform.childCount;
                GameObject[] units = new GameObject[y];
                structures = GameObject.FindGameObjectsWithTag("Structure");
                for (int i = 0; i < units.Length; i++)
                {
                    units[i] = gm.containerPlayer2.transform.GetChild(i).gameObject;

                    if (units[i].GetComponent<Unit>().ia == true && units[i].GetComponent<Unit>().waiting == false)
                    {
                        map.selectedUnit = units[i];
                        map.selectedUnit.GetComponent<Unit>().setMovementState(1);
                        highlightUnitRange();
                    }
                    else
                    {
                        foreach (GameObject s in structures)
                        {
                            if (s.GetComponent<Structure>().playerNum == gm.currPlayer && s.name == "Cuartel")
                            {
                                if (s.GetComponent<Structure>().waiting == false)
                                    InvokeUnit(s);
                            }
                        }
                    }

                }
            }
            // else
            // gm.EndTurn();
        }
        else if (gm.IAPlayer2)
        {
            structures = GameObject.FindGameObjectsWithTag("Structure");
            units = GameObject.FindGameObjectsWithTag("Unit");
            if (gm.IAPlayer2 == true)
            {
                
                y = gm.containerPlayer2.transform.childCount;
                GameObject[] units = new GameObject[y];
                structures = GameObject.FindGameObjectsWithTag("Structure");
                for (int i = 0; i < units.Length; i++)
                {
                    units[i] = gm.containerPlayer2.transform.GetChild(i).gameObject;

                    if (units[i].GetComponent<Unit>().ia == true && units[i].GetComponent<Unit>().waiting == false)
                    {
                        map.selectedUnit = units[i];
                        map.selectedUnit.GetComponent<Unit>().setMovementState(1);
                        highlightUnitRange();
                    }
                    

                }
                if (units[y - 1].GetComponent<Unit>().waiting == true)
                {
                    foreach (GameObject s in structures)
                    {
                        if (s.GetComponent<Structure>().playerNum == gm.currPlayer && s.name == "Cuartel")
                        {
                            if (s.GetComponent<Structure>().waiting == false)
                                InvokeUnit(s);
                        }
                    }
                }
            }
            // else
                // gm.EndTurn();
        }
    }

    public void highlightUnitRange()
    {
        HashSet<Node> finalMovementHighlight = new HashSet<Node>();
        HashSet<Node> totalAttackableTiles = new HashSet<Node>();
        HashSet<Node> finalEnemyUnitsInMovementRange = new HashSet<Node>();
        

        int attRange = map.selectedUnit.GetComponent<Unit>().minAtkRange;
        int moveSpeed = map.selectedUnit.GetComponent<Unit>().moveSpeed;


        Node unitInitialNode = map.graph[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY];
        finalMovementHighlight = map.getUnitMovementOptions();
        totalAttackableTiles = map.getUnitTotalAttackableTiles(finalMovementHighlight, attRange, unitInitialNode);

        //Configurar
        foreach (Node n in totalAttackableTiles)
        {

            if (map.tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile != null)
            {
                
                GameObject unitOnCurrentlySelectedTile = map.tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile;
                if (unitOnCurrentlySelectedTile.GetComponent<Unit>().playerNum != map.selectedUnit.GetComponent<Unit>().playerNum)
                {
                    finalEnemyUnitsInMovementRange.Add(n);
                    unitsToAttack = new GameObject[x];
                    for (int i = 0; i < unitsToAttack.Length; i++)
                    {                       
                        unitsToAttack[i] = unitOnCurrentlySelectedTile;
                        map.selectedUnit.GetComponent<Unit>().unitToBattle = true;
                    }
                }
            }    
        }        

        map.highlightEnemiesInRange(totalAttackableTiles);

        map.highlightMovementRange(finalMovementHighlight);

        map.selectedUnitMoveRange = finalMovementHighlight;

        map.selectedUnitTotalRange = map.getUnitTotalRange(finalMovementHighlight, totalAttackableTiles);

        if (map.selectedUnit.GetComponent<Unit>().unitToBattle != true)
        {
            if (selectTileToMoveTo())
            {
                Debug.Log("Me muevo");
                Path();
            }
        }
        else if (map.selectedUnit.GetComponent<Unit>().unitToBattle == true)
        {
            Attack();
            Debug.Log("identificado");

            if ((unitToAttack.transform.position.x == map.selectedUnit.transform.position.x && unitToAttack.transform.position.y != map.selectedUnit.transform.position.y)
            && (unitToAttack.transform.position.y == map.selectedUnit.transform.position.y && unitToAttack.transform.position.x != map.selectedUnit.transform.position.x))
            {
                StartCoroutine(moveUnitAndFinalize());
            }
            else if (selectTileToMoveTo())
            {
                Debug.Log("Me muevo");
                Path();
            }
        }
    }

    void Attack()
    {
        unitToAttack = unitsToAttack[0];
        for (int i = 0; i < unitsToAttack.Length; i++)
        {
            if (unitsToAttack[i].GetComponent<Unit>().currHp < unitToAttack.GetComponent<Unit>().currHp)
            {
                unitToAttack = unitsToAttack[i];
            }
        }
    }

    void Path()
    {
        StartCoroutine(moveOverSeconds(map.selectedUnit, map.selectedUnit.GetComponent<Unit>().currentPath[map.selectedUnit.GetComponent<Unit>().currentPath.Count - 1]));
        StartCoroutine(moveUnitAndFinalize());                  
    }

    void Battle()
    {
        attack = false;
        map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().unitPreviousX, map.selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
        map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().weaponDamage[unitToAttack.GetComponent<Unit>().weaponID];
        unitToAttack.GetComponent<Unit>().maxDmg = unitToAttack.GetComponent<Unit>().weaponDamage[map.selectedUnit.GetComponent<Unit>().weaponID];
        //map.selectedUnit.GetComponent<Weapons>().WeaponDamage(map.selectedUnit, unitToAttack);
        StartCoroutine(bm.GetComponent<BattleManager>().attack(map.selectedUnit, unitToAttack));

        map.selectedUnit.GetComponent<Unit>().unitPreviousX = map.selectedUnit.GetComponent<Unit>().tileX;
        map.selectedUnit.GetComponent<Unit>().unitPreviousY = map.selectedUnit.GetComponent<Unit>().tileY;
        map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = map.selectedUnit;
        /*
        if (map.selectedUnit.name == "pTruck" || map.selectedUnit.name == "pTruck(Clone)")
        {
            if (selectedUnit.transform.position.x - tempUnit.transform.position.x == -1 && selectedUnit.transform.position.y == tempUnit.transform.position.y)
            {
                if (tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
                    && tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null
                    && tileTypes[tiles[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y]].isWalkable)
                {
                    tempUnit.transform.position = new Vector3(tempUnit.transform.position.x + 1, tempUnit.transform.position.y);
                }
                else if (tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile != null)
                {
                    tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp = tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp - 10;
                    tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().UpdateHpUI();
                    if (tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp <= 0)
                    {
                        Destroy(tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile);
                        if (tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().lider == true)
                        {
                            gm.GameOver();
                        }
                    }
                }
            }
            else if (selectedUnit.transform.position.x - tempUnit.transform.position.x == 1 && selectedUnit.transform.position.y == tempUnit.transform.position.y)
            {
                if (tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
                    && tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null
                    && tileTypes[tiles[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y]].isWalkable)
                {
                    tempUnit.transform.position = new Vector3(tempUnit.transform.position.x - 1, tempUnit.transform.position.y);
                }
                else if (tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile != null)
                {
                    tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp = tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp - 10;
                    tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().UpdateHpUI();
                    if (tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp <= 0)
                    {
                        Destroy(tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile);
                        if (tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().lider == true)
                        {
                            gm.GameOver();
                        }
                    }
                }
            }
            else if (selectedUnit.transform.position.y - tempUnit.transform.position.y == -1 && selectedUnit.transform.position.x == tempUnit.transform.position.x)
            {
                if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile == null
                    && tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().structureOnTile == null
                    && tileTypes[tiles[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1]].isWalkable)

                    tempUnit.transform.position = new Vector3(tempUnit.transform.position.x, tempUnit.transform.position.y + 1);
                else if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile != null)
                {
                    tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp = tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp - 10;
                    tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().UpdateHpUI();
                    if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp <= 0)
                    {
                        Destroy(tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile);
                        if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().lider == true)
                        {
                            gm.GameOver();
                        }
                    }
                }
            }

            else if (selectedUnit.transform.position.y - tempUnit.transform.position.y == 1 && selectedUnit.transform.position.x == tempUnit.transform.position.x)
            {
                if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile == null
                    && tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().structureOnTile == null
                    && tileTypes[tiles[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1]].isWalkable)
                    tempUnit.transform.position = new Vector3(tempUnit.transform.position.x, tempUnit.transform.position.y - 1);
                else if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile != null)
                {
                    tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp = tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp - 10;
                    tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().UpdateHpUI();
                    if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().currHp <= 0)
                    {
                        Destroy(tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile);
                        if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().lider == true)
                        {
                            gm.GameOver();
                        }
                    }
                }
            }
            tilesOnMap[tempUnit.GetComponent<Unit>().unitPreviousX, tempUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
            tempUnit.GetComponent<Unit>().tileX = (int)tempUnit.transform.position.x;
            tempUnit.GetComponent<Unit>().tileY = (int)tempUnit.transform.position.y;
            tempUnit.GetComponent<Unit>().unitPreviousX = tempUnit.GetComponent<Unit>().tileX;
            tempUnit.GetComponent<Unit>().unitPreviousY = tempUnit.GetComponent<Unit>().tileY;
            tilesOnMap[tempUnit.GetComponent<Unit>().tileX, tempUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = tempUnit;
        }*/
    }

    void SelectStructureToAttack(GameObject s)
    {
        structures = GameObject.FindGameObjectsWithTag("Structure");
        for (int i = 0; i < structures.Length; i++)
        {
            if (i + 1 != structures.Length)
            {
                if (structures[i].GetComponent<Structure>().playerNum == 0 && structures[i + 1].GetComponent<Structure>().playerNum == 0)
                {
                    //X
                    int sIX  = (int)structures[i].transform.position.x;
                    int uIX  = (int)map.selectedUnit.transform.position.x;
                    int sIIX = (int)structures[i + 1].transform.position.x;
                    int uIIX = (int)map.selectedUnit.transform.position.x;
                    int tIX  = sIX - uIX;
                    int tIIX = sIIX - uIIX;
                    //Y
                    int sIY  = (int)structures[i].transform.position.y;
                    int uIY  = (int)map.selectedUnit.transform.position.y;
                    int sIIY = (int)structures[i + 1].transform.position.y;
                    int uIIY = (int)map.selectedUnit.transform.position.y;
                    int tIY  = sIY - uIY;
                    int tIIY = sIIY - uIIY;
                    if (tIX <= tIIX)
                    {
                        if (tIY <= tIIY)
                        {
                            Debug.Log("Selected structure");
                            structure = structures[i];
                            return;
                        }
                        else
                        {
                            Debug.Log("Selected structure");
                            structure = structures[i++];
                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("Selected structure");
                        structure = structures[i++];
                        return;
                    }
                }
            }
            else
            {
                structure = structures[i];
                return;
            }
        }
    }

    bool selectTileToMoveTo()
    {
        if (map.selectedUnit.GetComponent<Unit>().unitToBattle == false)
        {                        
            units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                for (int i = 0; i < units.Length; i++)
                {
                    if (u.GetComponent<Unit>().playerNum != gm.currPlayer)
                    {
                        unitsToAttack = new GameObject[x];
                        unitsToAttack[0] = u;
                        unitToAttack = u; 
                    }
                }
            }

            //Select a structure
            if (structure == null)
            {
                SelectStructureToAttack(structure);
            }
            //Move to a structure
            if (structure != null && structure.GetComponent<Structure>().playerNum == 0)
            {
                Debug.Log("ola");
                //Derecha a izq
                if ((structure.transform.position.x - map.selectedUnit.transform.position.x == 1)
                    || (structure.transform.position.x - map.selectedUnit.transform.position.x == - 1)
                    || (structure.transform.position.y - map.selectedUnit.transform.position.y == 1)
                    || (structure.transform.position.x - map.selectedUnit.transform.position.x == 1))
                {
                    GeneratePathTo((int)map.selectedUnit.transform.position.x, (int)map.selectedUnit.transform.position.y);
                }

                else if ((structure.transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.x - structure.transform.position.x)
                    && (structure.transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - structure.transform.position.y))
                {
                    GeneratePathTo((int)structure.transform.position.x + 1, (int)structure.transform.position.y);
                    return true;
                }
                else if ((structure.transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.x - structure.transform.position.x)
                    && (structure.transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - structure.transform.position.y))
                {
                    GeneratePathTo((int)structure.transform.position.x - 1, (int)structure.transform.position.y);
                    return true;
                }
                else if ((structure.transform.position.y - map.selectedUnit.transform.position.y < map.selectedUnit.transform.position.y - structure.transform.position.y)
                    && (structure.transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.y - structure.transform.position.y))
                {
                    GeneratePathTo((int)structure.transform.position.x, (int)structure.transform.position.y + 1);
                    return true;
                }
                else
                {
                    GeneratePathTo((int)structure.transform.position.x, (int)structure.transform.position.y - 1);
                    return true;
                }
            }

        }
        //Move
        if (map.selectedUnit.GetComponent<Unit>().unitToBattle == false
            && (unitToAttack.GetComponent<Unit>().playerNum != map.selectedUnit.GetComponent<Unit>().playerNum))
        {
            Debug.Log("micropene");

            if ((unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.x - unitsToAttack[0].transform.position.x)
                    && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y))
            {
                GeneratePathTo((int)unitsToAttack[0].transform.position.x + 1, (int)unitsToAttack[0].transform.position.y);
                return true;
            }
            else if ((unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.x - unitsToAttack[0].transform.position.x)
                && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y))
            {
                GeneratePathTo((int)unitsToAttack[0].transform.position.x - 1, (int)unitsToAttack[0].transform.position.y);
                return true;
            }
            else if ((unitsToAttack[0].transform.position.y - map.selectedUnit.transform.position.y < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y)
                && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y))
            {
                GeneratePathTo((int)unitsToAttack[0].transform.position.x, (int)unitsToAttack[0].transform.position.y + 1); 
                return true;
            }
            else
            {
                GeneratePathTo((int)unitsToAttack[0].transform.position.x, (int)unitsToAttack[0].transform.position.y - 1);
                return true;
            }            
        }
        //Attack
        if ((unitToAttack.GetComponent<Unit>().playerNum != map.selectedUnit.GetComponent<Unit>().playerNum)
           && map.selectedUnit.GetComponent<Unit>().unitToBattle != false)
        {
            Debug.Log("problem");
            if (((unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.x - unitsToAttack[0].transform.position.x)
                    && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y)))
            {
                GeneratePathTo((int)unitToAttack.transform.position.x + 1, (int)unitsToAttack[0].transform.position.y);
                return true;
            }
            else if ((unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.x - unitsToAttack[0].transform.position.x)
                && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y))
            {
                GeneratePathTo((int)unitToAttack.transform.position.x - 1, (int)unitsToAttack[0].transform.position.y);
                return true;
            }
            else if ((unitsToAttack[0].transform.position.y - map.selectedUnit.transform.position.y < map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y)
                && (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x > map.selectedUnit.transform.position.y - unitsToAttack[0].transform.position.y))
            {
                GeneratePathTo((int)unitToAttack.transform.position.x, (int)unitsToAttack[0].transform.position.y + 1);
                return true;
            }
            else
            {
                GeneratePathTo((int)unitToAttack.transform.position.x, (int)unitsToAttack[0].transform.position.y - 1);
                return true;
            }
        }
        else if (map.selectedUnit.GetComponent<Unit>().currHp <= 25)
        {
            int a = (int)unitsToAttack[0].transform.position.x;
            int b = (int)map.selectedUnit.transform.position.x;
            int c;
            int d = map.selectedUnit.GetComponent<Unit>().moveSpeed;

            //Mover de izquierda a derecha
            if (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x <= -1
                && unitsToAttack[0].transform.position.y - map.selectedUnit.transform.position.y == 0)
            {
                c = d - (int)CostToEnterTile(b, (int)map.selectedUnit.transform.position.y, b + d, (int)unitsToAttack[0].transform.position.y);

                if (map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
                    && map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null)
                {
                    Node nodeToCheck = map.graph[(int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y];
                    GeneratePathTo((int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y);
                    Debug.Log("existo2");
                    return true;
                }
                else
                {
                    Node nodeToCheck = map.graph[(int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y];
                    GeneratePathTo((int)map.selectedUnit.transform.position.x + c - 1, (int)unitsToAttack[0].transform.position.y);
                    Debug.Log("existo2");
                    return true;
                }
            }

            //mover izquierda derecha abajo
            else if (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x <= -1
                && unitsToAttack[0].transform.position.y - map.selectedUnit.transform.position.y <= -1)
            {
                c = d - (int)CostToEnterTile(b, (int)map.selectedUnit.transform.position.y, b + d, (int)map.selectedUnit.transform.position.y + d);
                //Node nodeToCheck = map.graph[(int)map.selectedUnit.transform.position.x - c, (int)unitsToAttack[0].transform.position.y - c];

                if (map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
                    && map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null)
                {
                    GeneratePathTo((int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y + c);
                    Debug.Log("existo2");
                    return true;
                }
            }

            //mover izquierda derecha arriba
            else if (unitsToAttack[0].transform.position.x - map.selectedUnit.transform.position.x <= -1
                && unitsToAttack[0].transform.position.y - map.selectedUnit.transform.position.y >= 1)
            {
                c = d - (int)CostToEnterTile(b, (int)map.selectedUnit.transform.position.y, b + d, (int)map.selectedUnit.transform.position.y + d);
                Node nodeToCheck = map.graph[(int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y + c];

                if (map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y + c].GetComponent<ClickableTile>().unitOnTile == null
                    && map.tilesOnMap[(int)map.selectedUnit.transform.position.x + c, (int)map.selectedUnit.transform.position.y + c].GetComponent<ClickableTile>().structureOnTile == null)
                {
                    GeneratePathTo((int)map.selectedUnit.transform.position.x + c, (int)unitsToAttack[0].transform.position.y + c);
                    Debug.Log("existo2");
                    return true;
                }
            }
        }
        else if (
            //Derecha
            (((int)unitToAttack.transform.position.x - (int)map.selectedUnit.transform.position.x == -1
            && (int)unitToAttack.transform.position.y == (int)map.selectedUnit.transform.position.y))
              //Izquierda
              || (((int)unitToAttack.transform.position.x - (int)map.selectedUnit.transform.position.x == -1
            && (int)unitToAttack.transform.position.y == (int)map.selectedUnit.transform.position.y))
              //Arriba
              || (((int)unitToAttack.transform.position.x == (int)map.selectedUnit.transform.position.x
            && (int)unitToAttack.transform.position.y - (int)map.selectedUnit.transform.position.y == 1))
              //Abajo
              || (((int)unitToAttack.transform.position.x == (int)map.selectedUnit.transform.position.x
            && (int)unitToAttack.transform.position.y - (int)map.selectedUnit.transform.position.y == -1))
            && map.selectedUnit.GetComponent<Unit>().unitToBattle != false)
        {
            //StartCoroutine(moveUnitAndFinalize());
            return true;
        }
        else if ((int)unitToAttack.transform.position.x - (int)map.selectedUnit.transform.position.x == (int)map.selectedUnit.GetComponent<Unit>().atkRange && unitToAttack != null)
        {
            //StartCoroutine(moveUnitAndFinalize());
            return true;
        }
        else if (unitToAttack != null)
        {
            GeneratePathTo((int)unitToAttack.transform.position.x, (int)unitsToAttack[0].transform.position.y - 1);
            Debug.Log("existo2");
            return true;
        }
        return false;
    }

    void GeneratePathTo(int x, int y)
    {
        // Clear out our unit's old path.
        map.selectedUnit.GetComponent<Unit>().currentPath = null;
        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = map.graph[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY];

        Node target = map.graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        // Initialize everything to have INFINITY distance, since
        // we don't know any better right now. Also, it's possible
        // that some nodes CAN'T be reached from the source,
        // which would make INFINITY a reasonable value
        foreach (Node v in map.graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + map.CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null)
        {
            // No route between our target and the source
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        currentPath.Reverse();

        map.selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    IEnumerator moveUnitAndFinalize()
    {
        map.disableHighlightUnitRange();
        while (map.selectedUnit.GetComponent<Unit>().movementQueue.Count != 0)
        {
            yield return new WaitForEndOfFrame();
        }


        map.selectedUnit.GetComponent<Unit>().tileX = (int)map.selectedUnit.transform.position.x;
        map.selectedUnit.GetComponent<Unit>().tileY = (int)map.selectedUnit.transform.position.y;

        if (map.selectedUnit.GetComponent<Unit>().unitToBattle == true)
            Battle();
        attack = false;
        //Capture structure
        if ((map.selectedUnit.transform.position.x - structure.transform.position.x == -1 && map.selectedUnit.transform.position.y == structure.transform.position.y)
               || (map.selectedUnit.transform.position.x - structure.transform.position.x == 1 && map.selectedUnit.transform.position.y == structure.transform.position.y)
               || (map.selectedUnit.transform.position.y - structure.transform.position.y == -1 && map.selectedUnit.transform.position.x == structure.transform.position.x)
               || (map.selectedUnit.transform.position.y - structure.transform.position.y == 1 && map.selectedUnit.transform.position.x == structure.transform.position.x))
        {
            structure.GetComponent<Structure>().playerNum = gm.currPlayer;
            structure.GetComponent<Structure>().faction = map.selectedUnit.GetComponent<Unit>().faction;
            gm.GetComponent<GameManager>().UpdateColors();
            map.selectedUnit.GetComponent<Unit>().wait();
            structure = null;            
        }

        map.selectedUnit.GetComponent<Unit>().tileBeingOccupied = map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY];
        map.selectedUnit.GetComponent<Unit>().wait();
        map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = map.selectedUnit;
        if (map.selectedUnit.GetComponent<Unit>().waiting == true)
        StartTurn();
    }
    
    void MoveNextTile()
    {
        int remMove = map.selectedUnit.GetComponent<Unit>().moveSpeed;

        while (remMove > 0)
        {
            if (map.selectedUnit.GetComponent<Unit>().currentPath == null)
                return;

            remMove = remMove - (int)map.CostToEnterTile((int)map.selectedUnit.transform.position.x, (int)map.selectedUnit.transform.position.y, map.selectedUnit.GetComponent<Unit>().currentPath[0].x, map.selectedUnit.GetComponent<Unit>().currentPath[0].y);

            if (map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().structureOnTile == false
                && map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().unitOnTile == false)
            {
                Debug.Log("Pene1");
                map.selectedUnit.transform.position = map.TileCoordToWorldCoord(map.selectedUnit.GetComponent<Unit>().currentPath[0].x, map.selectedUnit.GetComponent<Unit>().currentPath[0].y);
            }
            else if (map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 1, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().structureOnTile == false
                && map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 1, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().unitOnTile == false)
            {
                Debug.Log("pene2");
                map.selectedUnit.transform.position = map.TileCoordToWorldCoord(map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 1, map.selectedUnit.GetComponent<Unit>().currentPath[0].y);
            }
            else if (map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 2, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().structureOnTile == false
                && map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 2, map.selectedUnit.GetComponent<Unit>().currentPath[0].y].GetComponent<ClickableTile>().unitOnTile == false)
            {
                Debug.Log("pene3");
                map.selectedUnit.transform.position = map.TileCoordToWorldCoord(map.selectedUnit.GetComponent<Unit>().currentPath[0].x - 2, map.selectedUnit.GetComponent<Unit>().currentPath[0].y);
            }
            else
            {
                map.selectedUnit.transform.position = map.TileCoordToWorldCoord(map.selectedUnit.GetComponent<Unit>().currentPath[0].x + 1, map.selectedUnit.GetComponent<Unit>().currentPath[0].y);
            }

            map.selectedUnit.GetComponent<Unit>().currentPath.RemoveAt(0);

            if (map.selectedUnit.GetComponent<Unit>().currentPath.Count <= 0)
            {
                map.selectedUnit.GetComponent<Unit>().currentPath = null;
            }
        }
    }

    public IEnumerator moveOverSeconds(GameObject objectToMove, Node endNode)
    {
        int remMove = map.selectedUnit.GetComponent<Unit>().moveSpeed;

        map.selectedUnit.GetComponent<Unit>().movementQueue.Enqueue(1);

        //remove first thing on path because, its the tile we are standing on
        map.selectedUnit.GetComponent<Unit>().currentPath.RemoveAt(0);

        MoveNextTile();

        map.selectedUnit.GetComponent<Unit>().visualMovementSpeed = .3f;

        map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().unitPreviousX, map.selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
        map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = map.selectedUnit;

        map.selectedUnit.GetComponent<Unit>().tileX = endNode.x;
        map.selectedUnit.GetComponent<Unit>().tileY = endNode.y;
        map.selectedUnit.GetComponent<Unit>().tileBeingOccupied.GetComponent<ClickableTile>().unitOnTile = null;
        map.selectedUnit.GetComponent<Unit>().tileBeingOccupied = map.tilesOnMap[map.selectedUnit.GetComponent<Unit>().tileX, map.selectedUnit.GetComponent<Unit>().tileY];
        map.selectedUnit.GetComponent<Unit>().movementQueue.Dequeue();
        yield return new WaitForEndOfFrame();
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        if (targetX < 0 || targetY < 0)
        {
            targetX = 0;
            targetY = 0;
        }
        TileType tt = map.tileTypes[map.tiles[targetX, targetY]];
        float cost = tt.movementCost;
                
        //if (map.tilesOnMap[targetX, targetY].GetComponent<ClickableTile>().structureOnTile == true)
          //  return cost = 666;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.
        Debug.Log(x);
        Debug.Log(y);
        if (map.tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile != null)
        {
            if (map.tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().playerNum != map.selectedUnit.GetComponent<Unit>().playerNum)
                return false;
        }
        else if (map.tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile != null)
        {
            if (map.tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile.GetComponent<Structure>().playerNum != map.selectedUnit.GetComponent<Unit>().playerNum)
                return false;
        }
        return map.tileTypes[map.tiles[x, y]].isWalkable;
    }

    void SelectectUnit(GameObject ss)
    {
        switch (ss.GetComponent<Structure>().faction)
        {
            case 0: //Military
                ss.GetComponent<Structure>().unitToInvoke = ss.GetComponent<Structure>().mSoldier;
                break;
            case 1: //Povery 
                ss.GetComponent<Structure>().unitToInvoke = ss.GetComponent<Structure>().pSoldier;
                break;
            case 2: //Religion

                break;
        }
    }

    void InvokeUnit(GameObject ss)
    {
        Debug.Log("LOL");
        //Where put the soldier
        if (unitsToAttack[0].transform.position.x - ss.transform.position.x < -1 && map.tilesOnMap[(int)ss.transform.position.x - 1, (int)ss.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null)
        {
            boardX = (int)ss.transform.position.x - 1;
            boardY = (int)ss.transform.position.y;
        }
        else if (unitsToAttack[0].transform.position.y - ss.transform.position.y < -1 && map.tilesOnMap[(int)ss.transform.position.x, (int)ss.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile == null)
        {
            boardX = (int)ss.transform.position.x;
            boardY = (int)ss.transform.position.y - 1;
        }
        else if (unitsToAttack[0].transform.position.x - ss.transform.position.x < 1 && map.tilesOnMap[(int)ss.transform.position.x + 1, (int)ss.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null)
        {
            boardX = (int)ss.transform.position.x + 1;
            boardY = (int)ss.transform.position.y;
        }
        else if (unitsToAttack[0].transform.position.y - ss.transform.position.y < 1 && map.tilesOnMap[(int)ss.transform.position.x, (int)map.selectedUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile == null)
        {
            boardX = (int)ss.transform.position.x;
            boardY = (int)ss.transform.position.y + 1;
        }
        else 
            return;
        //Pick a soldier
        SelectectUnit(ss);
        //Create a soldier
        GameObject newUnit = Instantiate(ss.GetComponent<Structure>().unitToInvoke, new Vector3(boardX, boardY, 0), Quaternion.identity);
        newUnit.GetComponent<Unit>().tileX = boardX;
        newUnit.GetComponent<Unit>().tileY = boardY;
        newUnit.GetComponent<Unit>().tileBeingOccupied = map.tilesOnMap[boardX, boardY];
        newUnit.GetComponent<Unit>().ia = true;
        map.tilesOnMap[boardX, boardY].GetComponent<ClickableTile>().unitOnTile = newUnit;
        newUnit.GetComponent<Unit>().playerNum = gm.GetComponent<GameManager>().currPlayer;
        gm.GetComponent<GameManager>().UpdateColors();

        newUnit.GetComponent<Unit>().wait();

        if (gm.GetComponent<GameManager>().currPlayer == 1)
        {
            newUnit.transform.SetParent(gm.GetComponent<GameManager>().containerPlayer1.transform);
            gm.GetComponent<GameManager>().moneyPlayer1 = gm.GetComponent<GameManager>().moneyPlayer1 - newUnit.GetComponent<Unit>().cost;
        }
        else
        {
            newUnit.transform.SetParent(gm.GetComponent<GameManager>().containerPlayer2.transform);
            gm.GetComponent<GameManager>().moneyPlayer2 = gm.GetComponent<GameManager>().moneyPlayer2 - newUnit.GetComponent<Unit>().cost;
        }

        gm.GetComponent<GameManager>().MoneyUpdate();

        ss.GetComponent<Structure>().wait();
    }
}
