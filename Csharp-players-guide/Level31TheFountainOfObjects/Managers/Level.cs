using System.Diagnostics;
using Level31TheFountainOfObjects.Enums;
using Level31TheFountainOfObjects.Models;
using Level31TheFountainOfObjects.Interfaces;
using Level31TheFountainOfObjects.Providers;
using Level31TheFountainOfObjects.Helpers;

namespace Level31TheFountainOfObjects.Managers;

public class Level
{
    private Coordinates EntranceCoordinate { get; set; }
    private Coordinates FountainCoordinate { get; set; }
    private Coordinates PlayerCoordinate { get; set; }
    private Player Player { get; set; }
    private LevelDimensions LevelDimensions { get; set; }
    private GridItem[,] LevelGrid { get; set; }
    private LevelState LevelState { get; set; }
    private IInputInterface _inputProvider;
    private IOutputInterface _outputProvider;
    private bool LevelEnded {get; set; }

    public Level(IInputInterface inputProvider, IOutputInterface outputProvider)
    {
        _inputProvider = inputProvider;
        _outputProvider = outputProvider;
        InitLevelSize();
        PrintInfo();
        RunLevel();
    }
    

    public Level(Coordinates entranceCoordinate, Coordinates fountainCoordinate, Coordinates playerCoordinate, 
        Coordinates[] pitsCoordinates, Coordinates[] maelstromsCoordinates, Coordinates[] amaroksCoordinates,
        LevelDimensions levelDimensions, IInputInterface inputProvider, IOutputInterface outputProvider)
    {
        _inputProvider = inputProvider;
        _outputProvider = outputProvider;
        SetValues(entranceCoordinate, fountainCoordinate, playerCoordinate, pitsCoordinates, maelstromsCoordinates, 
            amaroksCoordinates,
            levelDimensions);
    }

    private void SetValues(Coordinates entranceCoordinate, Coordinates fountainCoordinate, Coordinates playerCoordinate, 
        Coordinates[] pitsCoordinates, Coordinates[] maelstromsCoordinates, Coordinates[] amaroksCoordinates, 
        LevelDimensions levelDimensions)
    {
        EntranceCoordinate = entranceCoordinate;
        FountainCoordinate = fountainCoordinate;
        PlayerCoordinate = playerCoordinate;
        LevelDimensions = levelDimensions;
        Player = new Player();

        LevelGrid = new GridItem[LevelDimensions.Rows, LevelDimensions.Columns];
        
        for (int row = 0; row < LevelDimensions.Rows; row++)
        {
            for (int column = 0; column < LevelDimensions.Columns; column++)
            {
                LevelGrid[row, column] = new EmptyRoom();
            }
        }

        LevelGrid[EntranceCoordinate.Row, EntranceCoordinate.Column] = new EntranceRoom();
        LevelGrid[FountainCoordinate.Row, FountainCoordinate.Column] = new FountainRoom();

        foreach (Coordinates pitCoordinates in pitsCoordinates)
        {
            LevelGrid[pitCoordinates.Row, pitCoordinates.Column] = new PitRoom();
        }
        
        foreach (Coordinates maelstromCoordinates in maelstromsCoordinates)
        {
            LevelGrid[maelstromCoordinates.Row, maelstromCoordinates.Column] = new MaelstromRoom();
        }
        
        foreach (Coordinates amarokCoordinates in amaroksCoordinates)
        {
            LevelGrid[amarokCoordinates.Row, amarokCoordinates.Column] = new AmarokRoom();
        }

        LevelState = LevelState.Ready;
    }
    
    private void CreateSmallLevel()
    {
        SetValues(new Coordinates(0, 0), new Coordinates(0, 2),
            new Coordinates(0, 0), 
            new Coordinates[] {new Coordinates(0, 3)}, 
            new Coordinates[] {new Coordinates(1, 1)},
            new Coordinates[] {new Coordinates(0, 1)},
            new LevelDimensions(4, 4));
    }
    
