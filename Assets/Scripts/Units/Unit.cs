using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Unit : MonoBehaviour
{
	public int tileX;
	public int tileY;

	public string unitName;
	public int maxHp = 100;
	public int currHp = 100;
	public int weaponID; // 0 mSoldier - 1 mWagon - 2 mAssessin - 3 mMini tank - 4 mSniper - 6 mShield - 7 Invoke machine - 8 pSoldier - 9 pKamikaze - 10 pRango
						 // 11 pMolotov - 12 pWagon - 13 pAnimator - 14 pTruck - 15/16 mSniperCommander - 17 pMechanicComander
    public int[] weaponDamage;
	public int maxDmg;
	public int dmgStructure;
	public int minAtkRange = 1;
	public int atkRange = 1;
	public int maxAtkRange;
	public int moveSpeed = 4;
	public int maxSpeed = 4;
	public int faction; // 0 military, 1 povery, 2 religion
	public Sprite unitSprite;

	//Ia
	public bool attack = false;
	public bool unitToBattle = false;

	// Kamikaze damage
	public int dmg1;
	public int dmg2;
	public int dmg3;
	public int dmg4;

	//Asessin
	public bool kill = false;

	public bool wagon = false;
	public GameObject unitIn;
	public bool inWagon = false;

	public bool lider = false;

	public int power = 0;
	public int maxPower;
	public bool powerBool = false;

	public GameObject robotPrefab;

	public int cost = 100;

	public int playerNum;

	public bool waiting = false;

	public Text hpUI;

	public TileMap map;

	public Queue<int> movementQueue;
	public Queue<int> combatQueue;

	public float visualMovementSpeed = .3f;

	public int unitPreviousX;
	public int unitPreviousY;

	public GameObject tileBeingOccupied;

	// Our pathfinding info.  Null if we have no destination ordered.
	public List<Node> currentPath = null;
	public bool completedMovement = false;

	public GameObject theColor;
	public GameObject minimap;

	//ia
	public bool ia = false;

	//Position
	public bool position = false;

	public enum movementStates
	{
		Unselected,
		Selected,
		Moved,
		Wait
	}
	public movementStates unitMoveState;

	private void Awake()
    {
		if(position == false)
		{
            movementQueue = new Queue<int>();
            combatQueue = new Queue<int>();

            tileX = (int)transform.position.x;
            tileY = (int)transform.position.y;
            unitPreviousX = (int)transform.position.x;
            unitPreviousY = (int)transform.position.y;
            map = FindObjectOfType<TileMap>();
            tileBeingOccupied = map.tilesOnMap[tileX, tileY];
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach (GameObject t in tiles)
            {
                if (t.GetComponent<ClickableTile>().tileX == tileX && t.GetComponent<ClickableTile>().tileY == tileY)
                    t.GetComponent<ClickableTile>().unitOnTile = this.gameObject;
            }
            unitMoveState = movementStates.Unselected;
        }
	} 

	public void MoveNextTile()
	{
		if (currentPath.Count == 0 || moveSpeed == 0)
		{
			return;
		}
		else
		{
			StartCoroutine(moveOverSeconds(transform.gameObject, currentPath[currentPath.Count - 1]));
		}
	}

	public void moveAgain()
	{
		moveSpeed = maxSpeed;
		waiting = false;
		currentPath = null;
		setMovementState(0);
		completedMovement = false;
		gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;	
	}

	public movementStates getMovementStateEnum(int i)
	{
		if (i == 0)
		{
			return movementStates.Unselected;
		}
		else if (i == 1)
		{
			return movementStates.Selected;
		}
		else if (i == 2)
		{
			return movementStates.Moved;
		}
		else if (i == 3)
		{
			return movementStates.Wait;
		}
		return movementStates.Unselected;

	}

	public void setMovementState(int i)
	{
		if (i == 0)
		{
			unitMoveState = movementStates.Unselected;
		}
		else if (i == 1)
		{
			unitMoveState = movementStates.Selected;
		}
		else if (i == 2)
		{
			unitMoveState = movementStates.Moved;
		}
		else if (i == 3)
		{
			unitMoveState = movementStates.Wait;
		}
	}

	public void wait()
	{
		gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
		waiting = true;
	}

	public IEnumerator checkIfRoutinesRunning()
	{
		while (combatQueue.Count > 0)
		{

			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);

	}

	public IEnumerator moveOverSeconds(GameObject objectToMove, Node endNode)
	{
		movementQueue.Enqueue(1);

		//remove first thing on path because, its the tile we are standing on

		currentPath.RemoveAt(0);
		while (currentPath.Count != 0)
		{		
			Vector3 endPos = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
			objectToMove.transform.position = Vector3.Lerp(transform.position, endPos, visualMovementSpeed);
			if ((transform.position - endPos).sqrMagnitude < 0.001)
			{		
				currentPath.RemoveAt(0);
			}
			yield return new WaitForEndOfFrame();
		}
		visualMovementSpeed = .3f;
		transform.position = map.TileCoordToWorldCoord(endNode.x, endNode.y);

		map.tilesOnMap[unitPreviousX, unitPreviousY].GetComponent<ClickableTile>().unitOnTile = null;
		map.tilesOnMap[tileX, tileY].GetComponent<ClickableTile>().unitOnTile = this.gameObject;

		tileX = endNode.x;
		tileY = endNode.y;
		tileBeingOccupied.GetComponent<ClickableTile>().unitOnTile = null;
		tileBeingOccupied = map.tilesOnMap[tileX, tileY];
		movementQueue.Dequeue();		
	}

	public void DealDamage(int x)
	{
		currHp = currHp - x;
		UpdateHpUI();
	}

	public void UnitDie()
    {
		Destroy(this.gameObject);
    }

	public bool CanAttack(GameObject unitToAtack)
	{
		if ((unitToAtack.transform.position.x - this.gameObject.transform.position.x <= atkRange
            && unitToAtack.transform.position.x - this.gameObject.transform.position.x <= 0
            && unitToAtack.transform.position.y - this.gameObject.transform.position.y <= 0)
            || (this.gameObject.transform.position.x - unitToAtack.transform.position.x <= atkRange
            && this.gameObject.transform.position.x - unitToAtack.transform.position.x <= 0
            && unitToAtack.transform.position.y - this.gameObject.transform.position.y <= 0)
			|| (unitToAtack.transform.position.y - this.gameObject.transform.position.y <= atkRange
			&& unitToAtack.transform.position.y - this.gameObject.transform.position.y <= 0
			&& unitToAtack.transform.position.x - this.gameObject.transform.position.x <= 0)
			|| (this.gameObject.transform.position.y - unitToAtack.transform.position.y <= atkRange
            && this.gameObject.transform.position.y - unitToAtack.transform.position.y <= 0
            && unitToAtack.transform.position.x - this.gameObject.transform.position.x <= 0))
		{
			Debug.Log("Rango rival encontrado");
            Debug.Log(atkRange);
            Debug.Log(this.gameObject);
            Debug.Log(unitToAtack.transform.position.x - this.gameObject.transform.position.x);
            Debug.Log(unitToAtack.transform.position.y - this.gameObject.transform.position.y);
            return true;
		}
		else
		{
			Debug.Log("Rango rival no encontrado");
            Debug.Log(atkRange);
            Debug.Log(unitToAtack.transform.position.x - this.gameObject.transform.position.x);			
            Debug.Log(unitToAtack.transform.position.y - this.gameObject.transform.position.y);
            return false;
        }			
	}

	public void UpdateHpUI()
	{
		if (currHp != 100)
		{
			hpUI.text = (currHp / 10).ToString();
		}
    }

	public void PowerBotton()
    {
		if (faction == 0)
		this.gameObject.GetComponent<Powers>().Concentration();
		else if (faction == 1)
			this.gameObject.GetComponent<Powers>().Concentration();
		else if (faction == 2)
			this.gameObject.GetComponent<Powers>().Concentration();
	}
}
