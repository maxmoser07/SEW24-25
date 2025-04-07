namespace BlazorTTT;

public interface IGame
{
    public string? this[int col, int row] { get; }
    public string? Winner { get; }
    public string? NextPlayer { get; }
    public void Set(int col, int row);
}