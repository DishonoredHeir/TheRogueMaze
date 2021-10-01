using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallTrap : Trap
{
    protected override void OnTrigger(Game game)
    {
        Debug.Log("ha... YOU'VE ACTIVATED MY TRAP CARD!!!");
        game.GetPlayer().SetExhausted();
    }
}
