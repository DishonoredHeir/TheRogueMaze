using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Game))]
public class MobManager : MonoBehaviour
{
    public GameObject slimePrefab;

    private List<Mob> AllMobs = new List<Mob>();
    private Game game;
    

    private void Start()
    {
        game = GetComponent<Game>();
    }

    private void Update()
    {

    }

    public void SpawnMobs(List<GridTile> SpawnPositions)
    {
        int index = Random.Range(0, SpawnPositions.Count);
        GridTile startingMobTile = SpawnPositions[index];
        Spawn(slimePrefab, startingMobTile);
    }

    private void Spawn(GameObject prefab, GridTile Location)
    {
        GameObject MobObj = Instantiate(prefab);
        Mob mob = MobObj.GetComponent<Mob>();
        mob.SetMobPos(Location, true, game);
    }
}
