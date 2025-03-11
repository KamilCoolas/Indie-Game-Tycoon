using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Scripts;


public class GameLogic : MonoBehaviour
{
    public static string[,] gamesReleased = new string[100,16];
    public static string[,] employees = new string[100, 11];
    public static string[] gameInProgress;
    public static bool isGameReleased = false;
    public static bool isGameInProgress = false;
    public static int money = 10000;
    public static int turn = 1;
    public static int costPerTurn = 500;
    public static int numberOfGamesIndex = 0;
    public static Dictionary<string, int> atributeId = new Dictionary<string, int>
    {
        {"Arcade", 1},
        {"Endless Runner", 2},
        {"Platformer", 3},
        {"Space", 4},
        {"Pirates", 5},
        {"Fantasy", 6},
        {"2D", 7},
        {"2.5D", 8},
        {"3D", 9}
    };
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
        EmpDrop.options.Add(new TMP_Dropdown.OptionData() { text = "1.You" });
        UpdateMoneyTurn.UpdateMoneyTurnText();
    }
    void Update()
    {
    }
    public static int AttributeLevel(int atributeValue)
    {
        string[] aAttribute = employees[0, atributeValue].Split(".");
        int level = Convert.ToInt32(aAttribute[0]);
        return level;
    }
    public static int CalculateSaleInWeek(int releasedTurn, float avgScore)
    {
        float scoreMultiplier = avgScore * avgScore * 100;
        int turnMultiplier = (turn + 1) - releasedTurn;
        int value = (int)scoreMultiplier / (turnMultiplier * turnMultiplier);
        return value;
    }
    public static void SalesCalculation(string[,] gamesReleased)
    {
        int price = 19;
        for (int i = 0; i < gamesReleased.GetLength(0); i++)
        {
            if (gamesReleased[i, 0] != null)
            {
                int sales = CalculateSaleInWeek(Convert.ToInt32(gamesReleased[i, 11]), Single.Parse(gamesReleased[i, 10]));
                int profit = sales * price;
                int overallsales = Convert.ToInt32(gamesReleased[i, 14]) + sales;
                int overallprofit = Convert.ToInt32(gamesReleased[i, 15]) + profit;
                UpdateMoneyTurn.UpdateMoney(profit);
                gamesReleased[i, 12] = sales.ToString();
                gamesReleased[i, 13] = profit.ToString();
                gamesReleased[i, 14] = overallsales.ToString();
                gamesReleased[i, 15] = overallprofit.ToString();
            }
        }
    }
}
