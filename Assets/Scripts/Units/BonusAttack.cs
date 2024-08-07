using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAttack : MonoBehaviour
{
	TileMap map;
	GameObject bm;

	public GameObject units1;
	public GameObject units2;
	public GameObject units3;
	public GameObject units4;

	private void Awake()
	{
		map = FindObjectOfType<TileMap>();
		bm = GameObject.FindGameObjectWithTag("GameController");
	}

	public void AttackPlus()
	{
		int x = (int)map.selectedUnit.transform.position.x;
		int y = (int)map.selectedUnit.transform.position.y;
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

		foreach (GameObject u in units)
		{
			if (u.transform.position.x == x - 1 && u.transform.position.y == y && u.name == "pAnimator" 
				|| u.transform.position.x == x - 1 && u.transform.position.y == y && u.name == "pAnimator(Clone)")
			{
				units1 = u;
			}

			if (u.transform.position.x == x + 1 && u.transform.position.y == y && u.name == "pAnimator" 
				|| u.transform.position.x == x + 1 && u.transform.position.y == y && u.name == "pAnimator(Clone)")
			{
				units2 = u;
			}

			if (u.transform.position.y == y - 1 && u.transform.position.x == x && u.name == "pAnimator" 
				|| u.transform.position.y == y - 1 && u.transform.position.x == x && u.name == "pAnimator(Clone)")
			{
				units3 = u;
			}

			if (u.transform.position.y == y + 1 && u.transform.position.x == x && u.name == "pAnimator" 
				|| u.transform.position.y == y + 1 && u.transform.position.x == x && u.name == "pAnimator(Clone)")
			{
				units4 = u;
			}
		}

		//4
		if (units1 != null && units2 != null && units3 != null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 40;
		}

		//3
		else if (units1 == null && units2 != null && units3 != null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 20;
		}
		else if (units1 != null && units2 == null && units3 != null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 20;
		}

		else if (units1 != null && units2 != null && units3 == null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 20;
		}

		else if (units1 != null && units2 != null && units3 != null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 20;
		}

		//2
		else if (units1 == null && units2 == null && units3 != null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		else if (units1 == null && units2 != null && units3 == null && units4 != null	
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum	
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		else if (units1 == null && units2 != null && units3 != null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		else if (units1 != null && units2 == null && units3 == null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		else if (units1 != null && units2 == null && units3 != null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units3.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		else if (units1 != null && units2 != null && units3 == null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 15;
		}

		//1
		else if (units1 != null && units2 == null && units3 == null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units1.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 10;
		}

		else if (units1 == null && units2 != null && units3 == null && units4 == null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 10;
		}

		else if (units1 == null && units2 == null && units3 != null && units4 == null 
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units2.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 10;
		}

		else if (units1 == null && units2 == null && units3 == null && units4 != null
			&& map.selectedUnit.GetComponent<Unit>().playerNum == units4.GetComponent<Unit>().playerNum)
		{
			map.selectedUnit.GetComponent<Unit>().maxDmg = map.selectedUnit.GetComponent<Unit>().maxDmg + 10;
		}
	}
}
