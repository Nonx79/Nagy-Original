using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    public int playerNum = 0;
    public bool waiting = false;

    public int hp = 100;
    public int currHp = 100;
    public int dmg = 30;
    public int atkRange = 1;
    public int faction; // 0 military, 1 povery, 2 religion

    public Text hpUI;

    public bool primaryStructure = false;

    [Header("Military Units")]
    public GameObject mSoldier, mSniper, mWagon, mInvokeMachine, mMinitank, mShield, mAssessin,
                      mSoldierSprite, mSniperSprite, mCarSprite, mInvokeMachineSprite, mMinitankSprite, mShieldSprite, mAssessinSprite;
    public Text costMSoldier, costMSniper, costMWagon, costMInvokeMachine, costMMinitank, costMShield, costMAssessin,
                mUnitName, mUnitDescript, mUnitMove, mUnitRange,
                mMoneyText;


    [Header("Povery Units")]
    public GameObject pSoldier, pAnimator, pKamikaze, pWagon, pTruck, pRange, pMolotov,
                      pSoldierSprite, pAnimatorSprite, pKamikazeSprite, pCarSprite, pTruckSprite, pRangeSprite, pMolotovSprite;

    public Text costPSoldier, costPAnimator, costPKamikaze, costPWagon, costPTruck, costPRange, costPMolotov,
                pUnitName, pUnitDescript, pUnitMove, pUnitRange,
                pMoneyText;

    public GameObject unitToInvoke;
    public TileMap map;
    public GameManager gm;

    public GameObject theColor;
    public GameObject minimap;

    public int usage = 2;

    private void Awake()
    {
        map = FindObjectOfType<TileMap>();
        gm = FindObjectOfType<GameManager>();
    }
    public void Income()
    {
        if (gameObject.name == "Mine")
        {
            if (gm.currPlayer == 1 && gameObject.GetComponent<Structure>().playerNum == gm.GetComponent<GameManager>().currPlayer)
            {
                gm.GetComponent<GameManager>().moneyPlayer1 = gm.GetComponent<GameManager>().moneyPlayer1 + 100;
                gm.GetComponent<GameManager>().MoneyUpdate();
            }
            else if (gm.GetComponent<GameManager>().currPlayer == 2 && gameObject.GetComponent<Structure>().playerNum == gm.GetComponent<GameManager>().currPlayer)
            {
                gm.GetComponent<GameManager>().moneyPlayer2 = gm.GetComponent<GameManager>().moneyPlayer2 + 100;
                gm.GetComponent<GameManager>().MoneyUpdate();
            }
        }
    }

    public void wait()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        waiting = true;
    }

    //Bottons
    //Military
    public void MSoldierBotton()
    {
        unitToInvoke = mSoldier;
        mUnitName.text = "Soldier";
        mUnitDescript.text = "Un soldado común y corriente.";
        mUnitMove.text = "Move: " + (mSoldier.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mSoldier.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mSoldierSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mSoldierSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mSoldierSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MSniperBotton()
    {
        unitToInvoke = mSniper;
        mUnitName.text = "Sniper";
        mUnitDescript.text = "Unidad con gran alcance y daño.";
        mUnitMove.text = "Move: " + (mSniper.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mSniper.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mSniperSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mSniperSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mSniperSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MWagon()
    {
        unitToInvoke = mWagon;
        mUnitName.text = "Car";
        mUnitDescript.text = "Auto capaz de llevar gente (Solo si la unidad esta al lado).";
        mUnitMove.text = "Move: " + (mWagon.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mWagon.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mCarSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mCarSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mCarSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MInvokeMachine()
    {
        unitToInvoke = mInvokeMachine;
        mUnitName.text = "Invoke Machine";
        mUnitDescript.text = "Auto capaz de invokar unidades.";
        mUnitMove.text = "Move: " + (mInvokeMachine.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mInvokeMachine.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mInvokeMachineSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mInvokeMachineSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mInvokeMachineSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MMinitank()
    {
        unitToInvoke = mMinitank;
        mUnitName.text = "Minitanks";
        mUnitDescript.text = "Auto que ataca arrollando undades y quedando detras de ellas.";
        mUnitMove.text = "Move: " + (mMinitank.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mMinitank.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mMinitankSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mMinitankSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mMinitankSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MShield()
    {
        unitToInvoke = mShield;
        mUnitName.text = "Shield";
        mUnitDescript.text = "Unidad capaz de mover unidades intercambiando sus posiciones";
        mUnitMove.text = "Move: " + (mShield.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mShield.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mShieldSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mShieldSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mShieldSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void MAssessin()
    {
        unitToInvoke = mAssessin;
        mUnitName.text = "Assessin";
        mUnitDescript.text = "Unidad que obtiene un turno extra si mata a otra unidad";
        mUnitMove.text = "Move: " + (mShield.GetComponent<Unit>().moveSpeed).ToString();
        mUnitRange.text = "Range: " + (mShield.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        mAssessinSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            mAssessinSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            mAssessinSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    //Povery
    public void PSoldierBotton()
    {
        unitToInvoke = pSoldier;
        pUnitName.text = "Soldier";
        pUnitDescript.text = "Un soldado común y corriente.";
        pUnitMove.text = "Move: " + (pSoldier.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pSoldier.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pSoldierSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pSoldierSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pSoldierSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PAnimatorBotton()
    {
        unitToInvoke = pAnimator;
        pUnitName.text = "Animator";
        pUnitDescript.text = "Un soldado que al estar al lado de un aliado le aumenta el daño, pero no puede atacar.";
        pUnitMove.text = "Move: " + (pAnimator.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pAnimator.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pAnimatorSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pAnimatorSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pAnimatorSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PKamikazeBotton()
    {
        unitToInvoke = pKamikaze;
        pUnitName.text = "Kamikaze";
        pUnitDescript.text = "Un soldado que al atacar explota.";
        pUnitMove.text = "Move: " + (pKamikaze.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pKamikaze.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pKamikazeSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pKamikazeSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pKamikazeSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PWagon()
    {
        unitToInvoke = pWagon;
        pUnitName.text = "Car";
        pUnitDescript.text = "Auto capaz de llevar gente (Solo si la unidad esta al lado).";
        pUnitMove.text = "Move: " + (pWagon.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pWagon.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pCarSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pCarSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pCarSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PTruck()
    {
        unitToInvoke = pTruck;
        pUnitName.text = "Truck";
        pUnitDescript.text = "Auto capaz de empujar al enemigo al atacar.";
        pUnitMove.text = "Move: " + (pTruck.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pTruck.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pTruckSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pTruckSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pTruckSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PRange()
    {
        unitToInvoke = pRange;
        pUnitName.text = "Range";
        pUnitDescript.text = "Soldado con rango.";
        pUnitMove.text = "Move: " + (pRange.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pRange.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pRangeSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pRangeSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pRangeSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    public void PMolotov()
    {
        unitToInvoke = pMolotov;
        pUnitName.text = "Molotov";
        pUnitDescript.text = "Soldado con una molotov, al atacar deja el piso con fuego.";
        pUnitMove.text = "Move: " + (pMolotov.GetComponent<Unit>().moveSpeed).ToString();
        pUnitRange.text = "Range: " + (pMolotov.GetComponent<Unit>().maxAtkRange).ToString();
        SetDisableSprites();
        pMolotovSprite.SetActive(true);
        if (gm.GetComponent<GameManager>().currPlayer == 1)
            pMolotovSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor1;
        else if (gm.GetComponent<GameManager>().currPlayer == 2)
            pMolotovSprite.transform.GetChild(0).GetComponent<Image>().color = gm.GetComponent<GameManager>().selectColor2;
    }

    //Religion


    public void RecruitBotton()
    {
        switch (faction)
        {
            case 0:
                if (gm.GetComponent<GameManager>().moneyPlayer1 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 1)
                {
                    transform.GetChild(0).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else if (gm.GetComponent<GameManager>().moneyPlayer2 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 2)
                {
                    transform.GetChild(0).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else
                    Debug.Log("Dinero insuficiente");
                break;
            case 1:
                if (gm.GetComponent<GameManager>().moneyPlayer1 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 1)
                {
                    transform.GetChild(1).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else if (gm.GetComponent<GameManager>().moneyPlayer2 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 2)
                {
                    transform.GetChild(1).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else
                    Debug.Log("Dinero insuficiente");
                break;
            case 2:
                if (gm.GetComponent<GameManager>().moneyPlayer1 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 1)
                {
                    transform.GetChild(2).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else if (gm.GetComponent<GameManager>().moneyPlayer2 >= unitToInvoke.GetComponent<Unit>().cost && gm.GetComponent<GameManager>().currPlayer == 2)
                {
                    transform.GetChild(2).GetComponent<Canvas>().enabled = false;
                    ActivateCollider();
                    map.finalizeMovementPositionStructure();
                }
                else
                    Debug.Log("Dinero insuficiente");
                break;
        }
    }

    public void CancelBotton()
    {
        switch (faction)
        {
            case 0:
                transform.GetChild(0).GetComponent<Canvas>().enabled = false;
                map.selectedStructure = null;
                ActivateCollider();
                break;
            case 1:
                transform.GetChild(1).GetComponent<Canvas>().enabled = false;
                map.selectedStructure = null;
                ActivateCollider();
                break;
            case 2:
                transform.GetChild(2).GetComponent<Canvas>().enabled = false;
                map.selectedStructure = null;
                ActivateCollider();
                break;
        }
    }

    public void ActivateCollider()
    {
        GameObject[] tiles;
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void DealDamage(int x)
    {
        currHp = currHp - x;
        UpdateHpUI();
    }

    public bool CanAttack(GameObject unitToAtack)
    {
        if (unitToAtack.transform.position.x - this.gameObject.transform.position.x <= atkRange
            && unitToAtack.transform.position.x - this.gameObject.transform.position.x >= -atkRange
            && unitToAtack.transform.position.y - this.gameObject.transform.position.y == 0
            || unitToAtack.transform.position.y - this.gameObject.transform.position.y <= atkRange
            && unitToAtack.transform.position.y - this.gameObject.transform.position.y >= -atkRange
            && unitToAtack.transform.position.x - this.gameObject.transform.position.x == 0)
        {
            Debug.Log("usado");
            return true;
        }
        else
            return false;
    }

    public void UpdateHpUI()
    {
        if (currHp != hp)
        {
            hpUI.text = (currHp / 10).ToString();
        }
    }

    public void UpdateMoney()
    {
        switch (faction)
        {
            case 0:
                switch (gm.GetComponent<GameManager>().currPlayer)
                {
                    case 1:                      
                        costMSoldier.text = (mSoldier.GetComponent<Unit>().cost).ToString();
                        costMWagon.text = (mWagon.GetComponent<Unit>().cost).ToString();
                        costMAssessin.text = (mAssessin.GetComponent<Unit>().cost).ToString();
                        costMMinitank.text = (mMinitank.GetComponent<Unit>().cost).ToString();
                        costMSniper.text = (mSniper.GetComponent<Unit>().cost).ToString();
                        costMShield.text = (mShield.GetComponent<Unit>().cost).ToString();
                        costMInvokeMachine.text = (mInvokeMachine.GetComponent<Unit>().cost).ToString();                           
                        mMoneyText.text = (gm.GetComponent<GameManager>().moneyPlayer1).ToString();

                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mSoldier.GetComponent<Unit>().cost)
                            costMSoldier.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mWagon.GetComponent<Unit>().cost)
                            costMWagon.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mAssessin.GetComponent<Unit>().cost)
                            costMAssessin.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mMinitank.GetComponent<Unit>().cost)
                            costMMinitank.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mSniper.GetComponent<Unit>().cost)
                            costMSniper.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mShield.GetComponent<Unit>().cost)
                            costMShield.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < mInvokeMachine.GetComponent<Unit>().cost)
                            costMInvokeMachine.color = Color.red;                                                                     
                        break;
                    case 2:
                        costMSoldier.text = (mSoldier.GetComponent<Unit>().cost).ToString();
                        costMWagon.text = (mWagon.GetComponent<Unit>().cost).ToString();
                        costMAssessin.text = (mAssessin.GetComponent<Unit>().cost).ToString();
                        costMMinitank.text = (mMinitank.GetComponent<Unit>().cost).ToString();
                        costMSniper.text = (mSniper.GetComponent<Unit>().cost).ToString();
                        costMShield.text = (mShield.GetComponent<Unit>().cost).ToString();
                        costMInvokeMachine.text = (mInvokeMachine.GetComponent<Unit>().cost).ToString();
                        mMoneyText.text = (gm.GetComponent<GameManager>().moneyPlayer1).ToString();

                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mSoldier.GetComponent<Unit>().cost)
                            costMSoldier.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mWagon.GetComponent<Unit>().cost)
                            costMWagon.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mAssessin.GetComponent<Unit>().cost)
                            costMAssessin.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mMinitank.GetComponent<Unit>().cost)
                            costMMinitank.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mSniper.GetComponent<Unit>().cost)
                            costMSniper.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mShield.GetComponent<Unit>().cost)
                            costMShield.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < mInvokeMachine.GetComponent<Unit>().cost)
                            costMInvokeMachine.color = Color.red;
                        break;
                }
                break;
            case 1:
                switch (gm.GetComponent<GameManager>().currPlayer)
                {
                    case 1:
                        costPSoldier.text = (pSoldier.GetComponent<Unit>().cost).ToString();
                        costPKamikaze.text = (pKamikaze.GetComponent<Unit>().cost).ToString();
                        costPRange.text = (pRange.GetComponent<Unit>().cost).ToString();
                        costPMolotov.text = (pMolotov.GetComponent<Unit>().cost).ToString();
                        costPWagon.text = (pWagon.GetComponent<Unit>().cost).ToString();
                        costPAnimator.text = (pAnimator.GetComponent<Unit>().cost).ToString();
                        costPTruck.text = (pTruck.GetComponent<Unit>().cost).ToString();                                              
                        pMoneyText.text = (gm.GetComponent<GameManager>().moneyPlayer1).ToString();

                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pSoldier.GetComponent<Unit>().cost)
                            costPSoldier.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pKamikaze.GetComponent<Unit>().cost)
                            costPKamikaze.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pRange.GetComponent<Unit>().cost)
                            costPRange.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pMolotov.GetComponent<Unit>().cost)
                            costPRange.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pWagon.GetComponent<Unit>().cost)
                            costPWagon.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pAnimator.GetComponent<Unit>().cost)
                            costPAnimator.color = Color.red;                                                                       
                        if (gm.GetComponent<GameManager>().moneyPlayer1 < pTruck.GetComponent<Unit>().cost)
                            costPTruck.color = Color.red;
                        break;
                    case 2:
                        costPSoldier.text = (pSoldier.GetComponent<Unit>().cost).ToString();
                        costPKamikaze.text = (pKamikaze.GetComponent<Unit>().cost).ToString();
                        costPRange.text = (pRange.GetComponent<Unit>().cost).ToString();
                        costPMolotov.text = (pMolotov.GetComponent<Unit>().cost).ToString();
                        costPWagon.text = (pWagon.GetComponent<Unit>().cost).ToString();
                        costPAnimator.text = (pAnimator.GetComponent<Unit>().cost).ToString();
                        costPTruck.text = (pTruck.GetComponent<Unit>().cost).ToString();
                        pMoneyText.text = (gm.GetComponent<GameManager>().moneyPlayer1).ToString();

                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pSoldier.GetComponent<Unit>().cost)
                            costPSoldier.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pKamikaze.GetComponent<Unit>().cost)
                            costPKamikaze.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pRange.GetComponent<Unit>().cost)
                            costPRange.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pMolotov.GetComponent<Unit>().cost)
                            costPRange.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pWagon.GetComponent<Unit>().cost)
                            costPWagon.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pAnimator.GetComponent<Unit>().cost)
                            costPAnimator.color = Color.red;
                        if (gm.GetComponent<GameManager>().moneyPlayer2 < pTruck.GetComponent<Unit>().cost)
                            costPTruck.color = Color.red;                     
                        break;
                }
                break;
        }
    }

    void SetDisableSprites()
    {
        switch (faction)
        {
            case 0:
                mSoldierSprite.SetActive(false);
                mSniperSprite.SetActive(false);
                mCarSprite.SetActive(false);
                mInvokeMachineSprite.SetActive(false);
                mMinitankSprite.SetActive(false);
                mShieldSprite.SetActive(false);
                mAssessinSprite.SetActive(false);
                break;
            case 1:
                pSoldierSprite.SetActive(false);
                pAnimatorSprite.SetActive(false);
                pCarSprite.SetActive(false);
                pKamikazeSprite.SetActive(false);
                pRangeSprite.SetActive(false);
                pTruckSprite.SetActive(false);
                pMolotovSprite.SetActive(false);
                break;
        }
    }
}

