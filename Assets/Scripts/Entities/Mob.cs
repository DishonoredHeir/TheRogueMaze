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
        /*
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
        //*/

        Point currentPoint = GridTile.GridTileToPoint(currentTile);
        Point PlayerPoint = GridTile.GridTileToPoint(game.GetPlayer().GetTile());

        if(currentPoint.Equals(PlayerPoint))
        {
            // Done
            SetExhausted();
            return;
        }

        List<Point> list = game.GetGrid().GetPathFinder().FindPath(currentPoint, PlayerPoint);
        if (list == null || list.Count <= 0)
        {
            Debug.Log("the world has collapsed. no one will survive. everything is null and void.");
            SetExhausted();
            return;
        }
        Point nextPoint = list[0];

        GridTile NextTile = GridTile.PointToGridTile(nextPoint, game.GetGrid());
        if (NextTile.IsWall())
        {
            Debug.Log("OMG IT HAPPENED AGAIN I SMACKED MY FACE INTO A WALL AND IT HURT!!!");
        }
        SetPos(NextTile, false, game);
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
