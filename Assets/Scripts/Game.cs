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

    // Converts a GridTile to its equivalent scene position.
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

        // Spawn the player at a random dead end
        List<GridTile> deadEnds = grid.GetAllDeadEnds();
        int index = Random.Range(0, deadEnds.Count);
        GridTile startingPlayerTile = deadEnds[index];
        SetPlayerPos(startingPlayerTile);
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

    // Move the player by the given offset.
    private void MovePlayer(int xOffset, int yOffset)
    {
        GridTile currentTile = player.GetTile();
        GridTile nextTile = grid.GetTile(
            currentTile.GetX() + xOffset,
            currentTile.GetY() + yOffset
        );
        SetPlayerPos(nextTile);
    }

    // Sets the player position if the tile is valid, does nothing otherwise.
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
