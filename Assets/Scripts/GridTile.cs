using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {

    private bool isWall = true;
    private int x;
    private int y;

    public GridTile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetWall(bool flag)
    {
        isWall = flag;
    }

    public bool IsWall()
    {
        return isWall;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }
}
