using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

public enum Guess
{
    Person1,
    Person2,
    SameAge,
    None
};

namespace Age_Guessing_Game
{
    class Player
    {
        public Player(ProgressBar progressBar)
        {
            playerProgressBar = progressBar;
            CurrentGuess = Guess.None;
            Score = 0;
        }

        private ProgressBar playerProgressBar;

        public Guess CurrentGuess { get; set; }

        public int Score
        {
            get
            {
                return (int) playerProgressBar.Value;
            }
            private set
            {
                playerProgressBar.Value = value;
            }
        }

        public void EvaluateGuess(Guess correct)
        {
            if (CurrentGuess == correct)
            {
                Score++;
            }
            CurrentGuess = Guess.None;
        }       
    }
}
