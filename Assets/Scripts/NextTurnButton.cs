using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButton : MonoBehaviour
{
    public Button NextTurnBtn;
    void Start()
    {
        Button btn = NextTurnBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameLogic.turn++;
        if (GameLogic.isGameInProgress)
        {
            GameLogic.gameInProgress[4] = (Convert.ToInt32(GameLogic.gameInProgress[4]) - 1).ToString();
            int genreValue = GameLogic.GameInProgressGenre();
            int themeValue = GameLogic.GameInProgressTheme();
            int graphicValue = GameLogic.GameInProgressGraphic();
            ExpGain(genreValue);
            ExpGain(themeValue);
            ExpGain(graphicValue);
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
