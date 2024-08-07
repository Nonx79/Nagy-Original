using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
	public void WeaponDamage(GameObject unitDoDmg, GameObject unitToAttack)
	{
		switch (unitDoDmg.name)
		{
			case "mSniperCommander":
			case "mSniperCommander(Clone)":
				if (unitDoDmg.GetComponent<Unit>().powerBool == true)
				{
					switch (unitToAttack.name)
					{
						case "mSniperCommander":
						case "mSniperCommander(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 50;	
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mSoldier":
						case "mSoldier(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 100;
							unitToAttack.GetComponent<Unit>().maxDmg = 10;
							break;
						case "mSniper":
						case "mSniper(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 100;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mMinitank":
						case "mMinitank(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 70;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mWagon":
						case "mWagon(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 80;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "mInvokeMachine":
						case "mInvokeMachine(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 80;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "mShield":
						case "mShield(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 65;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mAssessin":
						case "mAssessin(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 70;
							unitToAttack.GetComponent<Unit>().maxDmg = 45;
							break;
						//Povery
						case "pMechanicCommander":
						case "pMechanicCommander(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 55;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pSoldier":
						case "pSoldier(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 100;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pKamikaze":
						case "pKamikaze(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 100;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pRange":
						case "pRange(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 80;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pAnimator":
						case "pAnimator(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 100;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "pTruck":
						case "pTruck(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 75;
							unitToAttack.GetComponent<Unit>().maxDmg = 20;
							break;
						case "pMolotov":
						case "pMolotov(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 95;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pWagon":
						case "pWagon(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 80;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
					}
					break;
				}
				else
                {
					switch (unitToAttack.name)
					{
						case "mSniperCommander":
						case "mSniperCommander(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 40;
							unitToAttack.GetComponent<Unit>().maxDmg = 40;
							break;
						case "mSoldier":
						case "mSoldier(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 80;
							unitToAttack.GetComponent<Unit>().maxDmg = 10;
							break;
						case "mSniper":
						case "mSniper(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 45;
							unitToAttack.GetComponent<Unit>().maxDmg = 25;
							break;
						case "mMinitank":
						case "mMinitank(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 45;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mWagon":
						case "mWagon(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 65;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "mInvokeMachine":
						case "mInvokeMachine(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 65;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "mShield":
						case "mShield(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 40;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "mAssessin":
						case "mAssessin(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 50;
							unitToAttack.GetComponent<Unit>().maxDmg = 45;
							break;
						//Povery
						case "pMechanicCommander":
						case "pMechanicCommander(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 40;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "pSoldier":
						case "pSoldier(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 75;
							unitToAttack.GetComponent<Unit>().maxDmg = 5;
							break;
						case "pKamikaze":
						case "pKamikaze(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 90;
							unitToAttack.GetComponent<Unit>().maxDmg = 30;
							break;
						case "pRange":
						case "pRange(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 50;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pAnimator":
						case "pAnimator(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 60;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;
						case "pTruck":
						case "pTruck(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 55;
							unitToAttack.GetComponent<Unit>().maxDmg = 20;
							break;
						case "pMolotov":
						case "pMolotov(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 70;
							unitToAttack.GetComponent<Unit>().maxDmg = 15;
							break;
						case "pWagon":
						case "pWagon(Clone)":
							unitDoDmg.GetComponent<Unit>().maxDmg = 75;
							unitToAttack.GetComponent<Unit>().maxDmg = 0;
							break;						
					}
					break;
				}
			case "mSoldier":
			case "mSoldier(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
                    case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 10;
						unitToAttack.GetComponent<Unit>().maxDmg = 85;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;	
						unitToAttack.GetComponent<Unit>().maxDmg = 55;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 80;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 15;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 20;
						unitToAttack.GetComponent<Unit>().maxDmg = 65;
						break;
						//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 15;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 25;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "mSniper":
			case "mSniper(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 25;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 80;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 60;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 60;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 60;
						break;
					//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 85;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 55;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 10;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "mMinitank":
			case "mMinitank(Clone)":
				switch(unitToAttack.name)
                {
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 55;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;					
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;					
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 25;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 85;
						unitToAttack.GetComponent<Unit>().maxDmg = 25;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 95;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 80;
						unitToAttack.GetComponent<Unit>().maxDmg = 5;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 80;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "mShield":
			case "mShield(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 25;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 10;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 85;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 25;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 80;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 10;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "mAssessin":
			case "mAssessin(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 65;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 65;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 95;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 60;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
				}
				break;
			case "pMechanicCommander":
			case "pMechanicCommander(Clone)":
				switch (unitToAttack.name)
                {
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 45;
						break;
					//Povery (falta de aqui en adelante)
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 5;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "pSoldier":
			case "pSoldier(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 5;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 65;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 30;
						unitToAttack.GetComponent<Unit>().maxDmg = 90;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 25;
						unitToAttack.GetComponent<Unit>().maxDmg = 85;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 25;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 10;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 15;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					//Povery
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 5;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "pRange":
			case "pRange(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 15;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 65;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 70;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 45;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 25;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 35;
						unitToAttack.GetComponent<Unit>().maxDmg = 65;
						break;
					//Povery (falta de aqui en adelante)
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 5;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 50;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
			case "pTruck":
			case "pTruck(Clone)":
				switch (unitToAttack.name)
				{
					case "mSniperCommander":
					case "mSniperCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 20;
						unitToAttack.GetComponent<Unit>().maxDmg = 55;
						break;
					case "mSoldier":
					case "mSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "mSniper":
					case "mSniper(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 55;
						break;
					case "mMinitank":
					case "mMinitank(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 60;
						break;
					case "mWagon":
					case "mWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mInvokeMachine":
					case "mInvokeMachine(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "mShield":
					case "mShield(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 70;
						unitToAttack.GetComponent<Unit>().maxDmg = 35;
						break;
					case "mAssessin":
					case "mAssessin(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					//Povery (falta de aqui en adelante)
					case "pMechanicCommander":
					case "pMechanicCommander(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 40;
						unitToAttack.GetComponent<Unit>().maxDmg = 40;
						break;
					case "pSoldier":
					case "pSoldier(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 5;
						break;
					case "pKamikaze":
					case "pKamikaze(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 90;
						unitToAttack.GetComponent<Unit>().maxDmg = 30;
						break;
					case "pRange":
					case "pRange(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 50;
						unitToAttack.GetComponent<Unit>().maxDmg = 15;
						break;
					case "pAnimator":
					case "pAnimator(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
					case "pTruck":
					case "pTruck(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 55;
						unitToAttack.GetComponent<Unit>().maxDmg = 20;
						break;
					case "pMolotov":
					case "pMolotov(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 60;
						unitToAttack.GetComponent<Unit>().maxDmg = 10;
						break;
					case "pWagon":
					case "pWagon(Clone)":
						unitDoDmg.GetComponent<Unit>().maxDmg = 75;
						unitToAttack.GetComponent<Unit>().maxDmg = 0;
						break;
				}
				break;
		}
	}
}
