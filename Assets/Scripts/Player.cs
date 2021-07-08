using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GridTile playerTile;

    public void SetTile(GridTile tile)
    {
         playerTile = tile;
    }

    public GridTile GetTile()
    {
        return playerTile;
    }
}
