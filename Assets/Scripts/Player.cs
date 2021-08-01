using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity
{
    protected override void Update()
    {
        base.Update();
    }

    public void HandlePlayerInput(Game game)
    {
        bool moved = false;
         if(Input.GetKey(KeyCode.W))
        {
            moved = MoveByOffset(0, 1, game);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moved = MoveByOffset(-1, 0, game);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            moved = MoveByOffset(0, -1, game);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moved = MoveByOffset(1, 0, game);
        }
        if(moved)
        {
            game.ScheduleNextTurn();
        }
    }
}
