using System;
using TMPro;
using UnityEngine;

public class DropdownGames : MonoBehaviour
{
    public TMP_Dropdown DropGames;
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
    void Start()
    {
        NullOptions();
        GameLogic logic = GameLogicObject.GetComponent<GameLogic>();
        DropGames.onValueChanged.AddListener(delegate { OnChange(logic); });
    }

    public void OnChange(GameLogic logic)
    {
        if (dropdownText.text != "-")
        {
            string[] titleId = dropdownText.text.Split(".");
            int GameId = Convert.ToInt32(titleId[0]) - 1;
            Title.text = "Title: " + logic.gamesReleased[GameId].Title;
            Genre.text = "Genre: " + logic.gamesReleased[GameId].Genre;
            Theme.text = "Theme: " + logic.gamesReleased[GameId].Theme;
            Graphics.text = "Graphics: " + logic.gamesReleased[GameId].Graphics;
            ReleasedTurn.text = "Released Turn: " + logic.gamesReleased[GameId].ReleasedTurn;
            AvgScore.text = "Avg. Score: " + logic.gamesReleased[GameId].GetAvarageScore();
            SoldThisWeek.text = "Sold This Week: " + logic.gamesReleased[GameId].GetSalesForTurn(GameLogic.turn); ;
            IncomeThisWeek.text = "Income This Week: " + logic.gamesReleased[GameId].GetIncomeForTurn(GameLogic.turn); ;
            SoldOverall.text = "Sold Overall: " + logic.gamesReleased[GameId].GetOverallSales(); ;
            IncomeOverall.text = "Income Overall: " + logic.gamesReleased[GameId].GetOverallIncome(); ;
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
    }
}