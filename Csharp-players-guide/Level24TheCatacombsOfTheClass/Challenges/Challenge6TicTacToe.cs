// •Build the game of Tic-Tac-Toe as described in the requirements above. Starting with CRC cards is recommended,
//              but the goal is to make working software, not CRC cards.
// •Answer this question: How might you modify your completed program if running multiple rounds was a requirement
//              (for example, a best-out-of-five series)?

namespace Level24TheCatacombsOfTheClass.Challenges;

using Level24TheCatacombsOfTheClass.Enums;

public class TicTacToeTile
{
    public TicTacToeTileState State { get; private set; } = TicTacToeTileState.Empty;

    public TicTacToeTile()
    {
        State = TicTacToeTileState.Empty;
    }

    public bool UpdateState(TicTacToeTileState newState)
    {
        if (State != TicTacToeTileState.Empty) return false;
        State = newState;
        return true;
    }

    public string Print()
    {
        string toPrint = State switch
        {
            TicTacToeTileState.Empty => " ",
            TicTacToeTileState.X => "X",
            TicTacToeTileState.O => "O",
        };
        return toPrint;
    }
}

public class TicTacToePlayer
{
    public TicTacToeSign Sign { get; private set; }
    public TicTacToeTileState TileValue { get; private set; }

    public TicTacToePlayer(TicTacToeSign sign)
    {
        Sign = sign;
        TileValue = (sign == TicTacToeSign.O) ? TicTacToeTileState.O : TicTacToeTileState.X;
    }
}


public class TicTacToeBoard
{
    public int SideSize { get; init; }
    public TicTacToeTile[,] Tiles { get; private set; }
    public TicTacToeGameState State { get; private set; }
    public TicTacToeTileState Winner { get; private set; }

    public TicTacToeBoard(int sideSize)
    {
        SideSize = sideSize;
        Tiles = new TicTacToeTile[sideSize, sideSize];
        State = TicTacToeGameState.Running;
        for (int column = 0; column < SideSize; column++)
        {
            for (int row = 0; row < SideSize; row++) Tiles[row, column] = new TicTacToeTile();
        }
    }

    public int[] PositionToCoords(int position)
    {
        // should be checking if possible here
        int row = (position-1) / SideSize;
        int column = (position-1) % SideSize;
        return new int[2] {row, column};
    }

    public bool UpdateTile(int row, int column, TicTacToeTileState state)
    {
        bool success =  Tiles[row, column].UpdateState(state);

        if (success) DetermineGameState();
        
        return success;
    }

    public TicTacToeTile[] GetDiagonal()
    {
        TicTacToeTile[] diagonal = new TicTacToeTile[SideSize];
        for (int i = 0; i < SideSize; i++) diagonal[i] = Tiles[SideSize - i - 1, i];
        
        return diagonal;
    }
    
    public TicTacToeTile[] GetOffDiagonal()
    {
        TicTacToeTile[] offDiagonal = new TicTacToeTile[SideSize];
        for (int i = 0; i < SideSize; i++) offDiagonal[i] = Tiles[i, i];

        return offDiagonal;
    }

    public TicTacToeTile[] GetRow(int rowNumber)
    {
        TicTacToeTile[] row = new TicTacToeTile[SideSize];
        for (int i = 0; i < SideSize; i++) row[i] = Tiles[rowNumber, i];
        return row;
    }
    
    public TicTacToeTile[] GetColumn(int columnNumber)
    {
        TicTacToeTile[] column = new TicTacToeTile[SideSize];
        for (int i = 0; i < SideSize; i++) column[i] = Tiles[i, columnNumber];
        return column;
    }

    public TicTacToeTileState GetArrayPossibleWinner(TicTacToeTile[] tileArray)
    {
        TicTacToeTileState possibleWinner = tileArray[0].State;
        for (int i = 1; i < SideSize; i++)
        {
            if (possibleWinner != tileArray[i].State) return TicTacToeTileState.Empty;
        }

        return possibleWinner;
    }

