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
                _field_[i, j] = rand.Next(0, 2);
            }
        }
    }
    
}