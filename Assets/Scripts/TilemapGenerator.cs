using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public enum Type
    {
        Checkerboard,
        Random
    }

    public Type type = Type.Checkerboard;
    public Tile blankTile;
    public Tile filledTile;
    public int seed = 0;

    public bool isSquare;

    [Range(1, 30)]
    public int width = 10;

    [Range(1, 30)]
    public int height = 10;

    private Tilemap tilemap = null;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
    }

    private void Start()
    {
        //CreateTilemap();
    }

    public void CreateTilemapFromGrid(GameGrid grid)
    {
        int gridWidth = grid.GetWidth();
        int gridHeight = grid.GetHeight();

        int centerX = gridWidth / 2;
        int centerY = gridHeight / 2;

        for (int x = 0; x < gridWidth; ++x)
        {
            for (int y = 0; y < gridHeight; ++y)
            {
                Vector3Int p = new Vector3Int(x - centerX, y - centerY, 0);
                bool isEven = (x + y) % 2 == 0;
                GridTile gridTile = grid.GetTile(x, y);
                Tile tile = gridTile.IsWall() ? filledTile : blankTile;
                tilemap.SetTile(p, tile);
            }
        }
    }

    private void CreateTilemap()
    {
        tilemap.ClearAllTiles();
        Random.InitState(seed);

        if(type == Type.Checkerboard)
        {
            GenerateCheckerboard();
        } else if(type == Type.Random)
        {
            GenerateRandom();
        }
    }

    private void GenerateCheckerboard()
    {
        int centerX = width / 2;
        int centerY = height / 2;
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                Vector3Int p = new Vector3Int(x - centerX, y - centerY, 0);
                bool isEven = (x + y) % 2 == 0;
                Tile tile = isEven ? blankTile : filledTile;
                tilemap.SetTile(p, tile);
            }
        }
    }

    private void GenerateRandom()
    {
        int centerX = width / 2;
        int centerY = height / 2;
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                Vector3Int p = new Vector3Int(x - centerX, y - centerY, 0);
                bool isBlank = Random.value < 0.5;
                Tile tile = isBlank ? blankTile : filledTile;
                tilemap.SetTile(p, tile);
            }
        }
    }

    private void OnValidate()
    {
        if(isSquare)
        {
            height = width;
        }
    }

    private void OnDrawGizmos()
    {
        if(tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }

        //CreateTilemap();
    }
}