    public bool EmptyTileExists()
    {
        for (int row = 0; row < SideSize; row++)
        {
            for (int column = 0; column < SideSize; column++)
            {
                if (Tiles[row, column].State == TicTacToeTileState.Empty) return true;
            }
        }
        return false;
    }

    public TicTacToeTileState GetPossibleWinner()
    {
        TicTacToeTileState possibleWinner = GetArrayPossibleWinner(GetDiagonal());
        if (possibleWinner != TicTacToeTileState.Empty) return possibleWinner;
        
        possibleWinner = GetArrayPossibleWinner(GetOffDiagonal());
        if (possibleWinner != TicTacToeTileState.Empty) return possibleWinner;
        
        for (int i = 0; i < SideSize; i++)
        {
            possibleWinner = GetArrayPossibleWinner(GetRow(i));
            if (possibleWinner != TicTacToeTileState.Empty) return possibleWinner;
            
            possibleWinner = GetArrayPossibleWinner(GetColumn(i));
            if (possibleWinner != TicTacToeTileState.Empty) return possibleWinner;
        }

        return TicTacToeTileState.Empty;
    }

    public void DetermineGameState()
    {
        TicTacToeTileState possibleWinner = GetPossibleWinner();
        if (!EmptyTileExists() && possibleWinner == TicTacToeTileState.Empty)
        {
            Winner = TicTacToeTileState.Empty;
            State = TicTacToeGameState.Tie;
        }

        if (possibleWinner != TicTacToeTileState.Empty)
        {
            Winner = possibleWinner;
            if (possibleWinner == TicTacToeTileState.O) State = TicTacToeGameState.O;
            else State = TicTacToeGameState.X;
        }
        
    }

    public void PrintResults()
    {
        switch (State)
        {
            case TicTacToeGameState.O:
            case TicTacToeGameState.X:
                Console.WriteLine($"Winner is {State}.");
                break;
            case TicTacToeGameState.Tie:
                Console.WriteLine($"It was a tie.");
                break;
            case TicTacToeGameState.Running:
                Console.WriteLine($"Game is still running.");
                break;
        }
    }
        
    public void DrawBoard()
    {
        for (int row = 0; row < SideSize; row++)
        {
            for (int column = 0; column < SideSize; column++)
            {
                Console.Write($" {Tiles[row, column].Print()} ");
                if (column < SideSize-1) Console.Write("|");
            }
            Console.WriteLine("");
            if (row < SideSize - 1)
            {
                for (int column = 0; column < SideSize; column++)
                {
                    Console.Write("---");
                    if (column < SideSize-1) Console.Write("+");
                }
                Console.WriteLine("");
            }
            
        }
    }
}

public class TicTacToeGame
{
    public TicTacToePlayer Player1 { get; init; }
    public TicTacToePlayer Player2 { get; init; }
    public TicTacToeBoard Board { get; init; }

    public int Round { get; private set; } = 0;

    public TicTacToeGame(int sideSize)
    {
        Player1 = new TicTacToePlayer(TicTacToeSign.X);
        Player2 = new TicTacToePlayer(TicTacToeSign.O);
        Board = new TicTacToeBoard(sideSize);
    }

    public void RunRound()
    {
        TicTacToePlayer playerTurn = Convert.ToBoolean(Round % 2) ? Player1 : Player2;
        Console.WriteLine($"It is {playerTurn.Sign}'s turn.");
        Board.DrawBoard();
        Console.Write("What square do you want to play in? ");
        int position = Convert.ToInt32(Console.ReadLine());
        int[] coords = Board.PositionToCoords(position);
        bool updateResult = Board.UpdateTile(coords[0], coords[1], playerTurn.TileValue);
        if (updateResult) Round++;
        else Console.WriteLine("Illegal move, try again.");
    }

    public void RunGame()
    {
        while (Board.State == TicTacToeGameState.Running)
        {
            RunRound();
        }
        Board.DrawBoard();
        Board.PrintResults();
    }
}