    private void CreateMediumLevel()
    {
        SetValues(new Coordinates(0, 0), new Coordinates(0, 2),
            new Coordinates(0, 0), 
            new Coordinates[] {new Coordinates(0, 1), new Coordinates(0,3)},
            new Coordinates[] {new Coordinates(2, 1)},
            new Coordinates[]
            {
                new Coordinates(5, 5), 
                new Coordinates(4, 4)
            },
            new LevelDimensions(6, 6));
    }
    
    private void CreateLargeLevel()
    {
        SetValues(new Coordinates(0, 0), new Coordinates(0, 2),
            new Coordinates(0, 0), 
            new Coordinates[]
            {
                new Coordinates(0, 1), new Coordinates(0,3),
                new Coordinates(2, 0), new Coordinates(2,1)
            },
            new Coordinates[]
            {
                new Coordinates(2, 0),
                new Coordinates(2, 1),
                new Coordinates(2, 2),
            },
            new Coordinates[] {new Coordinates(2, 1), new Coordinates(5, 5)},
            new LevelDimensions(8, 8));
    }

    private void InitLevelSize()
    {
        while (LevelState != LevelState.Ready)
        {
            _outputProvider.Output("What world size would you like?", OutputType.Question);
            string action = _inputProvider.Input();
            LevelSize levelSize = LevelSize.Small;
        
            switch (action)
            {
                case "small":
                    CreateSmallLevel();
                    break;
                case "medium":
                    CreateMediumLevel();
                    break;
                case "large":
                    CreateLargeLevel();
                    break;
                default:
                    _outputProvider.Output("Please choose an available option: (small, medium, large).", OutputType.Warning);
                    break;
            }
        }
    }

    private void PrintMap()
    {
        for(int row = 0; row < LevelDimensions.Rows; row++)
        {
            for (int col = 0; col < LevelDimensions.Columns; col++)
            {
                if (LevelGrid[row, col].GetType() == typeof(EntranceRoom)) _outputProvider.Output("F", OutputType.EntranceLight, false);
                else if (LevelGrid[row, col].GetType() == typeof(PitRoom)) _outputProvider.Output("P", OutputType.Pit, false);
                else if (LevelGrid[row, col].GetType() == typeof(FountainRoom)) _outputProvider.Output("F", OutputType.Fountain, false);
                else if (LevelGrid[row, col].GetType() == typeof(EmptyRoom)) _outputProvider.Output("E", OutputType.Empty, false);
                else if (LevelGrid[row, col].GetType() == typeof(MaelstromRoom)) _outputProvider.Output("M", OutputType.Maelstrom, false);
                else if (LevelGrid[row, col].GetType() == typeof(AmarokRoom)) _outputProvider.Output("A", OutputType.Amarok, false);
            }
            _outputProvider.Output("", OutputType.Empty);
        }
    }

    private void CrateLevelBasedOnSize(LevelSize levelSize)
    {
        switch (levelSize)
        {
            case LevelSize.Small:
                CreateSmallLevel();
                break;
            case LevelSize.Medium:
                CreateMediumLevel();
                break;
            case LevelSize.Large:
                CreateLargeLevel();
                break;
        }
    }

    private bool CheckForWin()
    {
        if (PlayerCoordinate == EntranceCoordinate && GetFountainRoom().FountainActive) return true;
        return false;
    }

    private FountainRoom GetFountainRoom()
    {
        return (FountainRoom)LevelGrid[FountainCoordinate.Row, FountainCoordinate.Column];
    }

    private bool CoordinatesWithinBounds(Coordinates coordinates)
    {
        if (coordinates.Row < LevelDimensions.Rows && coordinates.Row >= 0 &&
            coordinates.Column < LevelDimensions.Columns && coordinates.Column >= 0) return true;
        return false;
    }


    private void DiedToPit()
    {
        _outputProvider.Output("You have fell into a pit.", OutputType.Warning);
        _outputProvider.Output("You lose.", OutputType.Warning);
        LevelState = LevelState.Lost;
        LevelEnded = true;
    }
    
