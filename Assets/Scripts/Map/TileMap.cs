using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public abstract class TileMap : MonoBehaviour
{
	public GameObject selectedUnit;
	public bool unitSelected;
	public GameObject selectedStructure;
	public GameObject selectedTile;

	public TileType[] tileTypes;

	public GameManager gm;
	public BattleManager bm;

	public Tilemap ground;

	public int[,] tiles;
	public Node[,] graph;

	public int mapSizeX = 20;
	public int mapSizeY = 10;

	[Header("Units on the board")]
	public GameObject unitsOnBoard;
	public GameObject[] unitsInGame;

	//This 2d array is the list of tile gameObjects on the board
	public GameObject[,] tilesOnMap;

	//This 2d array is the list of quadUI gameObjects on the board
	public GameObject[,] quadOnMap;
	public GameObject[,] quadOnMapAtk;
	public GameObject[,] quadOnMapSelected;

	public HashSet<Node> selectedUnitTotalRange;
	public HashSet<Node> selectedUnitMoveRange;

	public HashSet<Node> selectedUnitTotalRangeAtk;
	public HashSet<Node> selectedUnitMoveRangeAtk;

	public GameObject tileContainer;
	public GameObject UIQuadPotentialMovesContainer;
	public GameObject gridUIAtkContainer;
	public GameObject gridUISelectedContainer;

	//public is only to set them in the inspector, if you change these to private then you will
	//need to re-enable them in the inspector
	//Game object that is used to overlay onto the tiles to show possible movement
	public GameObject moveGrid;
	public GameObject atkGrid;
	public GameObject selectedGrid;

	public GameObject buttonPower;
	public GameObject buttonCancel;

	public GameObject positionCommander1;
	public GameObject positionCommander2;
	public GameObject[] positionSoldiers;

	public Camera normalCamera;
	public Camera minimap;

	//All units
	GameObject[] units;

	//Fire
	public GameObject fire;

	//Start
    void Start()
    {
        Application.targetFrameRate = 60;
        normalCamera.transform.position = new Vector3(mapSizeX / 2, mapSizeY / 2f, -10);
        minimap.transform.position = new Vector3(mapSizeX / 2, mapSizeY / 2, -10);
        GeneratePathfindingGraph();
        SetIfTileIsOccupied();
        positionSoldiers = GameObject.FindGameObjectsWithTag("Position");
    }

	//manejo de mouse
    private void Update()
    {
        //If input is left mouse down then select the unit
        if (Input.GetMouseButtonDown(0) && gm.um.canvasPlayer1 == null && gm.um.canvasPlayer2 == null)
        {
            if (selectedUnit == null && selectedStructure == null)
            {
                ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();

                mouseClickToSelectUnitV2(new Vector2Int(ct.tileX, ct.tileY));
            }
            //Invoke
            else if (selectedStructure != null)
            {
                ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
                if (ct.moveGrid.GetComponent<SpriteRenderer>().enabled == true)
                {
                    int boardX = (int)gm.tileBeingDisplayed.transform.position.x;
                    int boardY = (int)gm.tileBeingDisplayed.transform.position.y;
                    GameObject newUnit;
                    newUnit = Instantiate(selectedStructure.GetComponent<Structure>().unitToInvoke, new Vector3(boardX, boardY, 0), Quaternion.identity);
                    newUnit.GetComponent<Unit>().tileX = boardX;
                    newUnit.GetComponent<Unit>().tileY = boardY;
                    newUnit.GetComponent<Unit>().tileBeingOccupied = tilesOnMap[boardX, boardY];
                    ct.unitOnTile = newUnit;
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
                    disableHighlightUnitRange();

                    switch (selectedStructure.GetComponent<Structure>().faction)
                    {
                        case 1:
                            selectedStructure.GetComponent<Structure>().usage = selectedStructure.GetComponent<Structure>().usage - 1;
                            if (selectedStructure.GetComponent<Structure>().usage == 0)
                                selectedStructure.GetComponent<Structure>().wait();
                            break;
                        default:
                            selectedStructure.GetComponent<Structure>().wait();
                            break;
                    }
                    selectedStructure = null;
                }
            }
            //Move		
            else if (selectedUnit.GetComponent<Unit>().unitMoveState == selectedUnit.GetComponent<Unit>().getMovementStateEnum(1)
                    && selectedUnit.GetComponent<Unit>().movementQueue.Count == 0 && selectedUnit.GetComponent<Unit>().wagon == false)
            {
                ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
                if (selectTileToMoveTo())
                {
                    if (ct.unitOnTile == null && ct.structureOnTile == null)
                    {
                        selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
                        selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;

                        moveUnit();
                        if (selectedUnit.GetComponent<Unit>().tileX - ct.tileX <= 0)
                        {
                            selectedUnit.GetComponent<SpriteRenderer>().flipX = false;
                            selectedUnit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().flipX = false;
                        }
                        else
                        {
                            selectedUnit.GetComponent<SpriteRenderer>().flipX = true;
                            selectedUnit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().flipX = true;
                        }
                        StartCoroutine(moveUnitAndFinalize());
                    }
                }
                if (ct.unitOnTile == selectedUnit)
                {
                    StartCoroutine(moveUnitAndFinalize());
                }
            }
            //Move wagon
            else if (selectedUnit.GetComponent<Unit>().unitMoveState == selectedUnit.GetComponent<Unit>().getMovementStateEnum(1)
                    && selectedUnit.GetComponent<Unit>().movementQueue.Count == 0 && selectedUnit.GetComponent<Unit>().wagon == true)
            {
                ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
                if (selectTileToMoveTo())
                {
                    if (ct.unitOnTile == null && ct.structureOnTile == null)
                    {
                        selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
                        selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;

                        moveUnit();
                        if (selectedUnit.GetComponent<Unit>().tileX - ct.tileX <= 0)
                        {
                            selectedUnit.GetComponent<SpriteRenderer>().flipX = false;
                            selectedUnit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().flipX = false;
                        }
                        else
                        {
                            selectedUnit.GetComponent<SpriteRenderer>().flipX = true;
                            selectedUnit.GetComponent<Unit>().theColor.GetComponent<SpriteRenderer>().flipX = true;
                        }

                        if (selectedUnit.GetComponent<Unit>().unitIn != null)
                        {
                            StartCoroutine(moveUnitAndFinalizeStructure());
                            StartCoroutine(moveUnitAndFinalize());
                        }
                        else
                            StartCoroutine(moveUnitAndFinalize());
                    }
                }
                if (ct.unitOnTile == selectedUnit)
                {
                    if (selectedUnit.GetComponent<Unit>().unitIn != null)
                        StartCoroutine(moveUnitAndFinalizeStructure());
                    else
                        StartCoroutine(moveUnitAndFinalize());
                }

            }
            //Final
            else if (selectedUnit.GetComponent<Unit>().unitMoveState == selectedUnit.GetComponent<Unit>().getMovementStateEnum(2))
            {
                finalizeOption();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            disableHighlightUnitRange();
            if (selectedUnit != null)
            {
                if (selectedUnit.GetComponent<Unit>().movementQueue.Count == 0 && selectedUnit.GetComponent<Unit>().combatQueue.Count == 0)
                {
                    if (selectedUnit.GetComponent<Unit>().unitMoveState != selectedUnit.GetComponent<Unit>().getMovementStateEnum(3))
                    {
                        deselectUnit();
                    }
                }
                else if (selectedUnit.GetComponent<Unit>().movementQueue.Count == 1)
                {
                    selectedUnit.GetComponent<Unit>().visualMovementSpeed = 0.5f;
                }
            }

            if (selectedStructure != null)
            {
                selectedStructure = null;
                disableHighlightUnitRange();
            }
        }
    }  

	public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) 
	{
		TileType tt = tileTypes[tiles[targetX, targetY]];
		float cost = tt.movementCost;
		if (UnitCanEnterTile(targetX, targetY) == false)
			return Mathf.Infinity;

		if (sourceX != targetX && sourceY != targetY) {
			// We are moving diagonally!  Fudge the cost for tie-breaking
			// Purely a cosmetic thing!
			cost += 0.001f;
		}

		return cost;
	}

	public float CostToAtkTile(int sourceX, int sourceY, int targetX, int targetY)
	{		
		float cost = 1;

		if (sourceX != targetX && sourceY != targetY)
		{
			// We are moving diagonally!  Fudge the cost for tie-breaking
			// Purely a cosmetic thing!
			cost += 0.001f;
		}

		return cost;
	}

	void GeneratePathfindingGraph() {
		// Initialize the array
		graph = new Node[mapSizeX, mapSizeY];

		// Initialize a Node for each spot in the array
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				graph[x, y] = new Node();
				graph[x, y].x = x;
				graph[x, y].y = y;
			}
		}

		// Now that all the nodes exist, calculate their neighbours
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {

				// This is the 4-way connection version:
				if (x > 0)
					graph[x, y].neighbours.Add(graph[x - 1, y]);
				if (x < mapSizeX - 1)
					graph[x, y].neighbours.Add(graph[x + 1, y]);
				if (y > 0)
					graph[x, y].neighbours.Add(graph[x, y - 1]);
				if (y < mapSizeY - 1)
					graph[x, y].neighbours.Add(graph[x, y + 1]);
			}
		}
	}

	public Vector3 TileCoordToWorldCoord(int x, int y) {
		return new Vector3(x, y, 0);
	}

	public bool UnitCanEnterTile(int x, int y) {

		// We could test the unit's walk/hover/fly type against various
		// terrain flags here to see if they are allowed to enter the tile.
		ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
		if (tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile != null && ct.unitOnTile != null)
		{
			if (tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().playerNum != ct.unitOnTile.GetComponent<Unit>().playerNum)
				return false;
		}
		else if (tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile != null && ct.unitOnTile != null)
        {
			if (tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile.GetComponent<Structure>().playerNum != ct.unitOnTile.GetComponent<Unit>().playerNum)
				return false;
		}

		else if (tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile != null && selectedUnit != null)
		{
			if (tilesOnMap[x, y].GetComponent<ClickableTile>().unitOnTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum)
				return false;
		}
		else if (tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile != null && selectedUnit != null)
		{
			if (tilesOnMap[x, y].GetComponent<ClickableTile>().structureOnTile.GetComponent<Structure>().playerNum != selectedUnit.GetComponent<Unit>().playerNum)
				return false;
		}

		return tileTypes[tiles[x, y]].isWalkable;
	}

	public void GeneratePathTo(int x, int y) {
		// Clear out our unit's old path.
		selectedUnit.GetComponent<Unit>().currentPath = null;

		if (UnitCanEnterTile(x, y) == false) {
			// We probably clicked on a mountain or something, so just quit out.
			return;
		}
		
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		// Setup the "Q" -- the list of nodes we haven't checked yet.
		List<Node> unvisited = new List<Node>();

		Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];

		Node target = graph[x, y];

		dist[source] = 0;
		prev[source] = null;

		// Initialize everything to have INFINITY distance, since
		// we don't know any better right now. Also, it's possible
		// that some nodes CAN'T be reached from the source,
		// which would make INFINITY a reasonable value
		foreach (Node v in graph) {
			if (v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while (unvisited.Count > 0) {
			// "u" is going to be the unvisited node with the smallest distance.
			Node u = null;

			foreach (Node possibleU in unvisited) {
				if (u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if (u == target) {
				break;  // Exit the while loop!
			}

			unvisited.Remove(u);

			foreach (Node v in u.neighbours) {
				//float alt = dist[u] + u.DistanceTo(v);
				float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
				if (alt < dist[v]) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}

		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.

		if (prev[target] == null) {
			// No route between our target and the source
			return;
		}

		List<Node> currentPath = new List<Node>();

		Node curr = target;

		// Step through the "prev" chain and add it to our path
		while (curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!

		currentPath.Reverse();

		selectedUnit.GetComponent<Unit>().currentPath = currentPath;
	}

	public void highlightUnitRange()
	{
		HashSet<Node> finalMovementHighlight = new HashSet<Node>();
		HashSet<Node> totalAttackableTiles = new HashSet<Node>();
		HashSet<Node> finalEnemyUnitsInMovementRange = new HashSet<Node>();

		int attRange = selectedUnit.GetComponent<Unit>().minAtkRange;
		int moveSpeed = selectedUnit.GetComponent<Unit>().moveSpeed;


		Node unitInitialNode = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];
		finalMovementHighlight = getUnitMovementOptions();
		totalAttackableTiles = getUnitTotalAttackableTiles(finalMovementHighlight, attRange, unitInitialNode);

		//Configurar
		foreach (Node n in totalAttackableTiles)
		{	
			if (tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile != null)
			{
				GameObject unitOnCurrentlySelectedTile = tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile;
				if (unitOnCurrentlySelectedTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum)
				{
					finalEnemyUnitsInMovementRange.Add(n);
				}
			}
		}

		highlightEnemiesInRange(totalAttackableTiles);

		highlightMovementRange(finalMovementHighlight);

		selectedUnitMoveRange = finalMovementHighlight;

		selectedUnitTotalRange = getUnitTotalRange(finalMovementHighlight, totalAttackableTiles);
	}

	public void disableHighlightUnitRange()
	{
		foreach (GameObject quad in quadOnMap)
		{
			if (quad.GetComponent<SpriteRenderer>().enabled == true)
			{
				quad.GetComponent<SpriteRenderer>().enabled = false;
			}
		}

		foreach (GameObject quad in quadOnMapAtk)
        {
			if (quad.GetComponent<SpriteRenderer>().enabled == true)
			{
				quad.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}

	//Activar el grid de movimiento
	public void highlightMovementRange(HashSet<Node> movementToHighlight)
	{
		foreach (Node n in movementToHighlight)
		{
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5058824f);
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	public void highlightEnemiesInRange(HashSet<Node> enemiesToHighlight)
	{
		foreach (Node n in enemiesToHighlight)
		{
			quadOnMapAtk[n.x, n.y].GetComponent<SpriteRenderer>().color = new Color(.4352942f, .227451f, .227451f, .5058824f);
			quadOnMapAtk[n.x, n.y].GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	//activar el grid de movimiento de la unidad cliqueada
	public void highlightMovementRangeUnitOnTile(HashSet<Node> movementToHighlight)
	{
		foreach (Node n in movementToHighlight)
		{
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().color = new Color(.1415094f, .1415094f, .1415094f, .5058824f);
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	public void highlightEnemiesInRangeUnitOnTile(HashSet<Node> enemiesToHighlight)
	{
		foreach (Node n in enemiesToHighlight)
		{
			quadOnMapAtk[n.x, n.y].GetComponent<SpriteRenderer>().color = new Color(.3113208f, .1541919f, .1541919f, .5058824f);
			quadOnMapAtk[n.x, n.y].GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	//poner en ataque
	public HashSet<Node> getUnitMovementOptions()
	{
		float[,] cost = new float[mapSizeX, mapSizeY];
		HashSet<Node> UIHighlight = new HashSet<Node>();
		HashSet<Node> tempUIHighlight = new HashSet<Node>();
		HashSet<Node> finalMovementHighlight = new HashSet<Node>();
		int moveSpeed = selectedUnit.GetComponent<Unit>().moveSpeed;
		Node unitInitialNode = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];

		//Set-up the initial costs for the neighbouring nodes
		finalMovementHighlight.Add(unitInitialNode);
		foreach (Node n in unitInitialNode.neighbours)
		{
			cost[n.x, n.y] = CostToEnterTile(n.x, n.y, n.x, n.y);
			//Debug.Log(cost[n.x, n.y]);
			if (moveSpeed - cost[n.x, n.y] >= 0)
			{
				UIHighlight.Add(n);
			}
		}

		finalMovementHighlight.UnionWith(UIHighlight);

		while (UIHighlight.Count != 0)
		{
			foreach (Node n in UIHighlight)
			{
				foreach (Node neighbour in n.neighbours)
				{
					if (!finalMovementHighlight.Contains(neighbour))
					{
						cost[neighbour.x, neighbour.y] = CostToEnterTile(neighbour.x, neighbour.y, neighbour.x, neighbour.y) + cost[n.x, n.y];
						//Debug.Log(cost[neighbour.x, neighbour.y]);
						if (moveSpeed - cost[neighbour.x, neighbour.y] >= 0)
						{
							//Debug.Log(cost[neighbour.x, neighbour.y]);
							tempUIHighlight.Add(neighbour);
						}
					}
				}

			}

			UIHighlight = tempUIHighlight;
			finalMovementHighlight.UnionWith(UIHighlight);
			tempUIHighlight = new HashSet<Node>();

		}		
		return finalMovementHighlight;
	}

	public HashSet<Node> getUnitTotalAttackableTiles(HashSet<Node> finalMovementHighlight, int attRange, Node unitInitialNode)
	{
		HashSet<Node> tempNeighbourHash = new HashSet<Node>();
		HashSet<Node> neighbourHash = new HashSet<Node>();
		HashSet<Node> seenNodes = new HashSet<Node>();
		HashSet<Node> totalAttackableTiles = new HashSet<Node>();
		foreach (Node n in finalMovementHighlight)
		{
			neighbourHash = new HashSet<Node>();
			neighbourHash.Add(n);
			for (int i = 0; i < attRange; i++)
			{
				foreach (Node t in neighbourHash)
				{
					foreach (Node tn in t.neighbours)
					{
						tempNeighbourHash.Add(tn);
					}
				}

				neighbourHash = tempNeighbourHash;
				tempNeighbourHash = new HashSet<Node>();
				if (i < attRange - 1)
				{
					seenNodes.UnionWith(neighbourHash);
				}

			}
			neighbourHash.ExceptWith(seenNodes);
			seenNodes = new HashSet<Node>();
			totalAttackableTiles.UnionWith(neighbourHash);
		}
		totalAttackableTiles.Remove(unitInitialNode);

		//Debug.Log("The unit node has this many attack options" + totalAttackableTiles.Count);
		return (totalAttackableTiles);
	}

	public HashSet<Node> getUnitTotalRange(HashSet<Node> finalMovementHighlight, HashSet<Node> totalAttackableTiles)
	{
		HashSet<Node> unionTiles = new HashSet<Node>();
		unionTiles.UnionWith(finalMovementHighlight);
		unionTiles.UnionWith(totalAttackableTiles);
		return unionTiles;
	}

	public void SetIfTileIsOccupied()
	{
		foreach (Transform team in unitsOnBoard.transform)
		{			
			foreach (Transform unitOnTeam in team)
			{
				int unitX = unitOnTeam.GetComponent<Unit>().tileX;
				int unitY = unitOnTeam.GetComponent<Unit>().tileY;
				unitOnTeam.GetComponent<Unit>().tileBeingOccupied = tilesOnMap[unitX, unitY];
				tilesOnMap[unitX, unitY].GetComponent<ClickableTile>().unitOnTile = unitOnTeam.gameObject;
			}
		}
	}

	public void moveUnit()
	{
		if (selectedUnit != null)
		{
			selectedUnit.GetComponent<Unit>().MoveNextTile();
		}
	}

	public IEnumerator moveUnitAndFinalize()
	{
		disableHighlightUnitRange();
		while (selectedUnit.GetComponent<Unit>().movementQueue.Count != 0)
		{
			yield return new WaitForEndOfFrame();
		}
		finalizeMovementPosition();
	}

	public IEnumerator moveUnitAndFinalizeStructure()
	{
		disableHighlightUnitRange();
		selectedUnit.GetComponent<Unit>().setMovementState(2);
		while (selectedUnit.GetComponent<Unit>().movementQueue.Count != 0)
		{
			yield return new WaitForEndOfFrame();
		}
		finalizeMovementPositionStructure();
	}

	public void finalizeMovementPosition()
	{
		tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;
		//After a unit has been moved we will set the unitMoveState to (2) the 'Moved' state


		selectedUnit.GetComponent<Unit>().setMovementState(2);

		highlightUnitAttackOptionsFromPosition();
		highlightTileUnitIsOccupying();
	}

	public void highlightUnitAttackOptionsFromPosition()
	{
		if (selectedUnit != null)
		{
			//highlightEnemiesInRange(getUnitAttackOptionsFromPosition());
			highlightEnemiesInRange(getUnitAtkOptions());			
		}		
	}

	public void highlightTileUnitIsOccupying()
	{
		if (selectedUnit != null)
		{
			highlightMovementRange(getTileUnitIsOccupying());
		}
	}

	public HashSet<Node> getTileUnitIsOccupying()
	{

		int x = selectedUnit.GetComponent<Unit>().tileX;
		int y = selectedUnit.GetComponent<Unit>().tileY;
		HashSet<Node> singleTile = new HashSet<Node>();
		singleTile.Add(graph[x, y]);
		return singleTile;
	}

	public void deselectUnit()
	{
		if (selectedUnit != null)
		{
			if (selectedUnit.GetComponent<Unit>().unitMoveState == selectedUnit.GetComponent<Unit>().getMovementStateEnum(1))
			{
				disableHighlightUnitRange();
				selectedUnit.GetComponent<Unit>().setMovementState(0);

				selectedUnit = null;				
			}
			else if (selectedUnit.GetComponent<Unit>().unitMoveState == selectedUnit.GetComponent<Unit>().getMovementStateEnum(3))
			{
				disableHighlightUnitRange();
				selectedUnit = null;			
			}
			else
			{
				disableHighlightUnitRange();
				tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = null;
				tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;

				selectedUnit.GetComponent<Unit>().tileX = selectedUnit.GetComponent<Unit>().unitPreviousX;
				selectedUnit.GetComponent<Unit>().tileY = selectedUnit.GetComponent<Unit>().unitPreviousY;
				selectedUnit.transform.position = TileCoordToWorldCoord(selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY);
				selectedUnit.GetComponent<Unit>().setMovementState(0);
				selectedUnit = null;				
			}
		}
	}

	public IEnumerator deselectAfterMovements(GameObject unit, GameObject enemy)
	{
		//selectedSound.Play();
		selectedUnit.GetComponent<Unit>().setMovementState(3);
		disableHighlightUnitRange();
		//If i dont have this wait for seconds the while loops get passed as the coroutine has not started from the other script
		//Adding a delay here to ensure that it all works smoothly. (probably not the best idea)
		yield return new WaitForSeconds(.25f);
		if (unit != null)
			yield return new WaitForEndOfFrame();
		if(unit != null)
		{
            while (unit.GetComponent<Unit>().combatQueue.Count > 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }
		
		Debug.Log("All animations done playing");

		deselectUnit();
		units = GameObject.FindGameObjectsWithTag("Unit");
	}

	public HashSet<Node> getUnitAtkOptions()
	{
		float[,] cost = new float[mapSizeX, mapSizeY];
		HashSet<Node> UIHighlight = new HashSet<Node>();
		HashSet<Node> tempUIHighlight = new HashSet<Node>();
		HashSet<Node> finalMovementHighlight = new HashSet<Node>();
		int minAtk = selectedUnit.GetComponent<Unit>().minAtkRange;
		Node unitInitialNode = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];

		//Set-up the initial costs for the neighbouring nodes
		finalMovementHighlight.Add(unitInitialNode);
		foreach (Node n in unitInitialNode.neighbours)
		{
			cost[n.x, n.y] = CostToAtkTile(n.x, n.y, n.x, n.y);
			//Debug.Log(cost[n.x, n.y]);
			if (minAtk - cost[n.x, n.y] >= 0)
			{
				UIHighlight.Add(n);
			}
		}

		finalMovementHighlight.UnionWith(UIHighlight);

		while (UIHighlight.Count != 0)
		{
			foreach (Node n in UIHighlight)
			{
				foreach (Node neighbour in n.neighbours)
				{
					if (!finalMovementHighlight.Contains(neighbour))
					{
						cost[neighbour.x, neighbour.y] = CostToAtkTile(neighbour.x, neighbour.y, neighbour.x, neighbour.y) + cost[n.x, n.y];
						//Debug.Log(cost[neighbour.x, neighbour.y]);
						if (minAtk - cost[neighbour.x, neighbour.y] >= 0)
						{
							//Debug.Log(cost[neighbour.x, neighbour.y]);
							tempUIHighlight.Add(neighbour);
						}
					}
				}

			}

			UIHighlight = tempUIHighlight;
			finalMovementHighlight.UnionWith(UIHighlight);
			tempUIHighlight = new HashSet<Node>();

		}
		return finalMovementHighlight;
	}

	//UnitInvoke
	HashSet<Node> getUnitAttackOptionsFromPositionStructure()
	{
		HashSet<Node> tempNeighbourHash = new HashSet<Node>();
		HashSet<Node> neighbourHash = new HashSet<Node>();
		HashSet<Node> seenNodes = new HashSet<Node>();
		int boardX;
		int boardY;
		if (selectedStructure != null)
		{
			boardX = (int)selectedStructure.transform.position.x;
			boardY = (int)selectedStructure.transform.position.y;
		}
		else
        {
			boardX = (int)selectedUnit.transform.position.x;
			boardY = (int)selectedUnit.transform.position.y;
		}

		Node initialNode = graph[boardX, boardY];
		//int attRange = selectedStructure.GetComponent<Structure>().atkRange;


		neighbourHash = new HashSet<Node>();
		neighbourHash.Add(initialNode);
		for (int i = 0; i < 1; i++)
		{
			foreach (Node t in neighbourHash)
			{
				foreach (Node tn in t.neighbours)
				{
					tempNeighbourHash.Add(tn);
				}
			}
			neighbourHash = tempNeighbourHash;
			tempNeighbourHash = new HashSet<Node>();
			if (i < 1 - 1)
			{
				seenNodes.UnionWith(neighbourHash);
			}
		}
		neighbourHash.ExceptWith(seenNodes);
		neighbourHash.Remove(initialNode);
		return neighbourHash;
	}

	public void highlightEnemiesInRangeStructure(HashSet<Node> enemiesToHighlight)
	{
		foreach (Node n in enemiesToHighlight)
		{
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().color = new Color(.0518868f, 1, .4274139f, .5058824f);
			quadOnMap[n.x, n.y].GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	public void highlightUnitAttackOptionsFromPositionStructure()
	{
		if (selectedStructure != null)
		{
			highlightEnemiesInRangeStructure(getUnitAttackOptionsFromPositionStructure());
		}
		else if (selectedUnit != null)
			highlightEnemiesInRangeStructure(getUnitAttackOptionsFromPositionStructure());
	}


	public void finalizeMovementPositionStructure()
	{		
		highlightUnitAttackOptionsFromPositionStructure();
	}

	//Mouse
	public void mouseClickToSelectUnitV2(Vector2Int coords)
	{
		if(gm.tileBeingDisplayed == false)
        {
			return;
        }
		ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();
		//Nada
		if (ct.unitOnTile == null && ct.structureOnTile == null)
			Debug.Log("Nada");

		//Select Unit
		else if (ct.unitOnTile != null && ct.unitOnTile.GetComponent<Unit>().waiting == false
				&& ct.unitOnTile.GetComponent<Unit>().playerNum == gm.GetComponent<GameManager>().currPlayer)
		{
			GameObject unitOnSelect = GetUnitOnSquare(coords);
			SetSelectedUnit(coords);
			selectedUnit.GetComponent<Unit>().setMovementState(1);
			Debug.Log("Unidad seleccionada");
			highlightUnitRange();                                                                   

			switch (selectedUnit.name)
			{
				case "mSniperCommander":
				case "mSniperCommander(Clone)":
					buttonPower.transform.GetChild(0).GetComponent<Text>().text = "Concentration";
					buttonPower.GetComponent<Button>().onClick.AddListener(() => selectedUnit.GetComponent<Powers>().Concentration());
					break;
				case "pMechanicCommander":
				case "pMechanicCommander(Clone)":
					buttonPower.transform.GetChild(0).GetComponent<Text>().text = "Scream";
					buttonPower.GetComponent<Button>().onClick.AddListener(() => selectedUnit.GetComponent<Powers>().Concentration());
					break;
				case "mInvokeMachine(Clone)":
					buttonPower.transform.GetChild(0).GetComponent<Text>().text = "Invoke";
					buttonPower.GetComponent<Button>().onClick.AddListener(() => selectedUnit.GetComponent<Powers>().InvokeMachineButton());
					break;
				default:
					buttonPower.transform.GetChild(0).GetComponent<Text>().text = "Power";
					break;
			}

		}

		//Select Structure
		else if (ct.structureOnTile != null && ct.structureOnTile.GetComponent<Structure>().playerNum == gm.GetComponent<GameManager>().currPlayer
				&& ct.structureOnTile.GetComponent<Structure>().waiting == false)
		{
			switch (ct.structureOnTile.name)
			{
				case "Mine":
					Debug.Log("Mine");
					break;
				case "Cuartel":
					ct.structureOnTile.GetComponent<Structure>().UpdateMoney();
					if (ct.structureOnTile.GetComponent<Structure>().faction == 0)
						ct.structureOnTile.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
					else if (ct.structureOnTile.GetComponent<Structure>().faction == 1)
						ct.structureOnTile.transform.GetChild(1).GetComponent<Canvas>().enabled = true;
					else if (ct.structureOnTile.GetComponent<Structure>().faction == 2)
						ct.structureOnTile.transform.GetChild(2).GetComponent<Canvas>().enabled = true;
					//DisableCollider();
					selectedStructure = ct.structureOnTile;
					break;
				case "PrimaryStructure":
					Debug.Log("Princpal");
					break;
				default:
					Debug.Log("???");
					break;
			}
		}
		//Aliado
		else if (ct.unitOnTile.GetComponent<Unit>().waiting == true && ct.unitOnTile.GetComponent<Unit>().playerNum == gm.GetComponent<GameManager>().currPlayer)
		{
			highlightUnitOnTileRange();
			Debug.Log("Aliado");
		}
		//Enemigo
		else if (ct.unitOnTile.GetComponent<Unit>().playerNum != gm.GetComponent<GameManager>().currPlayer)
		{
			highlightUnitOnTileRange();
			Debug.Log("Enemy");
		}

	}

	public void highlightUnitOnTileRange()
	{
		ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();

		HashSet<Node> finalMovementHighlight = new HashSet<Node>();
		HashSet<Node> totalAttackableTiles = new HashSet<Node>();
		HashSet<Node> finalEnemyUnitsInMovementRange = new HashSet<Node>();

		int attRange = ct.unitOnTile.GetComponent<Unit>().atkRange;
		int moveSpeed = ct.unitOnTile.GetComponent<Unit>().moveSpeed;


		Node unitInitialNode = graph[ct.unitOnTile.GetComponent<Unit>().tileX, ct.unitOnTile.GetComponent<Unit>().tileY];
		finalMovementHighlight = getUnitOnTileMovementOptions();
		totalAttackableTiles = getUnitTotalAttackableTiles(finalMovementHighlight, attRange, unitInitialNode);

		//Configurar
		foreach (Node n in totalAttackableTiles)
		{

			if (tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile != null)
			{
				GameObject unitOnCurrentlySelectedTile = tilesOnMap[n.x, n.y].GetComponent<ClickableTile>().unitOnTile;
				if (unitOnCurrentlySelectedTile.GetComponent<Unit>().playerNum != ct.unitOnTile.GetComponent<Unit>().playerNum)
				{
					finalEnemyUnitsInMovementRange.Add(n);
				}
			}
		}

		highlightEnemiesInRangeUnitOnTile(totalAttackableTiles);

		highlightMovementRangeUnitOnTile(finalMovementHighlight);

		selectedUnitMoveRange = finalMovementHighlight;

		selectedUnitTotalRange = getUnitTotalRange(finalMovementHighlight, totalAttackableTiles);
	}

	public HashSet<Node> getUnitOnTileMovementOptions()
	{
		ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();

		float[,] cost = new float[mapSizeX, mapSizeY];
		HashSet<Node> UIHighlight = new HashSet<Node>();
		HashSet<Node> tempUIHighlight = new HashSet<Node>();
		HashSet<Node> finalMovementHighlight = new HashSet<Node>();
		int moveSpeed = ct.unitOnTile.GetComponent<Unit>().moveSpeed;
		Node unitInitialNode = graph[ct.unitOnTile.GetComponent<Unit>().tileX, ct.unitOnTile.GetComponent<Unit>().tileY];

		///Set-up the initial costs for the neighbouring nodes
		finalMovementHighlight.Add(unitInitialNode);
		foreach (Node n in unitInitialNode.neighbours)
		{
			cost[n.x, n.y] = CostToEnterTile(n.x, n.y, n.x, n.y);
			//Debug.Log(cost[n.x, n.y]);
			if (moveSpeed - cost[n.x, n.y] >= 0)
			{
				UIHighlight.Add(n);
			}
		}

		finalMovementHighlight.UnionWith(UIHighlight);

		while (UIHighlight.Count != 0)
		{
			foreach (Node n in UIHighlight)
			{
				foreach (Node neighbour in n.neighbours)
				{
					if (!finalMovementHighlight.Contains(neighbour))
					{
						cost[neighbour.x, neighbour.y] = CostToEnterTile(neighbour.x, neighbour.y, neighbour.x, neighbour.y) + cost[n.x, n.y];
						//Debug.Log(cost[neighbour.x, neighbour.y]);
						if (moveSpeed - cost[neighbour.x, neighbour.y] >= 0)
						{
							//Debug.Log(cost[neighbour.x, neighbour.y]);
							tempUIHighlight.Add(neighbour);
						}
					}
				}

			}

			UIHighlight = tempUIHighlight;
			finalMovementHighlight.UnionWith(UIHighlight);
			tempUIHighlight = new HashSet<Node>();

		}
		return finalMovementHighlight;
	}
	
	public bool selectTileToMoveTo()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{

			if (hit.transform.gameObject.CompareTag("Tile"))
			{

				int clickedTileX = hit.transform.GetComponent<ClickableTile>().tileX;
				int clickedTileY = hit.transform.GetComponent<ClickableTile>().tileY;
				Node nodeToCheck = graph[clickedTileX, clickedTileY];

				if (selectedUnitMoveRange.Contains(nodeToCheck))
				{
					if ((hit.transform.gameObject.GetComponent<ClickableTile>().unitOnTile == null 
						|| hit.transform.gameObject.GetComponent<ClickableTile>().unitOnTile == selectedUnit) && (selectedUnitMoveRange.Contains(nodeToCheck)))
					{
						Debug.Log("We have finally selected the tile to move to");
						GeneratePathTo(clickedTileX, clickedTileY);
						FixedStayUnit();
						return true;
					}
				}
			}			
		}
		FixedStayUnit();
		return false;
	}

	public void finalizeOption()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		HashSet<Node> attackableTiles = getUnitAtkOptions();
		ClickableTile ct = gm.tileBeingDisplayed.GetComponent<ClickableTile>();		

		if (Physics.Raycast(ray, out hit))
		{

			//This portion is to ensure that the tile has been clicked
			//If the tile has been clicked then we need to check if there is a unit on it
			if (hit.transform.gameObject.CompareTag("Tile"))
			{
				//Molotov attack in a single tile
				if ((selectedUnit.name == "pMolotov" || selectedUnit.name == "pMolotov(Clone)") 
					&& hit.transform.GetComponent<ClickableTile>().unitOnTile == null 
					&& attackableTiles.Contains(graph[(int)hit.transform.position.x, (int)hit.transform.position.y])
					&& hit.transform.GetComponent<ClickableTile>().structureOnTile == null)
                {
					tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;					
					selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
					selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
					if (selectedUnit.name == "pMolotov" || selectedUnit.name == "pMolotov(Clone)")
					{
						
						GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
						foreach (GameObject tile in tiles)
                        {
							if (tile.transform.position.x == hit.transform.position.x && tile.transform.position.y == hit.transform.position.y)
							{
								tile.GetComponent<ClickableTile>().fireOn = true;
								tile.GetComponent<ClickableTile>().FireOnTile();
							}
						}						
					}					
					selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
					selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
					tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;

					selectedUnit.GetComponent<Unit>().wait();
					StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
				}

				if (hit.transform.GetComponent<ClickableTile>().unitOnTile != null)
				{
					GameObject unitOnTile = hit.transform.GetComponent<ClickableTile>().unitOnTile;
					int unitX = unitOnTile.GetComponent<Unit>().tileX;
					int unitY = unitOnTile.GetComponent<Unit>().tileY;
					//El mismo
					if (unitOnTile == selectedUnit)
					{
						tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
						disableHighlightUnitRange();
						Debug.Log("ITS THE SAME UNIT JUST WAIT");
						selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
						selectedUnit.GetComponent<Unit>().wait();
						selectedUnit.GetComponent<Unit>().setMovementState(3);
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
						tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;
						deselectUnit();
					}
					//Enter Wagon
					else if (ct.unitOnTile.GetComponent<Unit>().wagon == true && ct.unitOnTile.GetComponent<Unit>().playerNum == selectedUnit.GetComponent<Unit>().playerNum
							 && ct.unitOnTile.GetComponent<Unit>().unitIn == null)
					{
						ct.unitOnTile.GetComponent<Unit>().unitIn = selectedUnit;
						selectedUnit.GetComponent<Unit>().inWagon = true;
						selectedUnit.transform.position = new Vector3(-100, -100, 0);
						selectedUnit.GetComponent<Unit>().tileBeingOccupied.GetComponent<ClickableTile>().unitOnTile = null;
						selectedUnit.GetComponent<Unit>().tileBeingOccupied = null;
						selectedUnit.GetComponent<Unit>().tileX = ct.unitOnTile.GetComponent<Unit>().tileX;
						selectedUnit.GetComponent<Unit>().tileY = ct.unitOnTile.GetComponent<Unit>().tileY;
						StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
					}
					//Kamikaze
					else if (selectedUnit.name == "pKamikaze(Clone)" && attackableTiles.Contains(graph[unitX, unitY]))
					{
						selectedUnit.GetComponent<Explosion>().KamikazeAtk();
						if (selectedUnit != null)
							Destroy(selectedUnit);
						StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
                    }
					//Minitank
					else if (ct.unitOnTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum
							 && attackableTiles.Contains(graph[unitX, unitY]) && (selectedUnit.name == "mMinitank(Clone)" || selectedUnit.name == "mMinitank"))
					{
						GameObject tempUnit = ct.unitOnTile;
						tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
						selectedUnit.GetComponent<Unit>().maxDmg = selectedUnit.GetComponent<Unit>().weaponDamage[ct.unitOnTile.GetComponent<Unit>().weaponID];
                        ct.unitOnTile.GetComponent<Unit>().maxDmg = ct.unitOnTile.GetComponent<Unit>().weaponDamage[selectedUnit.GetComponent<Unit>().weaponID];
                        //selectedUnit.GetComponent<Weapons>().WeaponDamage(selectedUnit, ct.unitOnTile);
                        bm.Battle(selectedUnit, ct.unitOnTile);
						selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;

						if (selectedUnit.transform.position.x - tempUnit.transform.position.x == -1 && selectedUnit.transform.position.y == tempUnit.transform.position.y)
						{
							if (tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
								&& tilesOnMap[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null
								&& tileTypes[tiles[(int)tempUnit.transform.position.x + 1, (int)tempUnit.transform.position.y]].isWalkable)
								selectedUnit.transform.position = new Vector3(tempUnit.transform.position.x + 1, tempUnit.transform.position.y);
						}
						else if (selectedUnit.transform.position.x - tempUnit.transform.position.x == 1 && selectedUnit.transform.position.y == tempUnit.transform.position.y)
						{
							if (tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile == null
								&& tilesOnMap[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y].GetComponent<ClickableTile>().structureOnTile == null
								&& tileTypes[tiles[(int)tempUnit.transform.position.x - 1, (int)tempUnit.transform.position.y]].isWalkable)
								selectedUnit.transform.position = new Vector3(tempUnit.transform.position.x - 1, tempUnit.transform.position.y);
						}
						else if (selectedUnit.transform.position.y - tempUnit.transform.position.y == -1 && selectedUnit.transform.position.x == tempUnit.transform.position.x)
						{
							if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().unitOnTile == null
								&& tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1].GetComponent<ClickableTile>().structureOnTile == null
								&& tileTypes[tiles[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y + 1]].isWalkable)
								selectedUnit.transform.position = new Vector3(tempUnit.transform.position.x, tempUnit.transform.position.y + 1);
						}
						else if (selectedUnit.transform.position.y - tempUnit.transform.position.y == 1 && selectedUnit.transform.position.x == tempUnit.transform.position.x)
						{
							if (tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().unitOnTile == null
								&& tilesOnMap[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1].GetComponent<ClickableTile>().structureOnTile == null
								&& tileTypes[tiles[(int)tempUnit.transform.position.x, (int)tempUnit.transform.position.y - 1]].isWalkable)
								selectedUnit.transform.position = new Vector3(tempUnit.transform.position.x, tempUnit.transform.position.y - 1);
						}
						tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
						selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
						selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
						selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
						tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;
						selectedUnit.GetComponent<Unit>().wait();
						StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
                    }
					//Shield
					else if (ct.unitOnTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum
							 && attackableTiles.Contains(graph[unitX, unitY]) 
							 && selectedUnit.name == "mShield" || selectedUnit.name == "mShield(Clone)")
					{
						GameObject tempUnit = ct.unitOnTile;
						Vector2 position = new Vector2(selectedUnit.transform.position.x, selectedUnit.transform.position.y);
						selectedUnit.transform.position = new Vector2(tempUnit.transform.position.x, tempUnit.transform.position.y);
						tempUnit.transform.position = position;

						selectedUnit.GetComponent<Unit>().maxDmg = selectedUnit.GetComponent<Unit>().weaponDamage[ct.unitOnTile.GetComponent<Unit>().weaponID];
                        ct.unitOnTile.GetComponent<Unit>().maxDmg = ct.unitOnTile.GetComponent<Unit>().weaponDamage[selectedUnit.GetComponent<Unit>().weaponID];
						
						//selectedUnit.GetComponent<Weapons>().WeaponDamage(selectedUnit, ct.unitOnTile);
						bm.Battle(selectedUnit, ct.unitOnTile);

						tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;

						tempUnit.GetComponent<Unit>().tileX = (int)tempUnit.transform.position.x;
						tempUnit.GetComponent<Unit>().tileY = (int)tempUnit.transform.position.y;
						tempUnit.GetComponent<Unit>().unitPreviousX = tempUnit.GetComponent<Unit>().tileX;
						tempUnit.GetComponent<Unit>().unitPreviousY = tempUnit.GetComponent<Unit>().tileY;
						tilesOnMap[tempUnit.GetComponent<Unit>().tileX, tempUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = tempUnit;

						selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
						selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
						selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
						selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
						tilesOnMap[(int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y].GetComponent<ClickableTile>().unitOnTile = selectedUnit;
						selectedUnit.GetComponent<Unit>().tileBeingOccupied = tilesOnMap[(int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y];

						selectedUnit.GetComponent<Unit>().wait();
						StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
                    }
					//Molotov
					else if (ct.unitOnTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum
							 && attackableTiles.Contains(graph[unitX, unitY]) 
							 && selectedUnit.name == "pMolotov(Clone)" || selectedUnit.name == "pMolotov")
					{
						if (ct.unitOnTile.GetComponent<Unit>().currHp > 0)
						{
							GameObject tempUnit = ct.unitOnTile;
							GameObject tempTank = selectedUnit;
							tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;							
							selectedUnit.GetComponent<Unit>().maxDmg = selectedUnit.GetComponent<Unit>().weaponDamage[ct.unitOnTile.GetComponent<Unit>().weaponID];
                            ct.unitOnTile.GetComponent<Unit>().maxDmg = ct.unitOnTile.GetComponent<Unit>().weaponDamage[selectedUnit.GetComponent<Unit>().weaponID];
                            //selectedUnit.GetComponent<Weapons>().WeaponDamage(selectedUnit, ct.unitOnTile);
                            StartCoroutine(bm.GetComponent<BattleManager>().attack(selectedUnit, ct.unitOnTile));
							selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
							selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
							if (selectedUnit.name == "pMolotov" || selectedUnit.name == "pMolotov(Clone)")
							{
								GameObject tile = tempUnit.GetComponent<Unit>().tileBeingOccupied;
								tile.GetComponent<ClickableTile>().fireOn = true;
								tile.GetComponent<ClickableTile>().FireOnTile();
							}
							if (tempUnit.name == "pMolotov" || tempUnit.name == "pMolotov(Clone)")
							{
								GameObject tile = selectedUnit.GetComponent<Unit>().tileBeingOccupied;
								tile.GetComponent<ClickableTile>().fireOn = true;
								tile.GetComponent<ClickableTile>().FireOnTile();
							}
							selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
							selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
							tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;

							selectedUnit.GetComponent<Unit>().wait();
							StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
						}
                }				
				//Attack
				else if (ct.unitOnTile.GetComponent<Unit>().playerNum != selectedUnit.GetComponent<Unit>().playerNum
						 && attackableTiles.Contains(graph[unitX, unitY]) && (selectedUnit.name != "mMinitank(Clone)"
						 || selectedUnit.name != "mMinitank" || selectedUnit.name != "mShield" || selectedUnit.name != "mShield(Clone"
						 || selectedUnit.name != "pMolotov" || selectedUnit.name != "pMolotov(Clone)"))
				{
						if (ct.unitOnTile.GetComponent<Unit>().currHp > 0)
						{
							GameObject tempUnit = ct.unitOnTile;
							GameObject tempTank = selectedUnit;
							tilesOnMap[selectedUnit.GetComponent<Unit>().unitPreviousX, selectedUnit.GetComponent<Unit>().unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
                            selectedUnit.GetComponent<Unit>().maxDmg = selectedUnit.GetComponent<Unit>().weaponDamage[ct.unitOnTile.GetComponent<Unit>().weaponID];
                            ct.unitOnTile.GetComponent<Unit>().maxDmg = ct.unitOnTile.GetComponent<Unit>().weaponDamage[selectedUnit.GetComponent<Unit>().weaponID];
                            //selectedUnit.GetComponent<Weapons>().WeaponDamage(selectedUnit, ct.unitOnTile);
                            StartCoroutine(bm.GetComponent<BattleManager>().attack(selectedUnit, ct.unitOnTile));
							selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
							selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
							if (selectedUnit.name == "pTruck" || selectedUnit.name == "pTruck(Clone)")
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
							}
							if (tempUnit.name == "pMolotov" || tempUnit.name == "pMolotov(Clone)")
							{
								GameObject tile = selectedUnit.GetComponent<Unit>().tileBeingOccupied;
								tile.GetComponent<ClickableTile>().fireOn = true;
								tile.GetComponent<ClickableTile>().FireOnTile();
							}
							selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
							selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
							tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;

							selectedUnit.GetComponent<Unit>().wait();
							StartCoroutine(deselectAfterMovements(selectedUnit, ct.unitOnTile));
                        }
				}
			}
				//Structure
				else if (ct.structureOnTile != null)
				{
					if (ct.structureOnTile.GetComponent<Structure>().playerNum != selectedUnit.GetComponent<Unit>().playerNum
						&& ct.structureOnTile.GetComponent<Structure>().playerNum != 0
						&& ct.atkGrid.GetComponent<SpriteRenderer>().enabled == true)
					{
						if (ct.structureOnTile.GetComponent<Structure>().currHp > 0)
						{
							if (selectedUnit.name == "pKamikaze(Clone)" && ct.atkGrid.GetComponent<SpriteRenderer>().enabled == true)
							{
								selectedUnit.GetComponent<Explosion>().KamikazeAtk();
								StartCoroutine(deselectAfterMovements(selectedUnit, ct.structureOnTile));
								return;
							}
							Debug.Log("We clicked an structure enemy that should be attacked");
							Debug.Log(selectedUnit.GetComponent<Unit>().currHp);
							selectedUnit.GetComponent<Unit>().maxDmg = selectedUnit.GetComponent<Unit>().dmgStructure;   
                            //selectedUnit.GetComponent<Weapons>().WeaponDamage(selectedUnit, ct.structureOnTile);
                            StartCoroutine(bm.GetComponent<BattleManager>().attackStructure(selectedUnit, ct.structureOnTile));
							StartCoroutine(deselectAfterMovements(selectedUnit, ct.structureOnTile));
						}
					}
					else if (ct.structureOnTile.GetComponent<Structure>().CanAttack(selectedUnit) == true
							 && ct.atkGrid.GetComponent<SpriteRenderer>().enabled == true && selectedUnit.GetComponent<Unit>().playerNum != ct.structureOnTile.GetComponent<Structure>().playerNum)
					{
						Debug.Log("We capture the structure");
						ct.structureOnTile.GetComponent<Structure>().faction = selectedUnit.GetComponent<Unit>().faction;
						ct.structureOnTile.GetComponent<Structure>().playerNum = selectedUnit.GetComponent<Unit>().playerNum;
						disableHighlightUnitRange();
						selectedUnit.GetComponent<Unit>().wait();
						selectedUnit.GetComponent<Unit>().setMovementState(3);
						deselectUnit();
						gm.GetComponent<GameManager>().UpdateColors();

						if (ct.structureOnTile.name == "Mine" && ct.structureOnTile.GetComponent<Structure>().playerNum == 1)
						{
							gm.GetComponent<GameManager>().incomePlayer1 = gm.GetComponent<GameManager>().incomePlayer1 + 100;
							gm.GetComponent<GameManager>().MoneyUpdate();
						}
						else if (ct.structureOnTile.name == "Mine" && ct.structureOnTile.GetComponent<Structure>().playerNum == 2)
						{
							gm.GetComponent<GameManager>().incomePlayer2 = gm.GetComponent<GameManager>().incomePlayer2 + 100;
							gm.GetComponent<GameManager>().MoneyUpdate();
						}
					}
					else
						Debug.Log("No pudo");
				}
				//Exit Wagon
				else if (selectedUnit.GetComponent<Unit>().unitIn != null && selectedUnit.GetComponent<Unit>().wagon == true
						 && ct.moveGrid.GetComponent<SpriteRenderer>().enabled == true)
				{				
					Debug.Log("Salimos");
					//Unit
					ct.GetComponent<ClickableTile>().unitOnTile = selectedUnit.GetComponent<Unit>().unitIn;
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().tileBeingOccupied = gm.tileBeingDisplayed;
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().tileX = ct.tileX;
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().tileY = ct.tileY;
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().unitPreviousX = ct.tileX;
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().unitPreviousY = ct.tileY;
					selectedUnit.GetComponent<Unit>().unitIn.transform.position = new Vector3(ct.tileX, ct.tileY, 0);
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().wait();
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().setMovementState(3);
					selectedUnit.GetComponent<Unit>().unitIn.GetComponent<Unit>().inWagon = false;

					//wagon
					selectedUnit.GetComponent<Unit>().unitPreviousX = selectedUnit.GetComponent<Unit>().tileX;
					selectedUnit.GetComponent<Unit>().unitPreviousY = selectedUnit.GetComponent<Unit>().tileY;
					tilesOnMap[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY].GetComponent<ClickableTile>().unitOnTile = selectedUnit;				
					selectedUnit.GetComponent<Unit>().tileBeingOccupied = gm.tileBeingDisplayed;
					selectedUnit.GetComponent<Unit>().unitIn = null;
					selectedUnit.GetComponent<Unit>().wait();
					selectedUnit.GetComponent<Unit>().setMovementState(3);
					deselectUnit();
				}
			}
		}
		
	}

	void FixedStayUnit()
    {
		units = GameObject.FindGameObjectsWithTag("Unit");
		foreach (GameObject u in units)
        {			
			if (u.GetComponent<Unit>().inWagon == false)
				tilesOnMap[(int)u.transform.position.x, (int)u.transform.position.y].GetComponent<ClickableTile>().unitOnTile = u;
		}
    }

	public void DisableColliders()
    {
		GameObject[] tilesCollider = GameObject.FindGameObjectsWithTag("Tile");

		foreach (GameObject t in tilesCollider)
        {
			t.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //Online
    public abstract void SelectUnitMove(Vector2 coords);

    public abstract void SetSelectedUnit(Vector2 coords);

    public void OnSetSelectedUnit(Vector2Int coords)
    {
        GameObject unitOnSelect = GetUnitOnSquare(coords);
        selectedUnit = unitOnSelect;
    }

    public GameObject GetUnitOnSquare(Vector2Int coords)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            return tilesOnMap[coords.x, coords.y].GetComponent<ClickableTile>().unitOnTile;
        return null;
    }

    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= mapSizeX || coords.y >= mapSizeY)
            return false;
        return true;
    }
}

