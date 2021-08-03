using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid {
    private static Point Up = new Point(0, 1);
    private static Point Down = new Point(0, -1);
    private static Point Left = new Point(-1, 0);
    private static Point Right = new Point(1, 0);

    public static Point GetOffsetFromDirection(Direction direction)
    {
        if (direction == Direction.Up)
        {
            return Up;
        }
        else if (direction == Direction.Down)
        {
            return Down;
        }
        else if (direction == Direction.Left)
        {
            return Left;
        }
        else if (direction == Direction.Right)
        {
            return Right;
        }
        return null;
    }

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
    private AStar pathFinder;

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

    public void RefreshPathFinder()
    {
        this.pathFinder = new AStar(this);
    }

    // Returns a list of all blank tiles.
    public List<GridTile> GetAllBlankTiles()
    {
        List<GridTile> results = new List<GridTile>();
        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                GridTile tile = GetTile(x, y);
                if(!tile.IsWall())
                {
                    results.Add(tile);
                }
            }
        }
        return results;
    }

    // Returns a list of all blank tiles with 1 or less blank neighbors.
    public List<GridTile> GetAllDeadEnds(List<GridTile> blankTiles = null)
    {
        if(blankTiles == null)
        {
            blankTiles = GetAllBlankTiles();
        }

        List<GridTile> deadEndTiles = new List<GridTile>();
        foreach(GridTile blankTile in blankTiles)
        {
            if(GetNumBlankNeighbors(blankTile) <= 1)
            {
                deadEndTiles.Add(blankTile);
            }
        }
        return deadEndTiles;
    }

    // Returns the number of blank neighbors of a tile.
    public int GetNumBlankNeighbors(GridTile tile)
    {
        List<GridTile> neighbors = GetNeighbors(tile);
        int numBlankNeighbors = 0;

        foreach (GridTile neighbor in neighbors)
        {
            if (!neighbor.IsWall())
            {
                ++numBlankNeighbors;
            }
        }
        return numBlankNeighbors;
    }

    // Returns a list of the blank neighbors of a tile.
    public List<GridTile> GetBlankNeighbors(GridTile tile)
    {
        List<GridTile> neighbors = GetNeighbors(tile);
        List<GridTile> blankNeighbors = new List<GridTile>();

        foreach(GridTile neighbor in neighbors)
        {
            if(!neighbor.IsWall())
            {
                blankNeighbors.Add(neighbor);
            }
        }
        return blankNeighbors;
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

    public GridTile GetTileInDirection(GridTile tile, Direction direction)
    {
        Point offset = GetOffsetFromDirection(direction);
        return GetTile(tile.GetX() + offset.GetX(), tile.GetY() + offset.GetY());
    }

    public bool IsTileInDirectionBlank(GridTile tile, Direction direction)
    {
        GridTile neighbor = GetTileInDirection(tile, direction);
        return neighbor != null && !neighbor.IsWall();
    }

    // Returns the tile at the given point, or null if it is out of bounds.
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

    public AStar GetPathFinder()
    {
        return pathFinder;
    }
}
