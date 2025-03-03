using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    public static string[,] gamesReleased = new string[100,12];
    public static string[,] employees = new string[100, 11];
    public static string[] gameInProgress;
    public static bool isGameReleased = false;
    public static bool isGameInProgress = false;
    public static int money = 10000;
    public static int turn = 1;
    public TMP_Text moneyText;
    public TMP_Text turnText;
    public static int numberOfGamesIndex = 0;
    public TMP_Dropdown GameDrop;
    public TMP_Dropdown EmpDrop;
    void Start()
    {
        employees[0, 0] = "You";
        employees[0, 1] = "0.00";
        employees[0, 2] = "0.00";
        employees[0, 3] = "0.00";
        employees[0, 4] = "0.00";
        employees[0, 5] = "0.00";
        employees[0, 6] = "0.00";
        employees[0, 7] = "0.00";
        employees[0, 8] = "0.00";
        employees[0, 9] = "0.00";
        employees[0, 10] = "0";
        EmpDrop.options.Add(new TMP_Dropdown.OptionData() { text = "1.You" });
    }
    void Update()
    {
        moneyText.text = "Money: " + money + "$";
        turnText.text = "Turn: " + turn;
        if (Convert.ToInt32(gameInProgress[4]) <= 0 && isGameInProgress)
        {      
            gamesReleased[numberOfGamesIndex, 0] = (numberOfGamesIndex + 1).ToString();
            gamesReleased[numberOfGamesIndex, 1] = gameInProgress[0];
            gamesReleased[numberOfGamesIndex, 2] = gameInProgress[1];
            gamesReleased[numberOfGamesIndex, 3] = gameInProgress[2];
            gamesReleased[numberOfGamesIndex, 4] = gameInProgress[3];
            gamesReleased[numberOfGamesIndex, 5] = gameInProgress[5];
            int genreValue = GameInProgressGenre();
            int themeValue = GameInProgressTheme();
            int graphicValue = GameInProgressGraphic();
            int gameQuality = (AttributeLevel(genreValue) + AttributeLevel(themeValue) + AttributeLevel(graphicValue)) / 3;
            int maxRating = gameQuality + 2;
            if (gameQuality >= 10) gameQuality = 9;
            if (maxRating >= 11) maxRating = 11;
            int review1 = Random.Range(gameQuality, maxRating);
            int review2 = Random.Range(gameQuality, maxRating);
            int review3 = Random.Range(gameQuality, maxRating);
            int review4 = Random.Range(gameQuality, maxRating);
            gamesReleased[numberOfGamesIndex, 6] = review1.ToString();
            gamesReleased[numberOfGamesIndex, 7] = review2.ToString();
            gamesReleased[numberOfGamesIndex, 8] = review3.ToString();
            gamesReleased[numberOfGamesIndex, 9] = review4.ToString();
            float avarageScore = ((float)review1 + (float)review2 + (float)review3 + (float)review4) / 4.0f;
            gamesReleased[numberOfGamesIndex, 10] = avarageScore.ToString();
            gamesReleased[numberOfGamesIndex, 11] = turn.ToString();
            GameDrop.options.Add(new TMP_Dropdown.OptionData() { text = gamesReleased[numberOfGamesIndex, 0] + "." + gamesReleased[numberOfGamesIndex, 1] });
            numberOfGamesIndex += 1;
            gameInProgress = new string[] { };
            isGameReleased = true;
            isGameInProgress = false;
        }
        
    }
    public static int GameInProgressGenre()
    {
        int genreValue = 1;
        switch (gameInProgress[1])
        {
            case "Arcade":
                genreValue = 1;
                break;
            case "Endless Runner":
                genreValue = 2;
                break;
            case "Platformer":
                genreValue = 3;
                break;
        }
        return genreValue;
    }
    public static int GameInProgressTheme()
    {
        int themeValue = 4;
        switch (gameInProgress[2])
        {
            case "Space":
                themeValue = 4;
                break;
            case "Pirates":
                themeValue = 5;
                break;
            case "Fantasy":
                themeValue = 6;
                break;
        }
        return themeValue;
    }
    public static int GameInProgressGraphic()
    {
        int graphicValue = 7;
        switch (gameInProgress[3])
        {
            case "2D":
                graphicValue = 7;
                break;
            case "2.5D":
                graphicValue = 8;
                break;
            case "3D":
                graphicValue = 9;
                break;
        }
        return graphicValue;
    }
    public int AttributeLevel(int atributeValue)
    {
        string[] aAttribute = GameLogic.employees[0, atributeValue].Split(".");
        int level = Convert.ToInt32(aAttribute[0]);
        return level;
    }
}
