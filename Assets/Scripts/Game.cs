using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Game : ICloneable<Game>, IUpdatable<Game>
    {
        private string title;
        private string genre;
        private string theme;
        private string graphics;
        private int gameDuration = 24;
        private int price = 19;
        private int gameQuality;
        private List<Review> reviewList = new List<Review>();
        private int releasedTurn;
        private List<Sales> salesList = new List<Sales>();
        private int agents = 0;
        private int gameSalesThisWeek = 0;

        public Game(string title, string genre, string theme, string graphics)
        {
            this.title = title;
            this.genre = genre;
            this.theme = theme;
            this.graphics = graphics;
        }
        public Game(string title, string genre, string theme, string graphics, int gameQuality)
        {
            this.title = title;
            this.genre = genre;
            this.theme = theme;
            this.graphics = graphics;
            this.gameQuality = gameQuality;
        }
        public Game(string title, string genre, string theme, string graphics, int gameQuality, List<Review> reviewList, int releasedTurn, List<Sales> salesList, int agents, int gameSalesThisWeek)
        {
            this.title = title;
            this.genre = genre;
            this.theme = theme;
            this.graphics = graphics;
            this.gameQuality = gameQuality;
            this.reviewList = reviewList;
            this.releasedTurn = releasedTurn;
            this.salesList = salesList;
            this.agents = agents;
            this.gameSalesThisWeek = gameSalesThisWeek;
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public string Theme
        {
            get { return theme; }
            set { theme = value; }
        }
        public string Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public int GameDuration
        {
            get { return gameDuration; }
            set { gameDuration = value; }
        }
        public int GameQuality
        {
            get { return gameQuality; }
            set { gameQuality = value; }
        }
        public List<Review> ReviewList
        {
            get { return reviewList; }
            set { reviewList = value; }
        }
        public int ReleasedTurn
        {
            get { return releasedTurn; }
            set { releasedTurn = value; }
        }
        public int Agents
        {
            get { return agents; }
            set { agents = value; }
        }
        public int GameSalesThisWeek
        {
            get { return gameSalesThisWeek; }
            set { gameSalesThisWeek = value; }
        }
        public List<Sales> SalesList
        {
            get { return salesList; }
            set { salesList = value; }
        }
        //public int CalculateSaleInWeek(int releasedTurn, float avgScore, int turn)
        //{
        //    float scoreMultiplier = avgScore * avgScore * 100;
        //    int turnMultiplier = (turn + 1) - releasedTurn;
        //    int value = (int)scoreMultiplier / (turnMultiplier * turnMultiplier);
        //    return value;
        //}
        public void SalesCalculation(int turn)
        {
                int sales = gameSalesThisWeek;
                Sales thisWeekSales = new(turn, sales, price);
                SalesList.Add(thisWeekSales);
        }
        public int GetOverallSales()
        {
            int overallSales = 0;
            for (int i = 0; i < salesList.Count; i++)
            {
                overallSales += salesList[i].SoldThisWeek;
            }
            return overallSales;
        }
        public int GetOverallIncome()
        {
            int overallIncome = 0;
            for (int i = 0; i < salesList.Count; i++)
            {
                overallIncome += salesList[i].IncomeThisWeek;
            }
            return overallIncome;
        }
        public void ReviewGenerator()
        {
            if (gameQuality >= 10) gameQuality = 9;
            int maxRating = gameQuality + 2;
            for (int i = 0; i < 4; i++)
            {
                int review = Random.Range(gameQuality, maxRating);
                Review thisReview = new("", review);
                reviewList.Add(thisReview);
            }
        }
        public float GetAvarageScore()
        {
            float avarageScore = 0;
            for (int i = 0; i < reviewList.Count; i++)
            {
                avarageScore += reviewList[i].Rate;
            }
            avarageScore /= 4.0f;
            return avarageScore;
        }
        public int GetSalesForTurn(int turn)
        {
            int salesThisTurn = 0;
            for (int i = 0; i < salesList.Count; i++)
            {
                if (salesList[i].Turn == turn)
                    {
                    salesThisTurn = salesList[i].SoldThisWeek;
                    break;
                    }
            }
            return salesThisTurn;
        }
        public int GetIncomeForTurn(int turn)
        {
            int incomeThisTurn = 0;
            for (int i = 0; i < salesList.Count; i++)
            {
                if (salesList[i].Turn == turn)
                {
                    incomeThisTurn = salesList[i].IncomeThisWeek;
                    break;
                }
            }
            return incomeThisTurn;
        }
        public Game Clone()
        {
            Game clonedGame = new Game(Title, Genre, Theme, Graphics, GameQuality, ReviewList, ReleasedTurn, SalesList, Agents, GameSalesThisWeek);
            return clonedGame;
        }
        public void UpdateFrom(Game source)
        {
            Title = source.Title;
            Genre = source.Genre;
            Theme = source.Theme;
            Graphics = source.Graphics;
            GameQuality = source.GameQuality;
            ReviewList = source.ReviewList;
            ReleasedTurn = source.ReleasedTurn;
            SalesList = source.SalesList;
            Agents = source.Agents;
            GameSalesThisWeek = source.GameSalesThisWeek;
        }
    }
}
