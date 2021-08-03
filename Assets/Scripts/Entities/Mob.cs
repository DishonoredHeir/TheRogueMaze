using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MovingEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoMovement(Game game)
    {
        Debug.Log("Mobs moved and or got tired.");
        List<GridTile> neighbors = game.GetGrid().GetBlankNeighbors(currentTile);
        if(neighbors.Count < 1)
        {
            Debug.Log("IM TRAPPED SAVE ME OMG IM DYING!!!");
            SetExhausted();
            return;
        }
        int index = Random.Range(0, neighbors.Count);
        GridTile tile = neighbors[index];
        SetPos(tile, false, game);
    }

    public override void SetTile(GridTile tile)
    {
        if(currentTile != null)
        {
            currentTile.RemoveMob(this);
        }
        base.SetTile(tile);
        currentTile.AddMob(this);
    }
}
