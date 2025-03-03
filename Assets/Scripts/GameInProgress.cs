using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInProgress : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text genre;
    public TMP_Text theme;
    public TMP_Text turnLeft;
    public Canvas gameInPro;

    void Update()
    {
        if (GameLogic.isGameInProgress)
        {
            gameInPro.enabled = true;
            title.text = GameLogic.gameInProgress[0];
            genre.text = GameLogic.gameInProgress[1];
            theme.text = GameLogic.gameInProgress[2];
            turnLeft.text = "Turn Left: " + GameLogic.gameInProgress[4];
        }
        else gameInPro.enabled = false;
    }
}
