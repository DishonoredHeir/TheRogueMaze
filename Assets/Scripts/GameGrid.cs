using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid {
    // Adds an object to a list if it is not null.
    private static void AddIfNotNull<T>(List<T> list, T obj)
    {
        if(obj != null)
        {
            list.Add(obj);
        }
    }

    private GridTile[,] grid;
    private int width;
    private int height;

    public GameGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        this.grid = new GridTile[width, height];
        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                this.grid[x, y] = new GridTile(x, y);
            }
        }
    }

    // Returns the valid neighbors of a tile. Only cardinal neighbors are considered.
    public List<GridTile> GetNeighbors(GridTile tile)
    {
        List<GridTile> neighbors = new List<GridTile>();
        int x = tile.GetX();
        int y = tile.GetY();

        AddIfNotNull(neighbors, GetTile(x, y + 1));
        AddIfNotNull(neighbors, GetTile(x, y - 1));
        AddIfNotNull(neighbors, GetTile(x + 1, y));
        AddIfNotNull(neighbors, GetTile(x - 1, y));

        return neighbors;
    }

    public GridTile GetTile(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return grid[x, y];
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