    private void DiedToAmarok()
    {
        _outputProvider.Output("You have died to an Amarok.", OutputType.Warning);
        _outputProvider.Output("You lose.", OutputType.Warning);
        LevelState = LevelState.Lost;
        LevelEnded = true;
    }

    private void HitMaelstrom()
    {
        LevelGrid[PlayerCoordinate.Row, PlayerCoordinate.Column] = new EmptyRoom();
        
        Coordinates newMaelstromPosition = GetBoxedCoordinates(PlayerCoordinate with
        {
            Row = PlayerCoordinate.Row + 1, 
            Column = PlayerCoordinate.Column - 2
        });
        
        LevelGrid[newMaelstromPosition.Row, newMaelstromPosition.Column] = new MaelstromRoom();

        Coordinates newPlayerCoordinates = GetBoxedCoordinates(PlayerCoordinate with
        {
            Row = PlayerCoordinate.Row - 1,
            Column = PlayerCoordinate.Column + 2
        });
        
        _outputProvider.Output("You have hit a Maelstrom!", OutputType.Warning);
        
        MovePlayerTo(newPlayerCoordinates);
        
    }

    private Coordinates GetBoxedCoordinates(Coordinates coordinates)
    {
        return coordinates with
        {
            Row = Math.Min(Math.Max(0, coordinates.Row), LevelDimensions.Rows-1),
            Column = Math.Min(Math.Max(0, coordinates.Column), LevelDimensions.Columns-1)
        };
    }
    
    private void MovePlayerTo(Coordinates newCoordinates)
    {
        if (CoordinatesWithinBounds(newCoordinates))
        {
            PlayerCoordinate = newCoordinates;
            if (CheckForRoom(PlayerCoordinate, typeof(PitRoom))) DiedToPit();
            if (CheckForRoom(PlayerCoordinate, typeof(MaelstromRoom))) HitMaelstrom();
            if (CheckForRoom(PlayerCoordinate, typeof(AmarokRoom))) DiedToAmarok();
        }
        else
        {
            _outputProvider.Output("Cannot move player out of bounds.", OutputType.Warning);
        }
    }

    private bool CheckForRoom(Coordinates coordinates, Type roomType)
    {
        if (LevelGrid[coordinates.Row, coordinates.Column].GetType() == roomType) return true;
        return false;
    }

    private void GetUserAction()
    {
        _outputProvider.Output("What do you want to do?", OutputType.Question);
        string action = _inputProvider.Input();
        ActBasedOnInput(action);
        
    }
    
    private void PrintDivider()
    {
        _outputProvider.Output("----------------------------------------------------------------------", OutputType.Descriptive);
    }

    private Coordinates GetDirectionalCoordinates(Coordinates coordinates, Direction direction)
    {
        Coordinates newCoordinates = coordinates;
        switch (direction)
        {
            case Direction.East:
                newCoordinates = coordinates with{Row=coordinates.Row, Column = coordinates.Column + 1};
                break;
            case Direction.West:
                newCoordinates = coordinates with{Row=coordinates.Row, Column = coordinates.Column - 1};
                break;
            case Direction.North:
                newCoordinates = coordinates with{Row=coordinates.Row - 1, Column = coordinates.Column};
                break;
            case Direction.South:
                newCoordinates = coordinates with{Row=coordinates.Row + 1, Column = coordinates.Column};
                break;
        }

        return newCoordinates;
    }

    private void Shoot(Coordinates targetRoom)
    {
        if (CoordinatesWithinBounds(targetRoom))
        {
            if (Player.ShootAnArrow())
            {
                if (LevelGrid[targetRoom.Row, targetRoom.Column].GetType() == typeof(MaelstromRoom) ||
                    LevelGrid[targetRoom.Row, targetRoom.Column].GetType() == typeof(AmarokRoom))
                {
                    LevelGrid[targetRoom.Row, targetRoom.Column] = new EmptyRoom();
                    _outputProvider.Output("Monster has been shot!", OutputType.Success);
                }
            }
            else _outputProvider.Output("You don't have any arrows left.", OutputType.Warning);
        }
        else _outputProvider.Output("Can't shoot outside of the bounds.", OutputType.Warning);
    }

