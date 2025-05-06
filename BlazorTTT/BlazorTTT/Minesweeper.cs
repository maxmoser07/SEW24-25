using System.Data;

namespace BlazorTTT;
            
public class MineSweeper
{
    private int[,] _field_;
    private int[,] logicalField;

    private bool[,] opened;
    
    public MineSweeper(int columns, int rows)
    {
        _field_ = new int[columns, rows];
        opened = new bool[columns, rows];
        logicalField = new int[columns, rows];
    }

    public string this[int col, int row]
    {
        get
        {
            return _field_[col, row].ToString();
        }
    }

    public void Reveal()
    {
        _field_ = logicalField;
    }

    public string GetFieldDisplay(int row, int col)
    {
        return logicalField[row, col] == 9 ? "\ud83d\udca3" : logicalField[row, col].ToString();
    }

    public void SetBombs()
    {
        Random rand = new Random();
        for (int i = 0; i < logicalField.GetLength(0); i++)
        {
            for (int j = 0; j < logicalField.GetLength(1); j++)
            {
                if (rand.Next(0, 9) == 5)
                {
                    logicalField[i, j] = 9;
                }
            }
        }
    }

    private void SetClues()
    {
        for (int i = 0; i < logicalField.GetLength(0); i++)
        {
            for (int j = 0; j < logicalField.GetLength(1); j++)
            {
                if (logicalField[i, j] == 9)
                {
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (x >= 0 && x < logicalField.GetLength(0) && y >= 0 && y < logicalField.GetLength(1))
                            {
                                if (logicalField[x, y] != 9)
                                {
                                    logicalField[x, y]++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void Uncover(int x, int y)
    {
        if (logicalField[x, y] != 9)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < logicalField.GetLength(0) && j >= 0 && j < logicalField.GetLength(1))
                    {
                        if (opened[i, j] != true)
                        {
                            //_field_[i, j] = logicalField[i, j];
                            _field_[i, j] = 10;
                        }
                    }
                }
            }
        }
    }

    public void TryField(int col, int row)
    {
            if (logicalField[col, row] == 9)
            {
                for (int i = 0; i < logicalField.GetLength(0); i++)
                {
                    for (int j = 0; j < logicalField.GetLength(1); j++)
                    {
                        _field_[i, j] = 69;
                    }
                }
            }
            else
            {
                Uncover(col, row);
            }
            opened[col, row] = true;
    }
}