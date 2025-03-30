using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Scripts;
using Random = UnityEngine.Random;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;
using System.Threading;
using System.Threading.Tasks;
using TreeEditor;
using System.ComponentModel;
using static Unity.VisualScripting.Member;
using System.Collections.Concurrent;


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
    public static Dictionary<int, string> gameAttributes = new Dictionary<int, string>
    {
        {1, "Arcade"},
        {2, "Endless Runner"},
        {3, "Platformer"},
        {4, "Space"},
        {5, "Pirates"},
        {6, "Fantasy"},
        {7, "2D"},
        {8, "2.5D"},
        {9, "3D"}
    };
    public TMP_Dropdown GameDrop;
    public TMP_Dropdown AllGameDrop;
    public Button CreateNewGameButton;
    public Canvas GameRel;
    public TMP_Dropdown EmpDrop;
    public TMP_Text MoneyText;
    public TMP_Text TurnText;
    private List<MoneyChange> moneyChangeList = new List<MoneyChange>();
    public GameObject IncomeCostText;
    public List<Game> allGames = new List<Game>();
    public List<Agent> allAgents = new List<Agent>();
    public ConcurrentBag<(Game game, int agents, int bought)> gamesToUpdate = new ConcurrentBag<(Game, int, int)>();
    private AsyncOperationManager<Agent> agentManager = new AsyncOperationManager<Agent>();
    private AsyncOperationManager<Game> gameManager = new AsyncOperationManager<Game>();
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
        GenerateGames(10);
        GenerateAgents(10000);
        _ = StartNewTurnCalculation();
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
        allGames.Add(newGame);
        GameDrop.options.Add(new TMP_Dropdown.OptionData() { text = gamesReleased.Count + "." + newGame.Title });
        AllGameDrop.options.Add(new TMP_Dropdown.OptionData() { text = allGames.Count + "." + newGame.Title });
        gameInProgress = new string[] { };
        isGameReleased = true;
        isGameInProgress = false;
        GameRel.GetComponent<GameReleased>().AssignReviewText(newGame);
        CreateNewGameButton.interactable = true;
    }
    public void GenerateGames (int amount)
    {
        for (int i = 0;  i < amount; i++)
        {
            Game game = new Game("Game " + (i+1).ToString(), gameAttributes[Random.Range(1, 4)], gameAttributes[Random.Range(4, 7)], gameAttributes[Random.Range(7, 10)], Random.Range(1, 11));
            game.ReleasedTurn = turn;
            game.ReviewGenerator();
            allGames.Add(game);
            AllGameDrop.options.Add(new TMP_Dropdown.OptionData() { text = numberOfGamesIndex + "." + game.Title });
            numberOfGamesIndex++;
        }
    }
    public void GenerateAgents(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            List<AgentFavorite> genreList = new(AgentFavoritesGenerator(1,4, Random.Range(1, 4)));
            List<AgentFavorite> themeList = new(AgentFavoritesGenerator(4, 7, Random.Range(1, 4)));
            List<AgentFavorite> graphicsList = new(AgentFavoritesGenerator(7, 10, Random.Range(1, 4)));
            Agent agent = new Agent(genreList, themeList, graphicsList, Random.Range(1, 24), Random.Range(1, 100));
            allAgents.Add(agent);
        }
    }
    public List<AgentFavorite> AgentFavoritesGenerator(int minInc, int maxExc, int amount)
    {
        List <AgentFavorite> agentFavorites = new List <AgentFavorite>();
        for (int i = 0; i < amount; i++)
        {
            AgentFavorite agentFavorite = new(gameAttributes[Random.Range(minInc, maxExc)], Random.Range(0.0f, 1.0f));
            agentFavorites.Add(agentFavorite);
        }
        return agentFavorites;
    }
    public void NullGameAgents()
    {
        foreach (var game in allGames)
        {
            game.Agents = 0;
        }
    }
    //public void AgentsPlayingAndBuyingGames()
    //{
    //    foreach (var game in allGames)
    //    {
    //        game.SalesCalculation(turn);
    //        game.GameSalesThisWeek = 0;
    //    }
    //}
    async Task<Agent> AgentPlayingAndBuyingGame(Agent agent)
    {
        agent.PlayGame(allGames);
        UpdateGamesToUpdateList(agent.CurrentPlayingGame, agent.StatisticMultiplier, 0);
        //Debug.Log("Agent Playing: " + agent.CurrentPlayingGame.Title);
        if (agent.BoughtGameThisWeek != null)
        {
            UpdateGamesToUpdateList(agent.CurrentPlayingGame, 0, agent.StatisticMultiplier);
            //Debug.Log("Agent Buying: " + agent.BoughtGameThisWeek.Title);
            agent.BoughtGameThisWeek = null;
        }
        return agent;
    }
    async Task<Game> GamePlayingAndBuying(Game game, int agents, int bought)
    {
        game.Agents = agents;
        game.GameSalesThisWeek = bought;
        Debug.Log("Game: " + game.Title + " Current Agents: " + game.Agents + " Current Sales: " + game.GameSalesThisWeek);
        return game;
    }
    public async Task StartNewTurnCalculation()
    {
        foreach (var agent in allAgents)
        {
            await agentManager.AddOperationAsync(AgentPlayingAndBuyingGame, agent);
        }
        await Task.WhenAll(agentManager.GetPendingTasks());
        var result = gamesToUpdate
            .GroupBy(item => item.game) // Grupujemy po stringu
            .Select(group => (
            game: group.Key,        // Klucz grupy (string)
            agents: group.Sum(x => x.agents), // Suma pierwszej wartoœci int
            bought: group.Sum(x => x.bought)  // Suma drugiej wartoœci int
            ))
            .ToList();
        foreach (var game in result)
        {
            await gameManager.AddOperationAsync(g => GamePlayingAndBuying(g, game.agents, game.bought), game.game);
        }
    }
    private void UpdateGamesToUpdateList(Game game, int agent, int bought)
    {
            gamesToUpdate.Add((game, agent, bought));
    }
    public async Task ConsumeNewTurnCalculation()
    {
        await agentManager.CommitChangesAsync();
        Debug.Log("Agent Consume");
        await gameManager.CommitChangesAsync();
        Debug.Log("Game Consume");
        gamesToUpdate.Clear();
        foreach (var game in allGames)
        {
            game.SalesCalculation(turn);
            game.GameSalesThisWeek = 0;
        }
    }
}
