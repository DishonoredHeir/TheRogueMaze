using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity
{
    protected override void DoMovement()
    {
        HandlePlayerInput();
    }

    public void HandlePlayerInput()
    {
        bool moved = false;
         if(Input.GetKey(KeyCode.W))
        {
            moved = MoveInDirection(Direction.Up);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moved = MoveInDirection(Direction.Left);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            moved = MoveInDirection(Direction.Down);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moved = MoveInDirection(Direction.Right);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            SetExhausted();
        }
    }
}
