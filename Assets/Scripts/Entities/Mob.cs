using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MovingEntity
{
    public float backtrackChance = .2f;
    public int sightDistance = 3;
    private GridTile previousTile = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoMovement(Game game)
    {
        GridTile NextTile = DoBehaviour(game);
        if(NextTile != null)
        {
            previousTile = currentTile;
            SetPos(NextTile, false, game);
        }

    }

    protected virtual GridTile DoBehaviour(Game game)
    {
        if(CanSeePlayer(game))
        {
            return DoChase(game);
        }
        
        return DoWander(game);
        
    }

    protected GridTile DoWander(Game game)
    {
        List<GridTile> neighbors = game.GetGrid().GetBlankNeighbors(currentTile);
        if(neighbors.Count < 1)
        {
            Debug.Log("IM TRAPPED SAVE ME OMG IM DYING!!!");
            SetExhausted();
            return null;
        }
        if(previousTile != null && neighbors.Count > 1)
        {
            if(Random.value > backtrackChance)
            {
                neighbors.Remove(previousTile);
            }
        }
        int index = Random.Range(0, neighbors.Count);
        GridTile tile = neighbors[index];
        return tile;
    }

    protected GridTile DoChase(Game game)
    {
        Point currentPoint = GridTile.GridTileToPoint(currentTile);
        Point PlayerPoint = GridTile.GridTileToPoint(game.GetPlayer().GetTile());

        if(currentPoint.Equals(PlayerPoint))
        {
            // Done
            SetExhausted();
            return null;
        }

        List<Point> list = game.GetGrid().GetPathFinder().FindPath(currentPoint, PlayerPoint);
        if (list == null || list.Count <= 0)
        {
            Debug.Log("the world has collapsed. no one will survive. everything is null and void.");
            SetExhausted();
            return null;
        }
        Point nextPoint = list[0];

        GridTile NextTile = GridTile.PointToGridTile(nextPoint, game.GetGrid());
        if (NextTile.IsWall())
        {
            Debug.Log("OMG IT HAPPENED AGAIN I SMACKED MY FACE INTO A WALL AND IT HURT!!!");
        }
        return NextTile;
    }

    protected bool CanSeePlayer(Game game)
    {
        return GridTile.GetManhattanDistance(game.GetPlayer().GetTile(), currentTile) <= sightDistance;
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
