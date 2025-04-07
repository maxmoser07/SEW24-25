using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;

namespace BlazorTTT;

public class Connect5_Flags : IGame
{
    public char[,] Field = new char[15, 15];//col row
    protected char EmptyCell = '\0';
    //1 is player 1; -1 is player 2;
    //0 is nothing
    public char Turn = 'X';
    private GameState _gameState = GameState.Going;
    private List<Point> Flags = new ();
    public Connect5_Flags()
    {
        Console.Write("Connect4");
        for (var index0 = 0; index0 < Field.GetUpperBound(0); index0++)
        for (var index1 = 0; index1 < Field.GetUpperBound(1); index1++)
        {
            var item = Field[index0, index1];
            item = EmptyCell;
        }
        Console.Write("Finished");
    }
    public string this[int x, int y]
    {
        get
        {
            return Field[x, y].ToString();
        }
    }



    public string? Winner
    {
        get
        {
            return _gameState.ToString();
        }
    }

    public string? NextPlayer => Turn.ToString();

    public void Set(int col, int row)
    {
        if (Field[col, row] == EmptyCell)
        {
            Field[col, row] = Turn;
            Turn = Turn == 'X' ? 'O' : 'X';
            Flags.Add(new Point(col, row));
            Console.WriteLine("added points");
            Console.WriteLine("------------");
            showPoints();
            Console.WriteLine("------------");
            _gameState = CheckWinner();
            
        }
    }

    void showPoints()
    {
        foreach (var item in Flags)
        {
            Console.WriteLine($"({item.X}|{item.Y})");
        }
    }
    public GameState CheckWinner()
    {
        GameState w = GameState.Going;
        foreach (var flag in Flags)
        {
            
             w = LineWinner(flag.X, flag.Y, Field.GetUpperBound(0));
             Console.WriteLine($"Pos: ({flag.X}|{flag.Y}) {w}");
             if (w == GameState.Going) w = LineWinner(flag.Y, flag.X, Field.GetUpperBound(1));
             if (w == GameState.Going) w = DiagonalWinner(flag.X, flag.Y, Field.GetUpperBound(0), Field.GetUpperBound(1));
             if (w != GameState.Going)
             {
                 return w;
             }

        }
        return GameState.Going;
    }

    protected GameState LineWinner(int col, int row, int upperBound)
    {
        bool WonPos = true;
        char player = Field[col, row];
        if (player == EmptyCell)
        {
            return GameState.Going;
        }


        for (var i = col; i <= upperBound && i <= col + 4; i++)
        {
            if (col == 0 && row == 0)
            {
                Console.WriteLine($"Pos: ({i}|{row})");
            }
            if (player != Field[i, row])
            {
                WonPos = false;
                break;
            }
        }

        if (!WonPos)
        {
            for (var i = col; i >= 0 && i >= col - 4; i++)
            {
                if (player != Field[i, row])
                { 
                    return GameState.Going;
                }
            }
        }

        return player == 'X' ? GameState.PlayerXWins : GameState.PlayerOWins;
    }

    protected GameState DiagonalWinner(int col, int row, int colBound, int rowBound)
    {
        char player = Field[col, row];
        if (player == EmptyCell)
        {
            return GameState.Going;
        }

        int count;

        // Check diagonal ↘ (bottom-left to top-right)
        count = 1;
        for (int i = 1; col + i <= colBound && row + i <= rowBound && Field[col + i, row + i] == player; i++)
        {
            count++;
        }
        for (int i = 1; col - i >= 0 && row - i >= 0 && Field[col - i, row - i] == player; i++)
        {
            count++;
        }
        if (count >= 5)
        {
            return player == 'X' ? GameState.PlayerXWins : GameState.PlayerOWins;
        }

        // Check diagonal ↙ (top-left to bottom-right)
        count = 1;
        for (int i = 1; col + i <= colBound && row - i >= 0 && Field[col + i, row - i] == player; i++)
        {
            count++;
        }
        for (int i = 1; col - i >= 0 && row + i <= rowBound && Field[col - i, row + i] == player; i++)
        {
            count++;
        }
        if (count >= 5)
        {
            return player == 'X' ? GameState.PlayerXWins : GameState.PlayerOWins;
        }

        return GameState.Going;
    }

}
