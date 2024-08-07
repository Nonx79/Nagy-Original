using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour 
{
	public List<ClickableTile> adjacentTiles = new List<ClickableTile>();

	public int tileX;
	public int tileY;
	
	public GameObject moveGrid;
	public GameObject atkGrid;
	public TileMap map;
	public GameObject unitOnTile;
	public GameObject structureOnTile;
	public GameObject selectedGrid;
	public GameObject fire;
	public GameManager fireTile;
	public bool fireActiveted = false;
	public bool isWalk = false;
	public bool fireOn = false;
	public int fireTime = 0;

	private void Awake()
    {
		tileX = (int)transform.position.x;
		tileY = (int)transform.position.y;
		map = FindObjectOfType<TileMap>();
		if (gameObject.name == "Ocean(Clone)")
		{
			isWalk = true;
		}
	}

    private void Update()
	{
		if (moveGrid == null)		
			Positions();
    }

	public void FireOnTile()
	{
		if (fireOn == true && fireActiveted == false)
		{
			GameObject fireTile = Instantiate(fire, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
			fireActiveted = true;
		}
		else
			foreach (GameObject fire in GameObject.FindGameObjectsWithTag("Fire"))
			{
				if (fireOn == false)
				{
					Destroy(fire);
				}
			}
	}

	public void Positions()
	{
		GameObject[] grids = GameObject.FindGameObjectsWithTag("Move Grid");

		GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");

		GameObject[] selectedGrids = GameObject.FindGameObjectsWithTag("Selected Grid");

		GameObject[] attableGrids = GameObject.FindGameObjectsWithTag("Attack Grid");

		foreach (GameObject g in grids)
		{
			if (g.transform.position.x == tileX && g.transform.position.y == tileY)
			{
				moveGrid = g;
			}
		}

		foreach (GameObject s in structures)
		{
			if (s.transform.position.x == tileX && s.transform.position.y == tileY)
			{
				structureOnTile = s;
			}
		}

		foreach (GameObject sg in selectedGrids)
		{
			if (sg.transform.position.x == tileX && sg.transform.position.y == tileY)
			{
				selectedGrid = sg;
			}
		}

		foreach (GameObject ag in attableGrids)
		{
			if (ag.transform.position.x == tileX && ag.transform.position.y == tileY)
			{
				atkGrid = ag;
			}
		}
	}
}
