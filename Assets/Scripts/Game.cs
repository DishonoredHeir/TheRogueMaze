using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const int playerTurn = 0;
    private const int enemyTurn = 1;
    private const int maxTurnIndex = enemyTurn;
    
    public int width;
    public int height;
    public TilemapGenerator tilemapGenerator;
    public Player player;
    public float delay = 0.5f;
    
    private GameGrid grid;
    private int currentTurn = playerTurn;
    private bool hasMoved = false;

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
        SetPlayerPos(startingPlayerTile, true);
    }

    private void Update()
    {
        if(hasMoved)
        {
            return;
        }
        if(currentTurn == playerTurn)
        {
            HandlePlayerInput();
        }
        else if(currentTurn == enemyTurn)
        {
            ScheduleNextTurn();
        }
    }

    private void HandlePlayerInput()
    {
        bool moved = false;
         if(Input.GetKeyDown(KeyCode.W))
        {
            moved = MovePlayer(0, 1);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            moved = MovePlayer(-1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            moved = MovePlayer(0, -1);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            moved = MovePlayer(1, 0);
        }
        if(moved)
        {
            ScheduleNextTurn();
        }
    }

    private void ScheduleNextTurn()
    {   
        if(!hasMoved)
        {
            hasMoved = true;
            Invoke("NextTurn", delay);
        }
    }

    private void NextTurn()
    {
        currentTurn = (currentTurn+1)%maxTurnIndex;
        hasMoved = false;
    }

    // Move the player by the given offset.
    private bool MovePlayer(int xOffset, int yOffset)
    {
        GridTile currentTile = player.GetTile();
        GridTile nextTile = grid.GetTile(
            currentTile.GetX() + xOffset,
            currentTile.GetY() + yOffset
        );
        return SetPlayerPos(nextTile, false);
    }

    // Sets the player position if the tile is valid, does nothing otherwise.
    private bool SetPlayerPos(GridTile tile, bool instant)
    {
        if(tile != null && !tile.IsWall())
        {
            player.SetTile(tile);
            Vector2 playerPos = TileToScenePos(tile);
            if(instant)
            {
                player.transform.position = playerPos;
            }
            player.SetTargetPos(playerPos);
            return true;
        }
        return false;
    }
}
