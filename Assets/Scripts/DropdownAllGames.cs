using System;
using TMPro;
using UnityEngine;

public class DropdownAllGames : MonoBehaviour
{
    public TMP_Dropdown DropAllGames;
    public GameObject GameLogicObject;
    public TMP_Text dropdownText;
    public TMP_Text Title;
    public TMP_Text Genre;
    public TMP_Text Theme;
    public TMP_Text Graphics;
    public TMP_Text ReleasedTurn;
    public TMP_Text AvgScore;
    public TMP_Text SoldThisWeek;
    public TMP_Text IncomeThisWeek;
    public TMP_Text SoldOverall;
    public TMP_Text IncomeOverall;
    public TMP_Text Agents;
    void Start()
    {
        NullOptions();
        GameLogic logic = GameLogicObject.GetComponent<GameLogic>();
        DropAllGames.onValueChanged.AddListener(delegate { OnChange(logic); });
    }

    public void OnChange(GameLogic logic)
    {
        if (dropdownText.text != "-")
        {
            string[] titleId = dropdownText.text.Split(".");
            int GameId = Convert.ToInt32(titleId[0]) - 1;
            Title.text = "Title: " + logic.allGames[GameId].Title;
            Genre.text = "Genre: " + logic.allGames[GameId].Genre;
            Theme.text = "Theme: " + logic.allGames[GameId].Theme;
            Graphics.text = "Graphics: " + logic.allGames[GameId].Graphics;
            ReleasedTurn.text = "Released Turn: " + logic.allGames[GameId].ReleasedTurn;
            AvgScore.text = "Avg. Score: " + logic.allGames[GameId].GetAvarageScore();
            SoldThisWeek.text = "Sold This Week: " + logic.allGames[GameId].GetSalesForTurn(GameLogic.turn);
            IncomeThisWeek.text = "Income This Week: " + logic.allGames[GameId].GetIncomeForTurn(GameLogic.turn);
            SoldOverall.text = "Sold Overall: " + logic.allGames[GameId].GetOverallSales();
            IncomeOverall.text = "Income Overall: " + logic.allGames[GameId].GetOverallIncome();
            Agents.text = "Agents: " + logic.allGames[GameId].Agents;
        }
        else
        {
            NullOptions();
        }
    }
    private void NullOptions()
    {
        Title.text = "Title: -";
        Genre.text = "Genre: -";
        Theme.text = "Theme: -";
        Graphics.text = "Graphics: -";
        ReleasedTurn.text = "Released Turn: -";
        AvgScore.text = "Avg. Score: -";
        SoldThisWeek.text = "Sold This Week: -";
        IncomeThisWeek.text = "Income This Week: -";
        SoldOverall.text = "Sold Overall: -";
        IncomeOverall.text = "Income Overall: -";
        Agents.text = "Agents: -";
    }
}