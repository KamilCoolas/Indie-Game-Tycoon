using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownGames : MonoBehaviour
{
    public TMP_Text dropdownText;
    public TMP_Text Title;
    public TMP_Text Genre;
    public TMP_Text Theme;
    public TMP_Text Graphics;
    public TMP_Text ReleasedTurn;
    public TMP_Text AvgScore;
    void Update()
    {
        if (dropdownText.text != "-")
        {
            string[] titleId = dropdownText.text.Split(".");
            int GameId = Convert.ToInt32(titleId[0]) - 1;
            Title.text = "Title: " + GameLogic.gamesReleased[GameId, 1];
            Genre.text = "Genre: " + GameLogic.gamesReleased[GameId, 2];
            Theme.text = "Theme: " + GameLogic.gamesReleased[GameId, 3];
            Graphics.text = "Graphics: " + GameLogic.gamesReleased[GameId, 4];
            ReleasedTurn.text = "Released Turn: " + GameLogic.gamesReleased[GameId, 11];
            AvgScore.text = "Avg. Score: " + GameLogic.gamesReleased[GameId, 10];
        }
        else
        {
            Title.text = "Title: -";
            Genre.text = "Genre: -";
            Theme.text = "Theme: -";
            Graphics.text = "Graphics: -";
            ReleasedTurn.text = "Released Turn: -";
            AvgScore.text = "Avg. Score: -";
        }
    }
}