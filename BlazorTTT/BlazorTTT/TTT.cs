namespace BlazorTTT;

public enum GameState
{
    PlayerXWins,
    PlayerOWins,
    Going
}
public class TTT
{
    private int[,] Field = new int[3,3];
    //1 is player 1; -1 is player 2;
    //0 is nothing
    public int Turn = 1;
    public GameState GameState = GameState.Going;
    private void checkForWinner()
    {
        int diag1 = Field[0, 0] + Field[1, 1] + Field[2, 2];
        int diag2 = Field[0, 2] + Field[1, 1] + Field[2, 0];
        for (int i = 0; i < 3; i++)
        {
            int rowsum = Field[0, i] + Field[1, i] + Field[2, i];
            int colsum = Field[i, 0] + Field[i, 1] + Field[i, 2];
            if (rowsum == 3 || colsum == 3|| diag1 == 3 || diag2 == 3)
            {
                GameState = GameState.PlayerXWins;
                return;
            }
            else if (rowsum == -3 || colsum == -3 || diag1 == -3 || diag2 == -3)
            {
                GameState = GameState.PlayerOWins;
                return;
            }
            GameState = GameState.Going;
        }
    }

    public string this[int x, int y]
    {
        get
        {
            if (Field[x, y] == 0)
            {
                return "\0";
            }
            return Field[x, y] == 1 ? "x" : "o";
        }
        set
        {
            if (GameState == GameState.Going)
            {
                if (Field[x, y] == 0)
                {
                    Field[x, y] = Turn;
                    Turn = Turn == 1 ? -1 : 1; //switcheroo
                };
                checkForWinner();
            }

        }
    }
}