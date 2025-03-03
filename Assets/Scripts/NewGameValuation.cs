using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameValuation : MonoBehaviour
{
    public TMP_Text genreDropdown;
    public TMP_Text themeDropdown;
    public TMP_Text graphicsDropdown;
    public TMP_Text estDur;
    public TMP_Text estCost;
    int genreValue;
    int themeValue;
    int graphicValue;
    public static int estDurValue;
    public static int estCostValue;
    void Update()
    {
        switch (genreDropdown.text)
        {
            case "Arcade":
                genreValue = 1;
                break;
            case "Endless Runner":
                genreValue = 1;
                break;
            case "Platformer":
                genreValue = 2;
                break;
        }
        switch (themeDropdown.text)
        {
            case "Space":
                themeValue = 1;
                break;
            case "Pirates":
                themeValue = 1;
                break;
            case "Fantasy":
                themeValue = 1;
                break;
        }
        switch (graphicsDropdown.text)
        {
            case "2D":
                graphicValue = 1;
                break;
            case "2.5D":
                graphicValue = 2;
                break;
            case "3D":
                graphicValue = 3;
                break;
        }
        estDurValue = genreValue + themeValue + graphicValue;
        estCostValue = (100 * genreValue + 100 * themeValue + 100 * graphicValue)*estDurValue;
        estDur.text = "Estimated Duration: " + estDurValue + " turn(s)";
        estCost.text = "Estimated Cost: " + estCostValue + "$";
    }
}
