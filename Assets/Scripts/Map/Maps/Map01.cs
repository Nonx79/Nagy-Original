using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01 : MonoBehaviour
{
    public TileMap map;

    private void Start()
    {
		GenerateMapData();
		GenerateMapVisual();
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
		map.tiles[7, 1] = 1;
		map.tiles[7, 2] = 1;
		map.tiles[8, 2] = 1;
		map.tiles[9, 2] = 1;
		map.tiles[10, 2] = 1;
		map.tiles[14, 2] = 1;
		map.tiles[8, 3] = 1;
		map.tiles[9, 3] = 1;
		map.tiles[13, 3] = 1;
		map.tiles[14, 3] = 1;
		map.tiles[15, 4] = 1;
		map.tiles[16, 4] = 1;
		map.tiles[6, 5] = 1;
		map.tiles[16, 5] = 1;
		map.tiles[6, 6] = 1;
		map.tiles[7, 6] = 1;
		map.tiles[12, 6] = 1;
		map.tiles[11, 7] = 1;
		map.tiles[12, 7] = 1;
		map.tiles[13, 7] = 1;
		map.tiles[14, 7] = 1;
		map.tiles[8, 8] = 1;
		map.tiles[13, 8] = 1;
		map.tiles[14, 8] = 1;
		map.tiles[7, 9] = 1;
		map.tiles[13, 9] = 1;
		map.tiles[14, 9] = 1;

		//Mountains
		map.tiles[1, 0] = 2;
		map.tiles[2, 0] = 2;
		map.tiles[3, 0] = 2;
		map.tiles[4, 0] = 2;
		map.tiles[5, 0] = 2;
		map.tiles[6, 0] = 2;
		map.tiles[7, 0] = 2;
		map.tiles[8, 0] = 2;
		map.tiles[9, 0] = 2;
		map.tiles[10, 0] = 2;
		map.tiles[17, 0] = 2;
		map.tiles[18, 0] = 2;
		map.tiles[19, 0] = 2;
		map.tiles[20, 0] = 2;
		map.tiles[2, 1] = 2;
		map.tiles[3, 1] = 2;
		map.tiles[9, 1] = 2;
		map.tiles[10, 1] = 2;
		map.tiles[18, 1] = 2;
		map.tiles[19, 1] = 2;
		map.tiles[20, 1] = 2;
		map.tiles[19, 2] = 2;
		map.tiles[20, 2] = 2;
		map.tiles[20, 8] = 2;
		map.tiles[1, 9] = 2;
		map.tiles[2, 9] = 2;
		map.tiles[3, 9] = 2;
		map.tiles[4, 9] = 2;
		map.tiles[5, 9] = 2;
		map.tiles[18, 9] = 2;
		map.tiles[19, 9] = 2;
		map.tiles[20, 9] = 2;

		map.tiles[0, 10] = 2;
		map.tiles[1, 10] = 2;
		map.tiles[2, 10] = 2;
		map.tiles[3, 10] = 2;
		map.tiles[4, 10] = 2;
		map.tiles[5, 10] = 2;
		map.tiles[6, 10] = 2;

		map.tiles[16, 10] = 2;
		map.tiles[17, 10] = 2;
		map.tiles[18, 10] = 2;
		map.tiles[19, 10] = 2;
		map.tiles[20, 10] = 2;

		// 1 Lake
		map.tiles[7, 10] = 3;
		map.tiles[8, 10] = 3;
		map.tiles[9, 10] = 3;
		map.tiles[10, 10] = 3;
		map.tiles[11, 10] = 3;
		map.tiles[12, 10] = 3;
		map.tiles[8, 9] = 3;
		map.tiles[9, 9] = 3;
		map.tiles[10, 9] = 3;
		map.tiles[11, 9] = 3;
		map.tiles[12, 9] = 3;
		map.tiles[9, 8] = 3;
		map.tiles[10, 8] = 3;
		map.tiles[11, 8] = 3;
		//2 Lake
		map.tiles[11, 0] = 3;
		map.tiles[12, 0] = 3;
		map.tiles[13, 0] = 3;
		map.tiles[14, 0] = 3;
		map.tiles[15, 0] = 3;
		map.tiles[16, 0] = 3;
		map.tiles[11, 1] = 3;
		map.tiles[12, 1] = 3;
		map.tiles[13, 1] = 3;
		map.tiles[14, 1] = 3;
		map.tiles[15, 1] = 3;
		map.tiles[11, 2] = 3;
		map.tiles[12, 2] = 3;
		map.tiles[13, 2] = 3;
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
}
