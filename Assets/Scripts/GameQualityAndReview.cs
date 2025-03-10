using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    internal static class GameQualityAndReview
    {
        public static void GameQuality()
        {
            int gameQuality = 0;
            for (int i = 1; i <= 3; i++)
            {
                gameQuality += (GameLogic.AttributeLevel(GameLogic.atributeId[GameLogic.gameInProgress[i]]));
            }
            gameQuality /= 3;
            int maxRating = gameQuality + 2;
            if (gameQuality >= 10) gameQuality = 9;
            if (maxRating >= 11) maxRating = 11;
            float avarageScore = 0;
            for (int i = 6; i <= 9; i++)
            {
                int review = Random.Range(gameQuality, maxRating);
                avarageScore += review;
                GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, i] = review.ToString();
            }
            avarageScore /= 4.0f;
            GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, 10] = avarageScore.ToString();
            GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, 11] = GameLogic.turn.ToString();
        }
    }
}
