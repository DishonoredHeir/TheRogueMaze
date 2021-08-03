using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Game))]
public class MobManager : MonoBehaviour
{
    public GameObject slimePrefab;

    private List<Mob> AllMobs = new List<Mob>();
    private Game game;
    

    private void Awake()
    {
        game = GetComponent<Game>();
    }

    private void Update()
    {

    }

    public void SpawnMobs(SpawnPointGenerator spawnPointGenerator)
    {
        for(int i = 0; i < 10; i++)
        {
            if(spawnPointGenerator.GetNumRemaining() <= 0)
            {
                break;
            }
            GridTile startingMobTile = spawnPointGenerator.GenerateSpawnPoint();
            Spawn(slimePrefab, startingMobTile);
        }
    }

    private void Spawn(GameObject prefab, GridTile Location)
    {
        GameObject MobObj = Instantiate(prefab);
        Mob mob = MobObj.GetComponent<Mob>();
        AllMobs.Add(mob);
        mob.SetPos(Location, true, game);
    }

    public bool UpdateMobs()
    {
        bool allExhausted = true;
        foreach(Mob mob in AllMobs)
        {
            if(!mob.DoUpdate(game))
            {
                allExhausted = false;
            }
        }
        return allExhausted;
    }

    public void OnTurnStart()
    {
        foreach(Mob mob in AllMobs)
        {
            mob.OnTurnStart();
        }
    }
}
