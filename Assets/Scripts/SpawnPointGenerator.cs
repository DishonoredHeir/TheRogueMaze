using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGenerator
{
    private List<GridTile> availableSpawnPoints;
    
    public SpawnPointGenerator(List<GridTile> availableSpawnPoints)
    {
        this.availableSpawnPoints = availableSpawnPoints;
    }

    public GridTile GenerateSpawnPoint()
    {
        if(availableSpawnPoints.Count <= 0)
        {
            Debug.Log("Error: No more spawn points!");
            return null;
        }
        int index = Random.Range(0, availableSpawnPoints.Count);
        GridTile tile = availableSpawnPoints[index];
        availableSpawnPoints.RemoveAt(index);
        return tile;
    }
}