using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int width;
    public int height;
    public TilemapGenerator tilemapGenerator;

    private GameGrid grid;

    private void Awake()
    {
        grid = new GameGrid(width, height);
    }

    // Start is called before the first frame update
    private void Start()
    {
        MazeGenerator.GenerateMaze(grid);
        tilemapGenerator.CreateTilemapFromGrid(grid);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
