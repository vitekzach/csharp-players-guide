namespace Level31TheFountainOfObjects.Managers;

public class Player: GridItem
{
    public int ArrowCount { get;private set; }

    public Player()
    {
        ArrowCount = 5;
    }

    public bool ShootAnArrow()
    {
        if (ArrowCount > 0)
        {
            ArrowCount -= 1;
            return true;
        }
        return false;
    }
}