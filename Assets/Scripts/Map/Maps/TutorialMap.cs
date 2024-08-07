using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMap : MonoBehaviour
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
