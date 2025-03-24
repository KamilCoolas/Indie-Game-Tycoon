using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Agent
    {
        private List<AgentFavorite> favoriteGenres = new List<AgentFavorite>();
        private List<AgentFavorite> favoriteThemes = new List<AgentFavorite>();
        private List<AgentFavorite> favoriteGraphics = new List<AgentFavorite>();
        private int playingHoursPerWeek;
        private Game currentPlayingGame;
        private List<Game> completedGames = new List<Game>();

        public Agent(List<AgentFavorite> favoriteGenres, List<AgentFavorite> favoriteThemes, List<AgentFavorite> favoriteGraphics, int playingHoursPerWeek)
        {
            this.favoriteGenres = favoriteGenres;
            this.favoriteThemes = favoriteThemes;
            this.favoriteGraphics = favoriteGraphics;
            this.playingHoursPerWeek = playingHoursPerWeek;
        }
        public List<AgentFavorite> FavoriteGenres
        {
            get { return favoriteGenres; }
            set { favoriteGenres = value; }
        }
        public List<AgentFavorite> FavoriteThemes
        {
            get { return favoriteThemes; }
            set { favoriteThemes = value; }
        }
        public List<AgentFavorite> FavoriteGraphics
        {
            get { return favoriteGraphics; }
            set { favoriteGraphics = value; }
        }
        public int PlayingHoursPerWeek
        {
            get { return playingHoursPerWeek; }
            set { playingHoursPerWeek = value; }
        }
        public Game CurrentPlayingGame
        {
            get { return currentPlayingGame; }
            set { currentPlayingGame = value; }
        }
        public List<Game> CompletedGames
        { 
            get { return completedGames; } 
            set { completedGames = value; }
        }
        public Game CalculateCurrentPlayingGame(List<Game> listOfGames)
        {
            int maxScore = 0;
            Game gameToPlay = null;
            foreach (Game game in listOfGames)
            {
                if (!completedGames.Contains(game))
                {
                    int score = 0;
                    score += CompareGameAttributeAndFavorites(favoriteGenres, game.Genre);
                    score += CompareGameAttributeAndFavorites(favoriteThemes, game.Theme);
                    score += CompareGameAttributeAndFavorites(favoriteGraphics, game.Graphics);
                    score += game.GameQuality * 10;
                    if (score > maxScore)
                    {
                        maxScore = score;
                        gameToPlay = game;
                    }
                }
            }
            return gameToPlay;
        }
        public void PlayGame(List<Game> listOfGames)
        {
            if (currentPlayingGame != null)
            {
                if (currentPlayingGame.GameDuration > 0) currentPlayingGame.GameDuration -= playingHoursPerWeek;
                else
                {
                    completedGames.Add(currentPlayingGame);
                    currentPlayingGame = CalculateCurrentPlayingGame(listOfGames);
                }
            }
            else
            {
                currentPlayingGame = CalculateCurrentPlayingGame(listOfGames);
            }
        }
        public int CompareGameAttributeAndFavorites (List<AgentFavorite> favoriteList, string gameAttribute)
        {
            int maxScore = 0;
            foreach (AgentFavorite favorite in favoriteList)
            {
                int score = 0;
                if (favorite.Name == gameAttribute) score = Convert.ToInt32(50 * favorite.Weight);
                if (score > maxScore) maxScore = score;
            }
            return maxScore;
        }
    }
}
