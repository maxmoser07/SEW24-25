using System.Data;

namespace BlazorTTT;
            
public class MineSweeper
{
    private int[,] _field_;

    private bool[,] opened;
    
    public MineSweeper(int columns, int rows)
    {
        _field_ = new int[columns, rows];
        opened = new bool[columns, rows];
    }

    public string this[int col, int row]
    {
        get
        {
            return _field_[col, row].ToString();
        }
        set
        {
            _field_[col, row] = int.Parse(value);
        }
    }

    public void SetBombs()
    {
        Random rand = new Random();
        for (int i = 0; i < _field_.GetLength(0); i++)
        {
            for (int j = 0; j < _field_.GetLength(1); j++)
            {
                if (rand.Next(0, 9) == 5)
                {
                    _field_[i, j] = 9;
                }
            }
        }
        SetClues();
    }

    public void SetClues()
    {
        for (int i = 0; i < _field_.GetLength(0); i++)
        {
            for (int j = 0; j < _field_.GetLength(1); j++)
            {
                if (_field_[i, j] == 9)
                {
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (x >= 0 && x < _field_.GetLength(0) && y >= 0 && y < _field_.GetLength(1))
                            {
                                if (_field_[x, y] != 9)
                                {
                                    _field_[x, y]++;
                                }
                            }
                        }
                    }
                }
            }
        }
    } 
    
}