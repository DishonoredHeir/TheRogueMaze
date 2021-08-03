using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private int sizeX;
    private int sizeY;
    private AStarNode[,] nodeMap;

    public AStar(GameGrid grid)
    {
        sizeX = grid.GetWidth();
        sizeY = grid.GetHeight();
        nodeMap = new AStarNode[sizeX, sizeY];

        CreateNodeMap(grid);
    }

    private void CreateNodeMap(GameGrid grid)
    {
        for (int i = 0; i < sizeX; ++i)
        {
            for (int j = 0; j < sizeY; ++j)
            {
                GridTile tile = grid.GetTile(i, j);
                nodeMap[i, j] = new AStarNode(new Point(tile.GetX(), tile.GetY()), !tile.IsWall());
            }
        }
    }

    // Returns a list of points for the shortest path between start and end
    public List<Point> FindPath(Point start, Point end)
    {
        AStarNode startNode = nodeMap[start.GetX(), start.GetY()];
        AStarNode endNode = nodeMap[end.GetX(), end.GetY()];

        MinHeap<AStarNode> openSet = new MinHeap<AStarNode>(sizeX * sizeY);
        HashSet<AStarNode> closedSet = new HashSet<AStarNode>();

        startNode.GCost = 0;
        startNode.HCost = GetCost(startNode, endNode);

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            AStarNode currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if(currentNode == endNode)
            {
                break;
            }

            List<AStarNode> neighbors = GetNeighbors(currentNode);
            foreach(AStarNode neighbor in neighbors)
            {
                if(!neighbor.IsWalkable() || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newCostToNeighbor = currentNode.GCost + GetCost(currentNode, neighbor);
                if(newCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newCostToNeighbor;
                    neighbor.HCost = GetCost(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return RetracePath(startNode, endNode);
    }

    // Returns null or a path from start to end, excluding end
    private List<Point> RetracePath(AStarNode startNode, AStarNode endNode)
    {
        List<Point> path = new List<Point>();
        AStarNode currentNode = endNode;

        while(currentNode != startNode)
        {
            if(currentNode == null)
            {
                return null;
            }

            path.Add(currentNode.GetPos());
            currentNode = currentNode.Parent;
        }

        path.Reverse();

        return path;
    }

    private List<AStarNode> GetNeighbors(AStarNode node)
    {
        List<AStarNode> neighbors = new List<AStarNode>();
        int x = node.GetPos().GetX();
        int y = node.GetPos().GetY();

        int[] xList = { 1, -1, 0, 0 };
        int[] yList = { 0, 0, 1, -1 };

        for(int i = 0; i < xList.Length; ++i)
        {
            int checkX = x + xList[i];
            int checkY = y + yList[i];

            if(IsValid(checkX, checkY))
            {
                AStarNode neighbor = nodeMap[checkX, checkY];
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
    }

    private int GetCost(AStarNode a, AStarNode b)
    {
        return GetCost(a.GetPos(), b.GetPos());
    }

    private int GetCost(Point a, Point b)
    {
        int deltaX = Mathf.Abs(a.GetX() - b.GetX());
        int deltaY = Mathf.Abs(a.GetY() - b.GetY());

        return deltaX + deltaY;
    }
}
