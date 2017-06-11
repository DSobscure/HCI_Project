using System;

namespace HCI_Project.Library
{
    public class Game
    {
        private int score;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnScoreChanged?.Invoke(this);
            }
        }

        private int wave;
        public int Wave
        {
            get { return wave; }
            set
            {
                wave = value;
                OnWaveChanged?.Invoke(this);
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
}
