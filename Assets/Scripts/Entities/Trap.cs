using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool destroyOnTrigger = true;

    private GridTile currentTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Trigger(Game game)
    {
        OnTrigger(game);

        if(destroyOnTrigger)
        {
            Destroy(gameObject);
            currentTile.SetTrap(null);
        }
    }

    protected virtual void OnTrigger(Game game)
    {
        Debug.Log("ha... YOU'VE ACTIVATED MY TRAP CARD!!!");
    }

    public void SetTile(GridTile gridTile)
    {
        currentTile = gridTile;
        gridTile.SetTrap(this);
    }

    public void SetPos(Vector2 vec)
    {
        transform.position = vec;
    }

    public GridTile GetTile()
    {
        return currentTile;
    }
}