    private void ActBasedOnInput(string userInput)
    {
        if (userInput == "help")
        {
            PrintHelp();
            return;
        }
        string[] actionSequence = userInput.Split(" ");
        if (actionSequence.Length != 2)
        {
            _outputProvider.Output("You must provide 2 word commands like \"move (east, west, north, south)\",  " +
                                   "\"(enable, disable) fountain\", \"shoot (east, west, north, south)\" or " +
                                   "\"help\" for help", OutputType.Warning);
            return;
        }
        string mainAction = actionSequence?[0].Trim() ?? "";
        string actionModifier = actionSequence?[1].Trim() ?? "";
        if (mainAction == "move")
        {
            switch (actionModifier)
            {
                case "east":
                    MovePlayerTo(GetDirectionalCoordinates(PlayerCoordinate, Direction.East));
                    break;
                case "west":
                    MovePlayerTo(GetDirectionalCoordinates(PlayerCoordinate, Direction.West));
                    break;
                case "north":
                    MovePlayerTo(GetDirectionalCoordinates(PlayerCoordinate, Direction.North));
                    break;
                case "south":
                    MovePlayerTo(GetDirectionalCoordinates(PlayerCoordinate, Direction.South));
                    break;
                default:
                    _outputProvider.Output("Unknown direction", OutputType.Warning);
                    break;
            }
        } else if (actionModifier == "fountain") ToggleFountain(mainAction);
        else if (mainAction == "shoot")
        {
            switch (actionModifier)
            {
                case "east":
                    Shoot(GetDirectionalCoordinates(PlayerCoordinate, Direction.East));
                    break;
                case "west":
                    Shoot(GetDirectionalCoordinates(PlayerCoordinate, Direction.West));
                    break;
                case "north":
                    Shoot(GetDirectionalCoordinates(PlayerCoordinate, Direction.North));
                    break;
                case "south":
                    Shoot(GetDirectionalCoordinates(PlayerCoordinate, Direction.South));
                    break;
                default:
                    _outputProvider.Output("Unknown direction", OutputType.Warning);
                    break;
            }
        }
    }

    private void ToggleFountain(string toggleDirection)
    {
        if (PlayerCoordinate == FountainCoordinate)
        {
            var fountainRoom = GetFountainRoom();
            if (toggleDirection == "enable") fountainRoom.TurnFountainOn();
            else if (toggleDirection == "disable") fountainRoom.TurnFountainOff();
            else _outputProvider.Output("Unknown toggle command, use (enable/disable)", OutputType.Warning);
        }
    }

    private void PrintStatusInformation()
    {
        _outputProvider.Output($"You are in the room at {PlayerCoordinate.ToString()}. You have {Player.ArrowCount} arrows.", OutputType.Descriptive);
    }

    private void PrintWin()
    {
        _outputProvider.Output("The Fountain of Objects has been reactivated, and you have escaped with your life!", OutputType.Win);
        _outputProvider.Output("You win!", OutputType.Win);
        LevelState = LevelState.Won;
        LevelEnded = true;
    }

    private void PrintHelp()
    {
        _outputProvider.Output("  You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.", OutputType.Help);
        _outputProvider.Output("  Light is visible only in the entrance, and no other light is seen anywhere in the caverns.", OutputType.Help);
        _outputProvider.Output("  You must navigate the Caverns with your other senses.", OutputType.Help);
        _outputProvider.Output("  Find the Fountain of Objects, activate it, and return to the entrance.", OutputType.Help);
        _outputProvider.Output("  Look out for pits. You will feel a breeze if a pit is in an adjacent room. If you enter a room with a pit, you will die.", OutputType.Help);
        _outputProvider.Output("  Maelstroms are violent forces of sentient wind. Entering a room with one could transport you to any other location in the caverns. You will be able to hear their growling and groaning in nearby rooms.", OutputType.Help);
        _outputProvider.Output("  Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms.", OutputType.Help);
        _outputProvider.Output("  You carry with you a bow and a quiver of arrows. You can use them to shoot monsters in the caverns but be warned: you have a limited supply.", OutputType.Help);
    }

