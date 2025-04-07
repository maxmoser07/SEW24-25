using System.Diagnostics;

namespace BlazorTTT;

public abstract class AWinner
{


    protected virtual string? CheckWinner()
    {
        string? w = HorizontalWinner();
        if (w == null) w = VerticalWinner();
        if (w == null) w = DiagonalWinner1();
        if (w == null) w = DiagonalWinner2();
        if (w == null)
            return "No Winner / Draw";

        return w;
    }

    protected abstract string? HorizontalWinner();
    protected abstract string? VerticalWinner();
    protected abstract string? DiagonalWinner1();
    protected abstract string? DiagonalWinner2();
}