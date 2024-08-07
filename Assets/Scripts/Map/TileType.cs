using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileType
{
	public string name;
	public GameObject tileVisualPrefab2;
	public TileBase tileVisualPrefab;

	public bool isWalkable = true;
	public float movementCost = 1;
}
