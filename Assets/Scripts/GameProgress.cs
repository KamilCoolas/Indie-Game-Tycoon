using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Analytics;

namespace Assets.Scripts
{
    public class GameProgress
    {
        private Game game;
        private int currentlyPlaying;
        public GameProgress(Game game, int currentlyPlaying) 
        {
            this.game = game;
            this.currentlyPlaying = currentlyPlaying;
        }
        public Game Game
        {
            get { return game; }
            set { game = value; }
        }
        public int CurrentlyPlaying
        {
            get { return currentlyPlaying; }
            set { currentlyPlaying = value; }
        }
    }
}
