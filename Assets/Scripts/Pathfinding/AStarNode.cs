// Represents a grid tile for A* calculations
public class AStarNode : IMinHeapItem<AStarNode>
{
    public AStarNode Parent = null;
    public int GCost;
    public int HCost;
    private int heapIndex;

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }

    // Needed for MinHeap implementation
    public int MinHeapIndex
    {
        get
        {
            return heapIndex;        
        }
        set
        {
            heapIndex = value;
        }
    }

    private Point pos;
    private bool walkable;

    public AStarNode(Point pos, bool walkable)
    {
        this.pos = pos;
        this.walkable = walkable;
    }

    public Point GetPos()
    {
        return pos;
    }

    public bool IsWalkable()
    {
        return walkable;
    }

    // Needed for MinHeap Implementation
    public int CompareTo(AStarNode o)
    {
        int result = o.FCost - FCost;
        if(result == 0)
        {
            return o.HCost - HCost;
        }
        return result;
    }
}