using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator {
    // Assumes the grid is initially all filled tiles.
    // https://stackoverflow.com/questions/23843197/maze-generating-algorithm-in-grid
    public static void GenerateMaze(GameGrid grid)
    {
        int width = grid.GetWidth();
        int height = grid.GetHeight();
        List<GridTile> neighborPool = new List<GridTile>();
        HashSet<GridTile> visited = new HashSet<GridTile>();

        // Pick a cell and mark it as part of the maze.
        int startX = Random.Range(0, width);
        int startY = Random.Range(0, height);
        GridTile startingTile = grid.GetTile(startX, startY);
        visited.Add(startingTile);
        startingTile.SetWall(false);

        // Add the surrounding filled cells of the cell to the cell list.
        List<GridTile> startingNeighbors = grid.GetNeighbors(startingTile);
        foreach(GridTile neighbor in startingNeighbors)
        {
            neighborPool.Add(neighbor);
        }

        // While there are cells in the list:
        while (neighborPool.Count > 0)
        {
            // Pick a random cell in the list
            int index = Random.Range(0, neighborPool.Count);
            GridTile tile = neighborPool[index];
            visited.Add(tile);

            // If the cell has one or less explored neighbors:
            int numExploredNeighbors = GetNumExploredNeighbors(grid, tile, visited);
            if (numExploredNeighbors <= 1)
            {
                // Clear the cell.
                tile.SetWall(false);

                // Add the neighboring filled cells to the list.
                List<GridTile> neighbors = grid.GetNeighbors(tile);
                foreach(GridTile neighbor in neighbors)
                {
                    if (neighbor.IsWall())
                    {
                        neighborPool.Add(neighbor);
                    }
                }
            }
            // Remove cell from list.
            neighborPool.Remove(tile);
        }
    }

    private static int GetNumExploredNeighbors(GameGrid grid, GridTile tile, HashSet<GridTile> explored)
    {
        int centerX = tile.GetX();
        int centerY = tile.GetY();

        int numExploredNeighbors = 0;
        List<GridTile> neighbors = grid.GetNeighbors(tile);
        foreach(GridTile neighbor in neighbors) {
            if(explored.Contains(neighbor))
            {
                ++numExploredNeighbors;
            }
        }
        return numExploredNeighbors;
    }
}
