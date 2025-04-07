using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;

namespace BlazorTTT;

public class Connect5 : AWinner, IGame
{
    public char[,] Field = new char[15, 15];//col row
    protected char EmptyCell = '\0';
    //1 is player 1; -1 is player 2;
    //0 is nothing
    public char Turn = 'X';
    public GameState GameState = GameState.Going;
    private List<Point> Flags = new ();
    public Connect5()
    {
        Debug.Write("Connect4");
        for (var index0 = 0; index0 < Field.GetLength(0); index0++)
        for (var index1 = 0; index1 < Field.GetLength(1); index1++)
        {
            var item = Field[index0, index1];
            item = EmptyCell;
        }
        Debug.Write("Finished");
    }
    public string this[int x, int y]
    {
        get
        {
            Debug.Write("GETTER");
            return Field[x, y].ToString();
        }
    }

    

    public string? Winner { get; private set; } = "No Winner / Draw";
    public string? NextPlayer => Turn.ToString();

    public void Set(int col, int row)
    {
        if (Field[col, row] == EmptyCell)
        {
            Field[col, row] = Turn;
            Turn = Turn == 'X' ? 'O' : 'X';
            Flags.Add(new Point(col, row));
            Winner = CheckWinner();
            Debug.WriteLine(CheckWinner());
            
        }
    }

    private bool IsInField(int col, int row)
    {
        return (col < Field.GetLength(0) && row < Field.GetLength(1));
    }

    protected override string? HorizontalWinner()
    {
        for (int row = 0; row < Field.GetLength(1); row++)
        {
            for (int col = 0; col <= 10; col++) // Only check up to column 3 (7-4)
            {
                char player = Field[col, row];
                if (player != EmptyCell &&
                    player == Field[col + 1, row] &&
                    player == Field[col + 2, row] &&
                    player == Field[col + 3, row] &&
                    player == Field[col + 4, row])
                {
                    return $"Winner: {player} (Horizontal)";
                }
            }
        }
        return null;
    }

    protected override string? VerticalWinner()
    {
        for (int col = 0; col < Field.GetLength(0); col++)
        {
            for (int row = 0; row <= 10; row++) // Only check up to column 3 (7-4)
            {
                char player = Field[col, row];
                if (player != EmptyCell &&
                    player == Field[col, row + 1] &&
                    player == Field[col, row + 2] &&
                    player == Field[col, row + 3]&&
                    player == Field[col, row + 4])
                {
                    return $"Winner: {player} (Vertical)";
                }
            }
        }
        return null;
    }

    protected override string? DiagonalWinner1() // Checks diagonals from bottom-left to top-right
    {
        for (int y = 5; y < 15; y++) // Start from row 3 to ensure enough space upwards
        {
            for (int x = 0; x <= 10; x++) // Only check up to column 3
            {
                char player = Field[x, y];
                if (player != EmptyCell &&
                    player == Field[x + 1, y - 1] &&
                    player == Field[x + 2, y - 2] &&
                    player == Field[x + 3, y - 3] &&
                    player == Field[x + 4, y - 4])
                {
                    return $"Winner: {player} (Diagonal /)";
                }
            }
        }
        return null;
    }

    protected override string? DiagonalWinner2() // Checks diagonals from top-left to bottom-right
    {
        for (int y = 0; y <= 10; y++) // Only check up to row 2
        {
            for (int x = 0; x <= 10; x++) // Only check up to column 3
            {
                char player = Field[x, y];
                if (player != EmptyCell &&
                    player == Field[x + 1, y + 1] &&
                    player == Field[x + 2, y + 2] &&
                    player == Field[x + 3, y + 3] &&
                    player == Field[x + 4, y + 4])
                {
                    return $"Winner: {player} (Diagonal \\)";
                }
            }
        }
        return null;
    }
}
