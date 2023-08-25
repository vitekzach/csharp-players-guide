namespace Level31TheFountainOfObjects.Managers;

public class FountainRoom: GridItem
{
    public bool FountainActive { get; private set; }

    public void TurnFountainOn()
    {
        if (!FountainActive) FountainActive = true;
    }
    
    public void TurnFountainOff()
    {
        if (FountainActive) FountainActive = false;
    }
}