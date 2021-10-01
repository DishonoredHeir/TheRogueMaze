using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Game))]
public class TrapManager : MonoBehaviour
{
    [Range(1,100)]
    public int NumTraps;
    public GameObject PitfallPrefab;

    private List<Trap> AllTraps = new List<Trap>();
    private Game game;
    

    private void Awake()
    {
        game = GetComponent<Game>();
    }

    private void Update()
    {

    }

    public void SpawnTraps(SpawnPointGenerator spawnPointGenerator)
    {
        for(int i = 0; i < NumTraps; i++)
        {
            if(spawnPointGenerator.GetNumRemaining() <= 0)
            {
                break;
            }
            GridTile TrapTile = spawnPointGenerator.GenerateSpawnPoint();
            Spawn(PitfallPrefab, TrapTile);
        }
    }

    private void Spawn(GameObject prefab, GridTile Location)
    {
        Vector2 pos = game.TileToScenePos(Location);
        
        GameObject TrapObj = Instantiate(prefab);
        Trap trap = TrapObj.GetComponent<Trap>();
        AllTraps.Add(trap);
        trap.SetTile(Location);
        trap.SetPos(pos);
    }


}