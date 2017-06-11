using System;

public class Game
{
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (OnScoreChanged != null)
                OnScoreChanged(this);
        }
    }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (OnWaveChanged != null)
                OnWaveChanged(this);
        }
    }

    public event Action<Game> OnScoreChanged;
    public event Action<Game> OnWaveChanged;

    public Game()
    {
        Score = 0;
        Wave = 1;
    }
}
