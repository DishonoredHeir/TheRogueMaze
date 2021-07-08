using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid {
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
