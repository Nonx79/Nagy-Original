using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map02 : MonoBehaviour
{
	public TileMap map;
	public GameManager gm;
	public GameObject positionPlayer11, positionPlayer12,
					  positionPlayer21, positionPlayer22, positionPlayer23;
	public Structure sc;
	string color;

	private void Start()
	{
		GenerateMapData();
		GenerateMapVisual();
		positionPlayer11.GetComponent<SpriteRenderer>().color = gm.morado;
		positionPlayer12.GetComponent<SpriteRenderer>().color = gm.morado;
		positionPlayer21.GetComponent<SpriteRenderer>().color = gm.rosa;
		positionPlayer22.GetComponent<SpriteRenderer>().color = gm.rosa;
		positionPlayer23.GetComponent<SpriteRenderer>().color = gm.rosa;
	}

	void GenerateMapData()
	{
		// Allocate our map tiles
		map.tiles = new int[map.mapSizeX, map.mapSizeY];

		int x, y;

		// Initialize our map tiles to be grass
		for (x = 0; x < map.mapSizeX; x++)
		{
			for (y = 0; y < map.mapSizeY; y++)
			{
				map.tiles[x, y] = 0;
			}
		}

		//Trees
		map.tiles[3, 0] = 1;
		map.tiles[4, 0] = 1;
		map.tiles[6, 0] = 1;
		map.tiles[7, 0] = 1;
		map.tiles[9, 0] = 1;
		map.tiles[10, 0] = 1;
		map.tiles[11, 0] = 1;
		map.tiles[15, 0] = 1;
		map.tiles[17, 0] = 1;
		map.tiles[18, 0] = 1;
		map.tiles[2, 1] = 1;
		map.tiles[3, 1] = 1;
		map.tiles[10, 1] = 1;
		map.tiles[16, 1] = 1;
		map.tiles[17, 1] = 1;
		map.tiles[18, 1] = 1;
		map.tiles[1, 2] = 1;
		map.tiles[2, 2] = 1;
		map.tiles[5, 2] = 1;
		map.tiles[7, 2] = 1;
		map.tiles[12, 2] = 1;
		map.tiles[17, 2] = 1;
		map.tiles[18, 2] = 1;
		map.tiles[18, 3] = 1;
		map.tiles[0, 4] = 1;
		map.tiles[5, 5] = 1;
		map.tiles[12, 5] = 1;
		map.tiles[13, 5] = 1;
		map.tiles[17, 5] = 1;
		map.tiles[18, 5] = 1;
		map.tiles[7, 6] = 1;
		map.tiles[17, 6] = 1;
		map.tiles[9, 7] = 1;
		map.tiles[10, 7] = 1;
		map.tiles[12, 7] = 1;
		map.tiles[13, 7] = 1;
		map.tiles[17, 7] = 1;
		map.tiles[0, 8] = 1;
		map.tiles[16, 8] = 1;
		map.tiles[17, 8] = 1;
		map.tiles[16, 9] = 1;
		map.tiles[17, 9] = 1;
		map.tiles[3, 11] = 1;
		map.tiles[4, 11] = 1;
		map.tiles[7, 11] = 1;
		map.tiles[17, 11] = 1;
		map.tiles[7, 12] = 1;
		map.tiles[8, 12] = 1;
		map.tiles[10, 12] = 1;
		map.tiles[11, 12] = 1;
		map.tiles[1, 13] = 1;
		map.tiles[11, 13] = 1;
		map.tiles[14, 13] = 1;
		map.tiles[15, 13] = 1;
		map.tiles[1, 14] = 1;
		map.tiles[2, 14] = 1;
		map.tiles[1, 15] = 1;
		map.tiles[1, 16] = 1;
		map.tiles[2, 16] = 1;
		map.tiles[18, 16] = 1;
		map.tiles[1, 17] = 1;
		map.tiles[5, 17] = 1;
		map.tiles[6, 17] = 1;
		map.tiles[8, 17] = 1;
		map.tiles[9, 17] = 1;
		map.tiles[1, 18] = 1;
		map.tiles[11, 18] = 1;
		map.tiles[0, 19] = 1;
		map.tiles[1, 19] = 1;
		map.tiles[5, 19] = 1;
		map.tiles[6, 19] = 1;
		map.tiles[13, 19] = 1;
		map.tiles[18, 20] = 1;
		map.tiles[0, 21] = 1;
		map.tiles[0, 22] = 1;
		map.tiles[1, 22] = 1;
		map.tiles[6, 22] = 1;
		map.tiles[11, 22] = 1;
		map.tiles[13, 22] = 1;
		map.tiles[16, 22] = 1; 
		map.tiles[17, 22] = 1;
		map.tiles[0, 23] = 1;
		map.tiles[1, 23] = 1;
		map.tiles[2, 23] = 1;
		map.tiles[0, 24] = 1;
		map.tiles[1, 24] = 1;
		map.tiles[2, 24] = 1;
		map.tiles[3, 24] = 1;
		map.tiles[7, 24] = 1;
		map.tiles[8, 24] = 1;
		map.tiles[9, 24] = 1;
		map.tiles[11, 24] = 1;
		map.tiles[12, 24] = 1;
		map.tiles[14, 24] = 1;
		map.tiles[15, 24] = 1;

		//Mountains
		map.tiles[0, 0] = 2;
		map.tiles[1, 0] = 2;
		map.tiles[2, 0] = 2;
		map.tiles[0, 1] = 2;
		map.tiles[1, 1] = 2;
		map.tiles[0, 2] = 2;
		map.tiles[17, 3] = 2;
		map.tiles[12, 4] = 2;
		map.tiles[17, 4] = 2;
		map.tiles[18, 4] = 2;
		map.tiles[18, 6] = 2;
		map.tiles[6, 8] = 2;
		map.tiles[7, 8] = 2;
		map.tiles[6, 9] = 2;
		map.tiles[0, 10] = 2;
		map.tiles[1, 10] = 2;
		map.tiles[2, 10] = 2;
		map.tiles[3, 10] = 2;
		map.tiles[0, 11] = 2;
		map.tiles[1, 11] = 2;
		map.tiles[2, 11] = 2;
		map.tiles[18, 11] = 2;
		map.tiles[0, 12] = 2;
		map.tiles[1, 12] = 2;
		map.tiles[17, 12] = 2;
		map.tiles[18, 12] = 2;
		map.tiles[0, 13] = 2;
		map.tiles[16, 13] = 2;
		map.tiles[17, 13] = 2;
		map.tiles[18, 13] = 2;
		map.tiles[15, 13] = 2;
		map.tiles[16, 13] = 2;
		map.tiles[17, 13] = 2;
		map.tiles[18, 14] = 2;
		map.tiles[12, 15] = 2;
		map.tiles[0, 18] = 2;
		map.tiles[0, 20] = 2;
		map.tiles[1, 20] = 2;
		map.tiles[6, 20] = 2;
		map.tiles[1, 21] = 2;
		map.tiles[18, 22] = 2;
		map.tiles[17, 23] = 2;
		map.tiles[18, 23] = 2;
		map.tiles[16, 24] = 2;
		map.tiles[17, 24] = 2;
		map.tiles[18, 24] = 2;

		// 1 Lake
		map.tiles[0, 3] = 3;
		map.tiles[1, 3] = 3;
		map.tiles[2, 3] = 3;
		map.tiles[3, 3] = 3;
		map.tiles[2, 4] = 3;
		map.tiles[3, 4] = 3;
		map.tiles[3, 5] = 3;
		map.tiles[3, 7] = 3;
		map.tiles[18, 7] = 3;
		map.tiles[2, 8] = 3;
		map.tiles[3, 8] = 3;
		map.tiles[18, 8] = 3;
		map.tiles[0, 9] = 3;
		map.tiles[1, 9] = 3;
		map.tiles[2, 9] = 3;
		map.tiles[3, 9] = 3;
		map.tiles[18, 9] = 3;
		map.tiles[18, 10] = 3;
		map.tiles[0, 14] = 3;
		map.tiles[0, 15] = 3;
		map.tiles[15, 15] = 3;
		map.tiles[16, 15] = 3;
		map.tiles[17, 15] = 3;
		map.tiles[18, 15] = 3;
		map.tiles[0, 16] = 3;
		map.tiles[15, 16] = 3;
		map.tiles[16, 16] = 3;
		map.tiles[0, 17] = 3;
		map.tiles[15, 17] = 3;
		map.tiles[15, 19] = 3;
		map.tiles[15, 20] = 3;
		map.tiles[16, 20] = 3;
		map.tiles[15, 21] = 3;
		map.tiles[16, 21] = 3;
		map.tiles[17, 21] = 3;
		map.tiles[18, 21] = 3;
	}

	void GenerateMapVisual()
	{
		map.tilesOnMap = new GameObject[map.mapSizeX, map.mapSizeY];
		map.quadOnMap = new GameObject[map.mapSizeX, map.mapSizeY];
		map.quadOnMapAtk = new GameObject[map.mapSizeX, map.mapSizeY];
		map.quadOnMapSelected = new GameObject[map.mapSizeX, map.mapSizeY];

		for (int x = 0; x < map.mapSizeX; x++)
		{
			for (int y = 0; y < map.mapSizeY; y++)
			{
				TileType tt = map.tileTypes[map.tiles[x, y]];

				GameObject newTile = (GameObject)Instantiate(tt.tileVisualPrefab2, new Vector3(x, y, 0), Quaternion.identity);

				ClickableTile ct = newTile.GetComponent<ClickableTile>();
				ct.tileX = x;
				ct.tileY = y;
				ct.map = FindObjectOfType<TileMap>();
				/*
				ct.gm = this.gameObject;
				ct.bm = this.gameObject;
				*/
				map.tilesOnMap[x, y] = newTile;

				newTile.transform.SetParent(map.tileContainer.transform);
				map.ground.SetTile(new Vector3Int(x, y, 0), tt.tileVisualPrefab);

				GameObject gridUI = Instantiate(map.moveGrid, new Vector3(x, y, 0), Quaternion.identity);
				gridUI.transform.SetParent(map.UIQuadPotentialMovesContainer.transform);
				map.quadOnMap[x, y] = gridUI;

				GameObject gridUIAtk = Instantiate(map.atkGrid, new Vector3(x, y, 0), Quaternion.identity);
				gridUIAtk.transform.SetParent(map.gridUIAtkContainer.transform);
				map.quadOnMapAtk[x, y] = gridUIAtk;

				GameObject gridUISelected = Instantiate(map.selectedGrid, new Vector3(x, y, 0), Quaternion.identity);
				gridUISelected.transform.SetParent(map.gridUISelectedContainer.transform);
				map.quadOnMapSelected[x, y] = gridUISelected;
			}
		}
	}

    private void Update()
    {		
		if (gm.creation == true && positionPlayer23 != null)
        {
			GameObject obj1;
			GameObject obj2;
			GameObject obj3;
			GameObject obj4;
			GameObject obj5;
			if (gm.player1Commander.GetComponent<Unit>().faction == 0)
            {
				obj1 = Instantiate(sc.mSoldier, positionPlayer11.transform.position, Quaternion.identity);
				obj2 = Instantiate(sc.mSoldier, positionPlayer12.transform.position, Quaternion.identity);
				obj1.GetComponent<Unit>().playerNum = 1;
				obj2.GetComponent<Unit>().playerNum = 1;
				if (gm.IAPlayer1 == true)
				{
					obj1.GetComponent<Unit>().ia = true;
					obj2.GetComponent<Unit>().ia = true;
				}
				obj1.transform.SetParent(gm.player1.transform);
				obj2.transform.SetParent(gm.player1.transform);
				
			}
			else if (gm.player1Commander.GetComponent<Unit>().faction == 1)
            {
				obj1 = Instantiate(sc.pSoldier, positionPlayer11.transform.position, Quaternion.identity);
				obj2 = Instantiate(sc.pSoldier, positionPlayer12.transform.position, Quaternion.identity);
				obj1.GetComponent<Unit>().playerNum = 1;
				obj2.GetComponent<Unit>().playerNum = 1;
				if (gm.IAPlayer1 == true)
				{
					obj1.GetComponent<Unit>().ia = true;
					obj2.GetComponent<Unit>().ia = true;
				}
				obj1.transform.SetParent(gm.player1.transform);
				obj2.transform.SetParent(gm.player1.transform);

				color = gm.colorUnit;				
			}

			if (gm.player2Commander.GetComponent<Unit>().faction == 0)
            {
				obj3 = Instantiate(sc.mSoldier, positionPlayer21.transform.position, Quaternion.identity);
				obj4 = Instantiate(sc.mSoldier, positionPlayer22.transform.position, Quaternion.identity);
				obj5 = Instantiate(sc.mSoldier, positionPlayer23.transform.position, Quaternion.identity);

				obj3.GetComponent<Unit>().playerNum = 2;
				obj4.GetComponent<Unit>().playerNum = 2;
				obj5.GetComponent<Unit>().playerNum = 2;
				if (gm.IAPlayer2 == true)
				{
					obj3.GetComponent<Unit>().ia = true;
					obj4.GetComponent<Unit>().ia = true;
					obj5.GetComponent<Unit>().ia = true;
				}
				obj3.transform.SetParent(gm.player2.transform);
				obj4.transform.SetParent(gm.player2.transform);
				obj5.transform.SetParent(gm.player2.transform);
				color = gm.colorUnit;				
			}

			else if (gm.player2Commander.GetComponent<Unit>().faction == 1)
            {
				obj3 = Instantiate(sc.pSoldier, positionPlayer21.transform.position, Quaternion.identity);
				obj4 = Instantiate(sc.pSoldier, positionPlayer22.transform.position, Quaternion.identity);
				obj5 = Instantiate(sc.pSoldier, positionPlayer23.transform.position, Quaternion.identity);

				obj3.GetComponent<Unit>().playerNum = 2;
				obj4.GetComponent<Unit>().playerNum = 2;
				obj5.GetComponent<Unit>().playerNum = 2;
				if (gm.IAPlayer2 == true)
				{
					obj3.GetComponent<Unit>().ia = true;
					obj4.GetComponent<Unit>().ia = true;
					obj5.GetComponent<Unit>().ia = true;
				}
				obj3.transform.SetParent(gm.player2.transform);
				obj4.transform.SetParent(gm.player2.transform);
				obj5.transform.SetParent(gm.player2.transform);

				color = gm.colorUnit;				
			}
											
			gm.UpdateColors();
			Destroy(positionPlayer11);
			Destroy(positionPlayer12);
			Destroy(positionPlayer21);
			Destroy(positionPlayer22);
			Destroy(positionPlayer23);
			gm.creation = false;
			gm.UpdateUnits();
		}
    }
}
