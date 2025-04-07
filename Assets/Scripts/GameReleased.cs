using Assets.Scripts;
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
    }
    public void AssignReviewText(Game newGame)
    {
        GameRel.enabled = true;
        Title.text = "Title: " + newGame.Title;
        Rev1.text = "Review 1: " + newGame.ReviewList[0].Rate + "/10";
        Rev2.text = "Review 2: " + newGame.ReviewList[1].Rate + "/10";
        Rev3.text = "Review 3: " + newGame.ReviewList[2].Rate + "/10";
        Rev4.text = "Review 4: " + newGame.ReviewList[3].Rate + "/10";
        Avg.text = "Avarage Score: " + newGame.GetAvarageScore() + "/10";
        GameLogic.isGameReleased = false;
    }
}
