using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeEditor;

namespace Assets.Scripts
{
    public class Agent : ICloneable<Agent>, IUpdatable<Agent>
    {
        Random rnd = new Random();
        private List<AgentFavorite> favoriteGenres = new List<AgentFavorite>();
        private List<AgentFavorite> favoriteThemes = new List<AgentFavorite>();
        private List<AgentFavorite> favoriteGraphics = new List<AgentFavorite>();
        private int playingHoursPerWeek;
        private int statisticMultiplier;
        private Game currentPlayingGame;
        private Game boughtGameThisWeek;
        private List<Game> completedGames = new List<Game>();
        private List<Game> boughtGames = new List<Game>();
        private List<GameProgress> gameProgressList = new List<GameProgress>();

        public Agent(List<AgentFavorite> favoriteGenres, List<AgentFavorite> favoriteThemes, List<AgentFavorite> favoriteGraphics, int playingHoursPerWeek, int statisticMultiplier)
        {
            this.favoriteGenres = favoriteGenres;
            this.favoriteThemes = favoriteThemes;
            this.favoriteGraphics = favoriteGraphics;
            this.playingHoursPerWeek = playingHoursPerWeek;
            this.statisticMultiplier = statisticMultiplier;
        }
        public Agent(List<AgentFavorite> favoriteGenres, List<AgentFavorite> favoriteThemes, List<AgentFavorite> favoriteGraphics, int playingHoursPerWeek, int statisticMultiplier, Game currentPlayingGame, Game boughtGameThisWeek, List<Game> completedGames, List<Game> boughtGames, List<GameProgress> gameProgressList)
        {
            this.favoriteGenres = favoriteGenres;
            this.favoriteThemes = favoriteThemes;
            this.favoriteGraphics = favoriteGraphics;
            this.playingHoursPerWeek = playingHoursPerWeek;
            this.statisticMultiplier = statisticMultiplier;
            this.currentPlayingGame = currentPlayingGame;
            this.boughtGameThisWeek = boughtGameThisWeek;
            this.completedGames = completedGames;
            this.boughtGames = boughtGames;
            this.gameProgressList = gameProgressList;
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
        public int StatisticMultiplier
        {
            get { return statisticMultiplier; }
            set { statisticMultiplier = value; }
        }
        public Game CurrentPlayingGame
        {
            get { return currentPlayingGame; }
            set { currentPlayingGame = value; }
        }
        public Game BoughtGameThisWeek
        {
            get { return boughtGameThisWeek; }
            set { boughtGameThisWeek = value; }
        }
        public List<Game> CompletedGames
        { 
            get { return completedGames; } 
            set { completedGames = value; }
        }
        public List<Game> BoughtGames
        {
            get { return boughtGames; }
            set { boughtGames = value; }
        }
        public List<GameProgress> GameProgressList
        {
            get { return gameProgressList; }
            set { gameProgressList = value; }
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
                if (gameToPlay == null) gameToPlay = boughtGames[rnd.Next(0, boughtGames.Count)];
            }
            if (!boughtGames.Contains(gameToPlay))
            {
                boughtGames.Add(gameToPlay);
                boughtGameThisWeek = gameToPlay;
            }
            return gameToPlay;
        }
        public void PlayGame(List<Game> listOfGames)
        {
            if (currentPlayingGame != null)
            {
                GameProgress gameInProgress = gameProgressList.Find(x => x.Game.Equals(currentPlayingGame));
                if (gameInProgress.CurrentlyPlaying < currentPlayingGame.GameDuration) gameInProgress.CurrentlyPlaying += playingHoursPerWeek;
                else
                {
                    completedGames.Add(currentPlayingGame);
                    currentPlayingGame = CalculateCurrentPlayingGame(listOfGames);
                    GameProgress gameStarted = new(currentPlayingGame, 0);
                    gameProgressList.Add(gameStarted);
                }
            }
            else
            {
                currentPlayingGame = CalculateCurrentPlayingGame(listOfGames);
                GameProgress gameStarted = new(currentPlayingGame, 0);
                gameProgressList.Add(gameStarted);
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
        public Agent Clone()
        {
            Agent clonedAgent = new Agent(FavoriteGenres, FavoriteThemes, FavoriteGraphics, PlayingHoursPerWeek, StatisticMultiplier, CurrentPlayingGame, BoughtGameThisWeek, CompletedGames, BoughtGames, GameProgressList);
            return clonedAgent;
        }
        public void UpdateFrom(Agent source)
        {
            FavoriteGenres = source.FavoriteGenres;
            FavoriteThemes = source.FavoriteThemes;
            FavoriteGraphics = source.FavoriteGraphics;
            PlayingHoursPerWeek = source.PlayingHoursPerWeek;
            StatisticMultiplier = source.StatisticMultiplier;
            CurrentPlayingGame = source.CurrentPlayingGame;
            BoughtGameThisWeek = source.BoughtGameThisWeek;
            CompletedGames = source.CompletedGames;
            BoughtGames = source.BoughtGames;
            GameProgressList = source.GameProgressList;
        }
    }
}
