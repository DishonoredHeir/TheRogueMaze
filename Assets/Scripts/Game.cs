using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobManager))]
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
    private MobManager mobManager;

    // Converts a GridTile to its equivalent scene position.
    public Vector2 TileToScenePos(GridTile tile)
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
        mobManager = GetComponent<MobManager>();

        // Spawn the player at a random dead end
        SpawnPointGenerator spawnPointGenerator = new SpawnPointGenerator(grid.GetAllDeadEnds());
        player.SetPos(spawnPointGenerator.GenerateSpawnPoint(), true, this);
        mobManager.SpawnMobs(spawnPointGenerator);
    }

    private void Update()
    {
        if(hasMoved)
        {
            return;
        }
        if(currentTurn == playerTurn)
        {
            player.HandlePlayerInput(this);
        }
        else if(currentTurn == enemyTurn)
        {
            ScheduleNextTurn();
        }
    }

    
    public void ScheduleNextTurn()
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

    public GameGrid GetGrid()
    {
        return grid;
    }
}
