using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using System;

public class NewGameValuation : MonoBehaviour
{
    public TextAsset genreList;
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
    string[,] genreListArray;
    //    {
    //          "Arcade",
    //          "Endless Runner",
    //          "Platformer",
    //          "RPG"
    //    };
    Dictionary<string, int> genre = new();


    // Adding elements

    private void Start()
    {
        genreListArray = ParseTextAsset(genreList);
        //foreach (string t in genreListArray)
        for (int i = 0; i < genreListArray.GetLength(0); i++)
        {
            genreDropdown.options.Add(new TMP_Dropdown.OptionData() { text = genreListArray[i,0] });
            genre.Add(genreListArray[i, 0], Convert.ToInt32(genreListArray[i, 1]));
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
    public string[,] ParseTextAsset(TextAsset ft)
    {
        string fs = ft.text;
        string[] fLines = Regex.Split(fs, "\r\n");
        List<string> lines = new List<string>();
        foreach (string t in fLines)
        {
            if (t != "")
            {
                lines.Add(t);
            }
        }
        string[,] values = new string[lines.Count, 2];
        int k = 0;
        for (int i = 0; i < lines.Count; i++)
        {
                string valueLine = lines[i];
                string[] splitet = Regex.Split(valueLine, ","); // your splitter here
                int j = 0;
                foreach (string t in splitet)
                {
                    values[k,j] = t;
                    j++;
                }
                k++;
            }
        return values;
    }
}
