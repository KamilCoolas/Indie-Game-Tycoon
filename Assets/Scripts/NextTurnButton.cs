using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class NextTurnButton : MonoBehaviour
{
    public Button NextTurnBtn;
    public Button CreateNewGameButton;
    public Canvas GameRel;
    public TMP_Dropdown GameDrop;
    public TMP_Text moneyText;
    public TMP_Text turnText;
    public GameObject GameLogicObject;
    void Start()
    {
        GameLogic logic = GameLogicObject.GetComponent<GameLogic>();
        Button btn = NextTurnBtn.GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClick(logic); });
    }
    public void OnClick(GameLogic logic)
    {
        GameLogic.turn++;
        logic.AgentsPlayingGames();
        logic.UpdateMoney(-GameLogic.costPerTurn, "Maintenance");
        logic.UpdateMoneyTurnText();
        logic.GenerateIncome();
        if (GameLogic.isGameInProgress)
        {
            GameLogic.gameInProgress[5] = (Convert.ToInt32(GameLogic.gameInProgress[5]) - 1).ToString();
            for (int i = 1; i <= 3; i++)
            {
                ExpGain(GameLogic.atributeId[GameLogic.gameInProgress[i]]);
            }
        }
        if (GameLogic.isGameInProgress && Convert.ToInt32(GameLogic.gameInProgress[5]) <= 0)
        {
            logic.GameReleased();
        }
        logic.IncomeCostTextGenerator();
    }
    public void ExpGain (int atributeValue)
    {
        string[] aAttribute = GameLogic.employees[0, atributeValue].Split(".");
        int expGenre = (10 - Convert.ToInt32(aAttribute[0])) * 5;
        int level = Convert.ToInt32(aAttribute[0]);
        int percent = (Convert.ToInt32(aAttribute[1]) + expGenre);
        if (percent >= 100)
        {
            level += 1;
            percent -= 100;
        }
        GameLogic.employees[0, atributeValue] = level.ToString() + "." + percent.ToString();
    }
}
