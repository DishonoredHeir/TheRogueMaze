using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed = 5;
    public float smoothTime = 0.1f;

    private GridTile MobTile;
    private Vector2 TargetPos;
    private Vector2 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, TargetPos, ref currentVelocity, smoothTime, speed, Time.deltaTime);
    }

    public void SetTile(GridTile tile)
    {
        if(MobTile != null)
        {
            MobTile.RemoveMob(this);
        }
        MobTile = tile;
        MobTile.AddMob(this);
    }

    public GridTile GetTile()
    {
        return MobTile;
    }

    public void SetTargetPos(Vector2 vector)
    {
        TargetPos = vector;
    }

    public bool SetMobPos(GridTile tile, bool instant, Game game)
    {
        if(tile != null && !tile.IsWall())
        {
            SetTile(tile);
            Vector2 MobPos = game.TileToScenePos(tile);
            if(instant)
            {
                transform.position = MobPos;
            }
            SetTargetPos(MobPos);
            return true;
        }
        return false;
    }
}
