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
    protected Game game;

    private Vector2 TargetPos;
    private Vector2 currentVelocity;
    private int movesRemaining = 0;
    private bool isMoving = false;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, TargetPos, ref currentVelocity, movingSmoothTime, movingSpeed, Time.deltaTime);
    }

    public bool DoUpdate()
    {
        if(!isMoving && movesRemaining > 0)
        {
            DoMovement();
        }

        return IsExhausted();
    }

    protected abstract void DoMovement();

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
    public virtual bool SetPos(GridTile tile, bool instant)
    {
        /*
        if(currentTile != null && GridTile.GetManhattanDistance(currentTile, tile) > 1 && !instant)
        {
            Debug.Log("Warning: Moving more than one tile in a single move!");
        }*/

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
                StartMoving();
            }
            return true;
        }
        return false;
    }

    // Move the entity by the given offset.
    protected virtual bool MoveInDirection(Direction direction)
    {
        GridTile nextTile = game.GetGrid().GetTileInDirection(currentTile, direction);
        return SetPos(nextTile, false);
    }

    protected void SetTargetPos(Vector2 vector)
    {
        TargetPos = vector;
    }

    private void StartMoving()
    {
        isMoving = true;
        movesRemaining--;
        Invoke("FinishMoving", moveTime);
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
