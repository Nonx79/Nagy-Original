using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SinglePlayerBoard : TileMap
{
    // Start is called before the first frame update
    public override void SelectUnitMove(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        mouseClickToSelectUnitV2(intCoords);
    }

    public override void SetSelectedUnit(Vector2 coords)
    {
        Vector2Int intCoords = new Vector2Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y));
        OnSetSelectedUnit(intCoords);
    }
}
