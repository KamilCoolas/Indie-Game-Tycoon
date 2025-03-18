using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class NextTurnButton : MonoBehaviour
{
    public Button NextTurnBtn;
    public Button CreateNewGameButton;
    public TMP_Dropdown GameDrop;
    public TMP_Text moneyText;
    public TMP_Text turnText;
    public Canvas GameRel;
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
        GameLogic.money -= GameLogic.costPerTurn;
        logic.UpdateMoneyTurnText();
        if (GameLogic.gamesReleased[0,0] != null) logic.SalesCalculation(GameLogic.gamesReleased);
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
            for (int i = 0; i < 6; i++)
            {
                if (i == 0) GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, i] = (GameLogic.numberOfGamesIndex + 1).ToString();
                else GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, i] = GameLogic.gameInProgress[i - 1];
            }
            logic.GameQuality();
            GameDrop.options.Add(new TMP_Dropdown.OptionData() { text = GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, 0] + "." + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex, 1] });
            GameLogic.numberOfGamesIndex += 1;
            GameLogic.gameInProgress = new string[] { };
            GameLogic.isGameReleased = true;
            GameLogic.isGameInProgress = false;
            GameRel.GetComponent<GameReleased>().AssignReviewText();
            CreateNewGameButton.interactable = true;
        }
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
