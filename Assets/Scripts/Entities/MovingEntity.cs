using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour
{
    public float movingSpeed = 5;
    public float movingSmoothTime = 0.1f;
    public int movesPerTurn = 1;
    public float moveTime = 0.5f;

    protected GridTile currentTile;

    private Vector2 TargetPos;
    private Vector2 currentVelocity;
    private int movesRemaining;
    private bool isMoving;

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, TargetPos, ref currentVelocity, movingSmoothTime, movingSpeed, Time.deltaTime);
    }

    public bool DoUpdate(Game game)
    {
        if(!isMoving && movesRemaining > 0)
        {
            DoMovement(game);
        }

        return IsExhausted();
    }

    protected abstract void DoMovement(Game game);

    public void OnTurnStart()
    {
        movesRemaining = movesPerTurn;
    }

    public virtual void SetTile(GridTile tile)
    {
        currentTile = tile;
    }

    public void SetExhausted()
    {
        movesRemaining = 0;
        isMoving = false;
    }

    // Sets the entity position if the tile is valid, does nothing otherwise.
    public virtual bool SetPos(GridTile tile, bool instant, Game game)
    {
        // Tile is valid
        if (tile != null && !tile.IsWall())
        {
            SetTile(tile);
            Vector2 targetPos = game.TileToScenePos(tile);
            SetTargetPos(targetPos);
            if (instant)
            {
                transform.position = targetPos;
            }
            else
            {
                // Already moving
                if(isMoving || movesRemaining <= 0)
                {
                    return false;
                }
                isMoving = true;
                movesRemaining--;
                Invoke("FinishMoving", moveTime);
            }
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

    private void FinishMoving()
    {
        isMoving = false;
    }

    public bool IsExhausted()
    {
        return !isMoving && movesRemaining <= 0;
    }

    public GridTile GetTile()
    {
        return currentTile;
    }
}
