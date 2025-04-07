using TMPro;
using UnityEngine;

public class GameInProgress : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text genreText;
    public TMP_Text themeText;
    public TMP_Text turnLeftText;
    public Canvas gameInPro;
    void Update()
    {
        if (GameLogic.isGameInProgress)
        {
            gameInPro.enabled = true;
            titleText.text = GameLogic.gameInProgress[0];
            genreText.text = GameLogic.gameInProgress[1];
            themeText.text = GameLogic.gameInProgress[2];
            turnLeftText.text = "Turn Left: " + GameLogic.gameInProgress[5];
        }
        else gameInPro.enabled = false;
    }

}
