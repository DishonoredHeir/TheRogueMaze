using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float smoothTime = 0.1f;

    private GridTile playerTile;
    private Vector2 TargetPos;
    private Vector2 currentVelocity;

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, TargetPos, ref currentVelocity, smoothTime, speed, Time.deltaTime);
    }

    public void SetTile(GridTile tile)
    {
         playerTile = tile;
    }
    
    public void HandlePlayerInput(Game game)
    {
        bool moved = false;
         if(Input.GetKeyDown(KeyCode.W))
        {
            moved = MovePlayer(0, 1, game);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            moved = MovePlayer(-1, 0, game);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            moved = MovePlayer(0, -1, game);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            moved = MovePlayer(1, 0, game);
        }
        if(moved)
        {
            game.ScheduleNextTurn();
        }
    }

    // Move the player by the given offset.
    private bool MovePlayer(int xOffset, int yOffset, Game game)
    {
        GridTile nextTile = game.GetGrid().GetTile(
            playerTile.GetX() + xOffset,
            playerTile.GetY() + yOffset
        );
        return SetPlayerPos(nextTile, false, game);
    }

    // Sets the player position if the tile is valid, does nothing otherwise.
    public bool SetPlayerPos(GridTile tile, bool instant, Game game)
    {
        if(tile != null && !tile.IsWall())
        {
            SetTile(tile);
            Vector2 playerPos = game.TileToScenePos(tile);
            if(instant)
            {
                transform.position = playerPos;
            }
            SetTargetPos(playerPos);
            return true;
        }
        return false;
    }

    public GridTile GetTile()
    {
        return playerTile;
    }

    public void SetTargetPos(Vector2 vector)
    {
        TargetPos = vector;
    }


}
