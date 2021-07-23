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

    public GridTile GetTile()
    {
        return playerTile;
    }

    public void SetTargetPos(Vector2 vector)
    {
        TargetPos = vector;
    }


}
