using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile {

    private bool isWall = true;
    private int x;
    private int y;
    private List<Mob> Mobs = new List<Mob>();

    public GridTile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetWall(bool flag)
    {
        isWall = flag;
    }

    public void AddMob(Mob mob)
    {
        Mobs.Add(mob);
    }

    public void RemoveMob(Mob mob)
    {
        if(!Mobs.Remove(mob))
        {
            Debug.Log("mob was not deleted successfully");
        }
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
