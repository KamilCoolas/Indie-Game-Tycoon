using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Scripts;
using Random = UnityEngine.Random;
using UnityEditor;


public class GameLogic : MonoBehaviour
{
    public List<Game> gamesReleased = new List<Game>();
    public static string[,] employees = new string[100, 11];
    public static string[] gameInProgress;
    public static bool isGameReleased = false;
    public static bool isGameInProgress = false;
    public static int money = 10000;
    public static int turn = 1;
    public static int costPerTurn = 500;
    public static int numberOfGamesIndex = 1;
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
    public TMP_Dropdown GameDrop;
    public Button CreateNewGameButton;
    public Canvas GameRel;
    public TMP_Dropdown EmpDrop;
    public TMP_Text MoneyText;
    public TMP_Text TurnText;
    private List<MoneyChange> moneyChangeList = new List<MoneyChange>();
    public GameObject IncomeCostText;
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
        UpdateMoneyTurnText();
    }
    void Update()
    {
    }
    public int AttributeLevel(int atributeValue)
    {
        string[] aAttribute = employees[0, atributeValue].Split(".");
        int level = Convert.ToInt32(aAttribute[0]);
        return level;
    }
    public void UpdateMoneyTurnText()
    {
        MoneyText.text = money + "$";
        TurnText.text = "Turn: " + turn;
    }
    public void UpdateMoney(int moneyToAdd, string source)
    {
        MoneyChange itemToDisplay = new MoneyChange(moneyToAdd, source);
        moneyChangeList.Add(itemToDisplay);
        money += moneyToAdd;
        UpdateMoneyTurnText();
    }
    public int CalculateGameQuality(Game game)
    {
        int gameQuality = (AttributeLevel(atributeId[game.Genre]) + AttributeLevel(atributeId[game.Theme]) + AttributeLevel(atributeId[game.Graphics])) / 3;
        return gameQuality;
    }
    public void IncomeCostTextGenerator()
    {
        for (int i = 0; i < moneyChangeList.Count; i++)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            GameObject obj = Instantiate(IncomeCostText, position, Quaternion.identity);
            var InitScript = obj.GetComponent<InitialScript>();
            InitScript.SetAmount(moneyChangeList[i].Amount);
            InitScript.SetSource(moneyChangeList[i].Source);
            InitScript.SetY(i);
        }
        moneyChangeList.Clear();
    }
    public void GenerateIncome()
    {
        for (int i = 0; i < gamesReleased.Count; i++)
        {
            gamesReleased[i].SalesCalculation(turn);
            int profit = gamesReleased[i].GetIncomeForTurn(turn);
            if (profit != 0) UpdateMoney(profit, gamesReleased[i].Title);
        }
    }
    public void GameReleased()
    {
        Game newGame = new(gameInProgress[0], gameInProgress[1], gameInProgress[2], gameInProgress[3]);
        newGame.GameQuality = CalculateGameQuality(newGame);
        newGame.ReleasedTurn = turn;
        newGame.ReviewGenerator();
        gamesReleased.Add(newGame);
        GameDrop.options.Add(new TMP_Dropdown.OptionData() { text = numberOfGamesIndex + "." + newGame.Title });
        numberOfGamesIndex++;
        gameInProgress = new string[] { };
        isGameReleased = true;
        isGameInProgress = false;
        GameRel.GetComponent<GameReleased>().AssignReviewText(newGame);
        CreateNewGameButton.interactable = true;
    }
}
