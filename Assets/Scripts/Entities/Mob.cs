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
