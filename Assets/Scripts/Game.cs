using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int width;
    public int height;
    public TilemapGenerator tilemapGenerator;
    public Player player;

    private GameGrid grid;

    private Vector2 TileToScenePos(GridTile tile)
    {
        float posX = tile.GetX() - (width / 2) + 0.5f;
        float posY = tile.GetY() - (height / 2) + 0.5f;

        return new Vector2(posX, posY);
    }

    private void Awake()
    {
        grid = new GameGrid(width, height);
    }

    private void Start()
    {
        MazeGenerator.GenerateMaze(grid);
        tilemapGenerator.CreateTilemapFromGrid(grid);

        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                GridTile tile = grid.GetTile(x, y);
                if(!tile.IsWall())
                {
                    SetPlayerPos(tile);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(0, 1);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(-1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(0, -1);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(1, 0);
        }
    }

    private void MovePlayer(int xOffset, int yOffset)
    {
        GridTile currentTile = player.GetTile();
        GridTile nextTile = grid.GetTile(
            currentTile.GetX() + xOffset,
            currentTile.GetY() + yOffset
        );
        SetPlayerPos(nextTile);
    }

    private void SetPlayerPos(GridTile tile)
    {
        if(tile != null && !tile.IsWall())
        {
            player.SetTile(tile);
            Vector2 playerPos = TileToScenePos(tile);
            player.transform.position = playerPos;
        }
    }
}