    private void SenseEntrance()
    {
        if (PlayerCoordinate == EntranceCoordinate)
        {
            _outputProvider.Output("You see light in this room coming from outside the cavern. This is the entrance.",
                OutputType.EntranceLight);
        }
    }

    private void SenseFountain()
    {
        if (PlayerCoordinate == FountainCoordinate)
        {
            var fountainRoom = GetFountainRoom();
            if (fountainRoom.FountainActive) _outputProvider.Output("You hear the rushing waters from the Fountain of Objects. It has been reactivated!", OutputType.Fountain);
            else _outputProvider.Output("You hear water dripping in this room. The Fountain of Objects is here!", OutputType.Fountain);
        }
    }

    private void SensePit()
    {
        if (SenseNearbyRoom(typeof(PitRoom))) _outputProvider.Output("You feel a draft. There is a pit in a nearby room", OutputType.Warning);
    }
    
    private void SenseMaelstrom()
    {
        if (SenseNearbyRoom(typeof(MaelstromRoom))) _outputProvider.Output("You hear the growling and groaning of a maelstrom nearby.", OutputType.Warning);
    }
    
    private void SenseAmarok()
    {
        if (SenseNearbyRoom(typeof(AmarokRoom))) _outputProvider.Output("You can smell the rotten stench of an amarok in a nearby room.", OutputType.Warning);
    }

    private bool SenseNearbyRoom(Type roomType)
    {
        bool roomNearby = false;
        
        foreach (Coordinates neighbors in GenerateNeighborCoordinates(PlayerCoordinate))
        {
            if (LevelGrid[neighbors.Row, neighbors.Column].GetType() == roomType)
            {
                roomNearby = true;
                break;
            }
        }
        return roomNearby;
    }
    
    private void GetSenses()
    {
        if (CheckForWin())
        {
            PrintWin();
            return;
        }
        SenseEntrance();
        SenseFountain();
        SensePit();
        SenseMaelstrom();
        SenseAmarok();
    }

    private Coordinates[] GenerateNeighborCoordinates(Coordinates coordinates)
    {
        var possibleNeighbors = new Coordinates[9];
        int index = 0;
        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                Coordinates possibleNeighbor =
                    coordinates with {Row = coordinates.Row + rowOffset, Column = coordinates.Column + colOffset};
                if (possibleNeighbor != coordinates && CoordinatesWithinBounds(possibleNeighbor)) possibleNeighbors[index] = possibleNeighbor; 
                index++;
            }
        }

        Coordinates[] neighborsOnly = IteratorHelpers.EliminateNulls<Coordinates>(possibleNeighbors);

        return neighborsOnly;
    }

    private void PrintInfo()
    {
        _outputProvider.Output($"Entrance: {EntranceCoordinate.ToString() ?? "null"}", OutputType.Descriptive);
        _outputProvider.Output($"Fountain: {FountainCoordinate.ToString() ?? "null"}", OutputType.Descriptive);
        _outputProvider.Output($"Player: {PlayerCoordinate.ToString() ?? "null"}", OutputType.Descriptive);
        _outputProvider.Output($"Dims: {LevelGrid.GetLength(0).ToString()} x {LevelGrid.GetLength(1).ToString()}", OutputType.Descriptive);
    }

    private void RunATurn()
    {   
        PrintStatusInformation();
        PrintMap();
        GetSenses();
        if (LevelEnded) return;
        GetUserAction();
        PrintDivider();
    }

    public void RunLevel()
    {
        PrintHelp();
        while (!LevelEnded) RunATurn();
    }
}