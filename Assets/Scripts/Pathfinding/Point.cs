// Represents a 2D integer position, primarily for grid tiles
public class Point
{
    private int x;
    private int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public override bool Equals(object obj)
    {
        Point o = obj as Point;

        if(o == null)
        {
            return false;
        }

        return x == o.x && y == o.y;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() * 31 + y.GetHashCode();
    }
}
