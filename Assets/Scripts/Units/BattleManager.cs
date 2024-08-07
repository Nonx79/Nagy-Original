using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{    
    private bool battleStatus;
    public GameManager gm;
    public TileMap map;

    //Contra Unidades
    public void Battle(GameObject initiator, GameObject recipient)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recUnit = recipient.GetComponent<Unit>();

        int iniRest;
        int recRest;

        int iniDmg = Random.Range(iniUnit.maxDmg - 9, iniUnit.maxDmg);
        int recDmg = Random.Range(recUnit.maxDmg - 9, recUnit.maxDmg); 

        if (iniUnit.currHp != 100)
        {
            iniRest = 100 - iniUnit.currHp;
            iniRest = 100 - iniRest;
            iniDmg = iniRest * iniDmg / 100;
        }

        iniUnit.GetComponent<BonusAttack>();

        if (iniDmg <= 0)
            iniDmg = 1;

        if (recDmg <= 0)
            recDmg = 1;

        if (recUnit.CanAttack(iniUnit.gameObject) == true)
        {
            recUnit.DealDamage(iniDmg);
            Debug.Log("Inicial dio daño");
            if (initiator.GetComponent<Unit>().lider == true)
            {
                if (initiator.GetComponent<Unit>().power >= initiator.GetComponent<Unit>().maxPower)
                {

                }
                else
                    initiator.GetComponent<Unit>().power = initiator.GetComponent<Unit>().power + 10;
            }
            if (CheckIfDead(recipient))
            {
                if (recipient.GetComponent<Unit>().lider == true)
                {
                       Destroy(recipient);
                       gm.GetComponent<GameManager>().GameOver();
                }
                else
                {
                    Destroy(recipient);
                    if (initiator.GetComponent<Unit>().lider == true)
                    {
                        if (initiator.GetComponent<Unit>().power >= initiator.GetComponent<Unit>().maxPower)
                        {

                        }
                        else
                            initiator.GetComponent<Unit>().power = initiator.GetComponent<Unit>().power + 10;
                    }
                    if (initiator.name == "mAssessin" || initiator.name == "mAssessin(Clone)")
                    {
                        iniUnit.kill = true;
                    }
                    battleStatus = false;
                    return;
                }
            }

            if (recUnit.currHp != 100)
            {
                recRest = 100 - recUnit.currHp;
                recRest = 100 - recRest;
                recDmg = recRest * recDmg / 100;
            }
            recUnit.GetComponent<BonusAttack>();

            iniUnit.DealDamage(recDmg);
            Debug.Log("receptor dio daño");
            if (recipient.GetComponent<Unit>().lider == true)
            {
                if (recipient.GetComponent<Unit>().power >= recipient.GetComponent<Unit>().maxPower)
                {

                }
                else
                    recipient.GetComponent<Unit>().power = recipient.GetComponent<Unit>().power + 10;
            }
            if (CheckIfDead(initiator))
            {
                if (initiator.GetComponent<Unit>().lider == true)
                {
                    gm.GetComponent<GameManager>().GameOver();
                    Destroy(initiator);
                }
                else
                {
                    Destroy(initiator); 
                    if (recipient.GetComponent<Unit>().lider == true)
                    {
                        if (recipient.GetComponent<Unit>().power >= recipient.GetComponent<Unit>().maxPower)
                        {

                        }
                        else
                            recipient.GetComponent<Unit>().power = recipient.GetComponent<Unit>().power + 10;
                    }
                    battleStatus = false;
                    return;
                }
            }
        }
        else
        {
            recUnit.DealDamage(iniDmg);
            if (initiator.GetComponent<Unit>().lider == true)
            {
                if (initiator.GetComponent<Unit>().power >= initiator.GetComponent<Unit>().maxPower)
                { }                
                else
                    initiator.GetComponent<Unit>().power = initiator.GetComponent<Unit>().power + 10;
            }
            if (CheckIfDead(recipient))
            {
                if (recipient.GetComponent<Unit>().lider == true)
                {
                    gm.GetComponent<GameManager>().GameOver();
                    Destroy(recipient);
                }
                else
                {
                    Destroy(recipient);
                    if (initiator.GetComponent<Unit>().lider == true)
                    {
                        if (initiator.GetComponent<Unit>().power >= initiator.GetComponent<Unit>().maxPower)
                        {

                        }
                        else
                            initiator.GetComponent<Unit>().power = initiator.GetComponent<Unit>().power + 10;
                    }
                    if (initiator.name == "mAssessin" || initiator.name == "mAssessin(Clone)")
                    {
                        iniUnit.kill = true;
                    }
                    battleStatus = false;
                    return;
                }
            }
        }
        battleStatus = false;
    }

    public bool CheckIfDead(GameObject unitToCheck)
    {
        if (unitToCheck.GetComponent<Unit>().currHp <= 0)
        {
            return true;
        }
        return false;
    }

    public IEnumerator attack(GameObject unit, GameObject enemy)
    {
        battleStatus = true;
        float elapsedTime = 0;
        Vector3 startingPos = unit.transform.position;
        Vector3 endingPos = enemy.transform.position;
        while (elapsedTime < .25f)
        {

            unit.transform.position = Vector3.Lerp(startingPos, startingPos + ((((endingPos - startingPos) / (endingPos - startingPos).magnitude)).normalized * .5f), (elapsedTime / .25f));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }



        while (battleStatus)
        {
            //StartCoroutine(CSS.camShake(.2f, unit.GetComponent<UnitScript>().attackDamage, getDirection(unit, enemy)));
            if (unit.GetComponent<Unit>().atkRange == enemy.GetComponent<Unit>().atkRange && enemy.GetComponent<Unit>().currHp - unit.GetComponent<Unit>().maxDmg > 0)
            {
                Debug.Log(enemy.GetComponent<Unit>().maxDmg);
                Debug.Log(unit.GetComponent<Unit>().maxDmg);
            }

            else
            {
                Debug.Log(unit.GetComponent<Unit>().maxDmg);
            }

            Battle(unit, enemy);
            yield return new WaitForEndOfFrame();
        }      

        if (unit != null)
        {
            StartCoroutine(returnAfterAttack(unit, startingPos));
        }
    }

    public IEnumerator returnAfterAttack(GameObject unit, Vector3 endPoint)
    {
        float elapsedTime = 0;

        while (elapsedTime < .30f)
        {
            unit.transform.position = Vector3.Lerp(unit.transform.position, endPoint, (elapsedTime / .25f));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (unit.name == "mAssessin" || unit.name == "mAssessin(Clone)")
        {
            Debug.Log("Kill");
            if (unit.GetComponent<Unit>().kill == true)
            {
                unit.GetComponent<Unit>().moveAgain();
                unit.GetComponent<Unit>().kill = false;
                map.deselectUnit();
            }
        }
        else
        unit.GetComponent<Unit>().wait();
    }

    //Explosion (for all)
    public void Explosion4(GameObject initiator, GameObject unit1, GameObject unit2, GameObject unit3, GameObject unit4)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recUnit1 = unit1.GetComponent<Unit>();
        var recUnit2 = unit2.GetComponent<Unit>();
        var recUnit3 = unit3.GetComponent<Unit>();
        var recUnit4 = unit4.GetComponent<Unit>();
        int iniDmg1 = Random.Range(iniUnit.dmg1 - 9, iniUnit.dmg1);
        int iniDmg2 = Random.Range(iniUnit.dmg2 - 9, iniUnit.dmg2);
        int iniDmg3 = Random.Range(iniUnit.dmg3 - 9, iniUnit.dmg3);
        int iniDmg4 = Random.Range(iniUnit.dmg4 - 9, iniUnit.dmg4);

        recUnit1.DealDamage(iniDmg1);
        recUnit2.DealDamage(iniDmg2);
        recUnit3.DealDamage(iniDmg3);
        recUnit4.DealDamage(iniDmg4);

        if (CheckIfDead(unit1))
        {
            if (unit1.GetComponent<Unit>().lider == true)
            {
                 gm.GetComponent<GameManager>().GameOver();
                 Destroy(unit1);
            }
            else
            {
                Destroy(unit1);
                
                battleStatus = false;                
            }
        }

        if (CheckIfDead(unit2))
        {
            if (unit2.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit2);
                 
            }
            else
            {
                Destroy(unit2);
                 
                battleStatus = false;                
            }
        }

        if (CheckIfDead(unit3))
        {
            if (unit3.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit3);
                 
            }
            else
            {
                Destroy(unit3);
                 
                battleStatus = false;                
            }
        }

        if (CheckIfDead(unit4))
        {
            if (unit4.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit4);
                 
            }
            else
            {
                Destroy(unit4);
                 
                battleStatus = false;
                return;
            }
        }
        Destroy(initiator);
         
        battleStatus = false;
    }

    public void Explosion3(GameObject initiator, GameObject unit1, GameObject unit2, GameObject unit3)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recUnit1 = unit1.GetComponent<Unit>();
        var recUnit2 = unit2.GetComponent<Unit>();
        var recUnit3 = unit3.GetComponent<Unit>();
        int iniDmg1 = iniUnit.dmg1;
        int iniDmg2 = iniUnit.dmg2;
        int iniDmg3 = iniUnit.dmg3;

        recUnit1.DealDamage(iniDmg1);
        recUnit2.DealDamage(iniDmg2);
        recUnit3.DealDamage(iniDmg3);

        if (CheckIfDead(unit1))
        {
            if (unit1.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit1);
                 
            }
            else
            {
                Destroy(unit1);
                 
                battleStatus = false;     
            }
        }

        if (CheckIfDead(unit2))
        {
            if (unit2.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit2);
                 
            }
            else
            {
                Destroy(unit2);
                 
                battleStatus = false;                
            }
        }

        if (CheckIfDead(unit3))
        {
            if (unit3.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit3);
                 
            }
            else
            {
                Destroy(unit3);
                 
                battleStatus = false;
                return;
            }
        }

        Destroy(initiator);
         
        battleStatus = false;
    }

    public void Explosion2(GameObject initiator, GameObject unit1, GameObject unit2)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recUnit1 = unit1.GetComponent<Unit>();
        var recUnit2 = unit2.GetComponent<Unit>();
        int iniDmg1 = iniUnit.dmg1;
        int iniDmg2 = iniUnit.dmg2;

        recUnit1.DealDamage(iniDmg1);
        recUnit2.DealDamage(iniDmg2);

        if (CheckIfDead(unit1))
        {
            if (unit1.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit1);
                 
            }
            else
            {
                Destroy(unit1);
                 
                battleStatus = false;
            }
        }

        if (CheckIfDead(unit2))
        {
            if (unit2.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit2);
                 
            }
            else
            {
                Destroy(unit2);
                 
                battleStatus = false;
                return;
            }
        }

        Destroy(initiator);
         
        battleStatus = false;
    }

    public void Explosion1(GameObject initiator, GameObject unit1)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recUnit1 = unit1.GetComponent<Unit>();
        int iniDmg1 = iniUnit.dmg1;

        recUnit1.DealDamage(iniDmg1);

        if (CheckIfDead(unit1))
        {
            if (unit1.GetComponent<Unit>().lider == true)
            {
                gm.GetComponent<GameManager>().GameOver();
                Destroy(unit1);
                 
            }
            else
            {
                Destroy(unit1);
                 
                battleStatus = false;
                return;
            }
        }     

        Destroy(initiator);
         
        battleStatus = false;
    }
    
    //Contra Structuras
    public void BattleStructure(GameObject initiator, GameObject recipient)
    {
        battleStatus = true;
        var iniUnit = initiator.GetComponent<Unit>();
        var recStructure = recipient.GetComponent<Structure>();
        int iniDmg = Random.Range(iniUnit.maxDmg - 9, iniUnit.maxDmg);    
        int recDmg = recStructure.dmg;
        int iniRest;

        if (iniUnit.currHp != 100)
        {
            iniRest = 100 - iniUnit.currHp;
            iniRest = 100 - iniRest;
            iniDmg = iniRest * iniDmg / 100;
        }

        if (iniDmg <= 0)
            iniDmg = 1;

        if (recStructure.CanAttack(iniUnit.gameObject) == true)
        {
            recStructure.DealDamage(iniDmg);
            if (CheckIfDeadStructure(recipient))
            {
                if (recStructure.GetComponent<Structure>().primaryStructure == true)
                    gm.GetComponent<GameManager>().GameOver();
                else
                {
                    if (recStructure.name == "Mine")
                    {
                        if (recStructure.GetComponent<Structure>().playerNum == 1)
                            gm.incomePlayer1 = gm.incomePlayer1 - 100;
                        else if (recStructure.GetComponent<Structure>().playerNum == 2)
                            gm.incomePlayer2 = gm.incomePlayer2 - 100;   
                        gm.MoneyUpdate();
                    }
                    recStructure.GetComponent<Structure>().playerNum = 0;
                    gm.GetComponent<GameManager>().UpdateColors();
                    recStructure.GetComponent<Structure>().currHp = 100;
                    battleStatus = false;                  
                    return;
                }
            }          

            iniUnit.DealDamage(recDmg);
            if (CheckIfDead(initiator))
            {
                if (initiator.GetComponent<Unit>().lider == true)
                    gm.GetComponent<GameManager>().GameOver();
                else
                {
                    Destroy(initiator);
                    battleStatus = false;
                    return;
                }
            }
        }
        else
        {
            recStructure.DealDamage(iniDmg);

            if (CheckIfDeadStructure(recipient))
            {
                if (recStructure.GetComponent<Structure>().primaryStructure == true)
                {
                    gm.GetComponent<GameManager>().GameOver();
                }
                else
                {
                    if (recStructure.name == "Mine")
                    {
                        if (recStructure.GetComponent<Structure>().playerNum == 1)
                            gm.incomePlayer1 = gm.incomePlayer1 - 100;
                        else if (recStructure.GetComponent<Structure>().playerNum == 2)
                            gm.incomePlayer2 = gm.incomePlayer2 - 100;
                        gm.MoneyUpdate();
                    }
                    recStructure.GetComponent<Structure>().playerNum = 0;
                    gm.GetComponent<GameManager>().UpdateColors();
                    recStructure.GetComponent<Structure>().currHp = 100;
                    battleStatus = false;                    
                    return;
                }               
            }
        }
        battleStatus = false;
        
    }

    public IEnumerator attackStructure(GameObject unit, GameObject enemy)
    {
        battleStatus = true;
        float elapsedTime = 0;
        Vector3 startingPos = unit.transform.position;
        Vector3 endingPos = enemy.transform.position;
        while (elapsedTime < .25f)
        {

            unit.transform.position = Vector3.Lerp(startingPos, startingPos + ((((endingPos - startingPos) / (endingPos - startingPos).magnitude)).normalized * .5f), (elapsedTime / .25f));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }


        
        while (battleStatus)
        {
            
            if (unit.GetComponent<Unit>().atkRange == enemy.GetComponent<Structure>().atkRange && enemy.GetComponent<Structure>().currHp - unit.GetComponent<Unit>().maxDmg > 0)
            {
            }

            else
            {
            }

            BattleStructure(unit, enemy);
            yield return new WaitForEndOfFrame();
        }

        if (unit != null)
        {
            StartCoroutine(returnAfterAttack(unit, startingPos));
        }
    }

    public bool CheckIfDeadStructure(GameObject unitToCheck)
    {
        if (unitToCheck.GetComponent<Structure>().currHp <= 0)
        {
            return true;
        }
        return false;
    }
}
