using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameValuation : MonoBehaviour
{
    public TMP_Text themeDropdown;
    public TMP_Text graphicsDropdown;
    public TMP_Text estDur;
    public TMP_Text estCost;
    public TMP_Dropdown genreDropdown;
    int genreValue;
    int themeValue;
    int graphicValue;
    public static int estDurValue;
    public static int estCostValue;
    string[] genreList =
        {
              "Arcade",
              "Endless Runner",
              "Platformer",
              "RPG"
        };
    Dictionary<string, int> genre = new Dictionary<string, int>();

    // Adding elements

    private void Start()
    {
        genre.Add("Arcade", 1);
        genre.Add("Endless Runner", 1);
        genre.Add("Platformer", 2);
        genre.Add("RPG", 4);
        foreach (string t in genreList)
        {
            genreDropdown.options.Add(new TMP_Dropdown.OptionData() { text = t });
        }
        genreDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(genreDropdown); });
    }
    void DropdownValueChanged(TMP_Dropdown change)
    {
        genreValue = genre[change.captionText.text];
    }
    void Update()
    {
        
        //switch (genreDropdown.captionText.text)
        //{
        //    case "Arcade":
        //        genreValue = 1;
        //        break;
        //    case "Endless Runner":
        //        genreValue = 1;
        //        break;
        //    case "Platformer":
        //        genreValue = 2;
        //        break;
        //}
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
