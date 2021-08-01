using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour
{
    public float movingSpeed = 5;
    public float movingSmoothTime = 0.1f;

    protected GridTile currentTile;

    private Vector2 TargetPos;
    private Vector2 currentVelocity;

    protected virtual void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, TargetPos, ref currentVelocity, movingSmoothTime, movingSpeed, Time.deltaTime);
    }
    public virtual void SetTile(GridTile tile)
    {
        currentTile = tile;
    }

    // Sets the entity position if the tile is valid, does nothing otherwise.
    public virtual bool SetPos(GridTile tile, bool instant, Game game)
    {
        if (tile != null && !tile.IsWall())
        {
            SetTile(tile);
            Vector2 targetPos = game.TileToScenePos(tile);
            if (instant)
            {
                transform.position = targetPos;
            }
            SetTargetPos(targetPos);
            return true;
        }
        return false;
    }

    // Move the entity by the given offset.
    protected virtual bool MoveByOffset(int xOffset, int yOffset, Game game)
    {
        GridTile nextTile = game.GetGrid().GetTile(
            currentTile.GetX() + xOffset,
            currentTile.GetY() + yOffset
        );
        return SetPos(nextTile, false, game);
    }

    protected void SetTargetPos(Vector2 vector)
    {
        TargetPos = vector;
    }

    public GridTile GetTile()
    {
        return currentTile;
    }
}
