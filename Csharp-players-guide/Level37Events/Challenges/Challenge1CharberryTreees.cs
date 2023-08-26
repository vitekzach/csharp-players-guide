namespace Level37Events.Challenges;

public class CharberryTree
{
    private Random _random = new Random();
    public bool Ripe { get; set; }

    public event Action? Ripened; 

    public void MaybeGrow()
    {
        // Only a tiny chance of ripening each time, but we try a lot!
        if (_random.NextDouble() < 0.00000001 && !Ripe)
        {
            Ripe = true;
            Ripened?.Invoke();
        }
    }
}

public class Notifier
{
    public Notifier(CharberryTree tree)
    {
        tree.Ripened += HandleRipened;
    }
    
    public void HandleRipened() => Console.WriteLine($"{DateTime.Now.ToString()} Notifier: The tree has ripened!");
}

public class Harverster
{
    private CharberryTree Tree { get;}
    
    public Harverster(CharberryTree tree)
    {
        Tree = tree;
        tree.Ripened += HandleRipened;
    }
    public void HandleRipened()
    {
        Console.WriteLine($"{DateTime.Now.ToString()} Harvester: The tree has been harvested!");
        Tree.Ripe = false;
    }
}