using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameReleased : MonoBehaviour
{
    public Canvas GameRel;
    public TMP_Text Title;
    public TMP_Text Rev1;
    public TMP_Text Rev2;
    public TMP_Text Rev3;
    public TMP_Text Rev4;
    public TMP_Text Avg;
    void Update()
    {
        if (GameLogic.isGameReleased)
        {
            GameRel.enabled = true;
            Title.text = "Title: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 1];
            Rev1.text = "Review 1: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 6] + "/10";
            Rev2.text = "Review 2: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 7] + "/10";
            Rev3.text = "Review 3: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 8] + "/10";
            Rev4.text = "Review 4: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 9] + "/10";
            Avg.text = "Avarage Score: " + GameLogic.gamesReleased[GameLogic.numberOfGamesIndex - 1, 10] + "/10";
        }
    }
}
