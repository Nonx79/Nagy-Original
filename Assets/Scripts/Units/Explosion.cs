using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
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

    public void Kamikaze4(GameObject kamikaze, GameObject unit1, GameObject unit2, GameObject unit3, GameObject unit4)
	{
		switch (unit1.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
		}

		switch (unit2.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
		}

		switch (unit3.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 35;
				break;
		}

		switch (unit4.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg4 = 35;
				break;
		}
	}

	public void Kamikaze3(GameObject kamikaze, GameObject unit1, GameObject unit2, GameObject unit3)
	{
		switch (unit1.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
		}

		switch (unit2.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
		}

		switch (unit3.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg3 = 35;
				break;
		}
	}

	public void Kamikaze2(GameObject kamikaze, GameObject unit1, GameObject unit2)
	{
		switch (unit1.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
		}

		switch (unit2.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg2 = 35;
				break;
		}
	}

	public void Kamikaze1(GameObject kamikaze, GameObject unit1)
	{
		switch (unit1.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mSoldier":
			case "mSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 70;
				break;
			case "mSniper":
			case "mSniper(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 45;
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 20;
				break;
			case "mWagon":
			case "mWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "mInvokeMachine":
			case "mInvokeMachine(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 25;
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 90;
				break;
			case "pKamikaze":
			case "pKamikaze(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 80;
				break;
			case "pRange":
			case "pRange(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 75;
				break;
			case "pAnimator":
			case "pAnimator(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 95;
				break;
			case "pTruck":
			case "pTruck(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 30;
				break;
			case "pWagon":
			case "pWagon(Clone)":
				kamikaze.GetComponent<Unit>().dmg1 = 35;
				break;
		}
	}

	public void KamikazeAtk()
    {
		int x = (int)map.selectedUnit.transform.position.x;
		int y = (int)map.selectedUnit.transform.position.y;
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

		foreach (GameObject u in units)
		{
			if (u.transform.position.x == x - 1 && u.transform.position.y == y)
			{
				units1 = u;
			}

			if (u.transform.position.x == x + 1 && u.transform.position.y == y)
			{
				units2 = u;
			}

			if (u.transform.position.y == y - 1 && u.transform.position.x == x)
			{
				units3 = u;
			}

			if (u.transform.position.y == y + 1 && u.transform.position.x == x)
			{
				units4 = u;
			}
		}


		if (units1 != null && units2 != null && units3 != null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze4(map.selectedUnit, units1, units2, units3, units4);
			bm.GetComponent<BattleManager>().Explosion4(map.selectedUnit, units1, units2, units3, units4);
		}

		else if (units1 == null && units2 != null && units3 != null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze3(map.selectedUnit, units2, units3, units4);
			bm.GetComponent<BattleManager>().Explosion3(map.selectedUnit, units2, units3, units4);
		}

		else if (units1 != null && units2 == null && units3 != null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze3(map.selectedUnit, units1, units3, units4);
			bm.GetComponent<BattleManager>().Explosion3(map.selectedUnit, units1, units3, units4);
		}

		else if (units1 != null && units2 != null && units3 == null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze3(map.selectedUnit, units1, units2, units4);
			bm.GetComponent<BattleManager>().Explosion3(map.selectedUnit, units1, units2, units4);
		}

		else if (units1 != null && units2 != null && units3 != null && units4 == null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze3(map.selectedUnit, units1, units2, units3);
			bm.GetComponent<BattleManager>().Explosion3(map.selectedUnit, units1, units2, units3);
		}

		else if (units1 == null && units2 == null && units3 != null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units3, units4);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units3, units4);
		}

		else if (units1 == null && units2 != null && units3 == null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units2, units4);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units2, units4);
		}

		else if (units1 == null && units2 != null && units3 != null && units4 == null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units2, units3);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units2, units3);
		}

		else if (units1 != null && units2 == null && units3 == null && units4 != null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units1, units4);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units1, units4);
		}

		else if (units1 != null && units2 == null && units3 != null && units4 == null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units1, units3);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units1, units3);
		}

		else if (units1 != null && units2 != null && units3 == null && units4 == null)
		{
			map.selectedUnit.GetComponent<Explosion>().Kamikaze2(map.selectedUnit, units1, units2);
			bm.GetComponent<BattleManager>().Explosion2(map.selectedUnit, units1, units2);
		}

		else if (units1 != null && units2 == null && units3 == null && units4 == null)
		{
			Kamikaze1(map.selectedUnit, units1);
			bm.GetComponent<BattleManager>().Explosion1(map.selectedUnit, units1);
		}

		else if (units1 == null && units2 != null && units3 == null && units4 == null)
		{
			Kamikaze1(map.selectedUnit, units2);
			bm.GetComponent<BattleManager>().Explosion1(map.selectedUnit, units2);
		}

		else if (units1 == null && units2 == null && units3 != null && units4 == null)
		{
			Kamikaze1(map.selectedUnit, units3);
			bm.GetComponent<BattleManager>().Explosion1(map.selectedUnit, units3);
		}

		else if (units1 == null && units2 == null && units3 == null && units4 != null)
		{
			Kamikaze1(map.selectedUnit, units4);
			bm.GetComponent<BattleManager>().Explosion1(map.selectedUnit, units4);
		}
	}
}
