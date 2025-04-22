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
    }
}