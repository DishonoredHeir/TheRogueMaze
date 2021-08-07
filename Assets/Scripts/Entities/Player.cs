using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity
{
    protected override void DoMovement(Game game)
    {
        HandlePlayerInput(game);
    }

    public void HandlePlayerInput(Game game)
    {
        bool moved = false;
         if(Input.GetKey(KeyCode.W))
        {
            moved = MoveInDirection(Direction.Up, game);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moved = MoveInDirection(Direction.Left, game);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            moved = MoveInDirection(Direction.Down, game);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moved = MoveInDirection(Direction.Right, game);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            SetExhausted();
        }
    }
}
