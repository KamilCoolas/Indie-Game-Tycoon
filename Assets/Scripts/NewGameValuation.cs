using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NewGameValuation : MonoBehaviour
{
    public TextAsset genreList;
    public TMP_Dropdown genreDropdown;
    public TextAsset themeList;
    public TMP_Dropdown themeDropdown;
    public TextAsset graphicList;
    public TMP_Dropdown graphicDropdown;
    public TMP_Text estDur;
    public TMP_Text estCost;
    public Button CreateButton;
    public TMP_InputField TitleInput;
    int genreValue;
    int themeValue;
    int graphicValue;
    public static int estDurValue;
    public static int estCostValue;
    string[,] genreListArray;
    Dictionary<string, int> genreId = new();
    string[,] themeListArray;
    Dictionary<string, int> themeId = new();
    string[,] graphicListArray;
    Dictionary<string, int> graphicId = new();
    private void Start()
    {
        LoadingDropdownValues("genre", genreList, genreDropdown, genreListArray, genreId);
        LoadingDropdownValues("theme", themeList, themeDropdown, themeListArray, themeId);
        LoadingDropdownValues("graphic", graphicList, graphicDropdown, graphicListArray, graphicId);
        TitleInput.onValueChanged.AddListener(delegate { RecalculateValues(); });
    }
    void Update()
    {
    }
    
    void RecalculateValues()
    {
        if (genreValue != 0 && themeValue != 0 && graphicValue != 0 && TitleInput.text != "")
        {
            estDurValue = genreValue + themeValue + graphicValue;
            estCostValue = (100 * genreValue + 100 * themeValue + 100 * graphicValue) * estDurValue;
            estDur.text = "Estimated Duration: " + estDurValue + " turn(s)";
            estCost.text = "Estimated Cost: " + estCostValue + "$";
            CreateButton.interactable = true;
        }
        else CreateButton.interactable = false;
    }
    void LoadingDropdownValues(string type, TextAsset file, TMP_Dropdown dropdown, string[,] ListArray, Dictionary<string, int> Id)
    {
        ListArray = Assets.Scripts.CsvFileParsing.ParseTextAsset(file);
        for (int i = 0; i < ListArray.GetLength(0); i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = ListArray[i, 1] });
            Id.Add(ListArray[i, 1], Convert.ToInt32(ListArray[i, 0]));
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(type, dropdown, ListArray, Id); });
    }
    void DropdownValueChanged(string type, TMP_Dropdown change, string[,] ListArray, Dictionary<string, int> Id)
    {
        int value = Convert.ToInt32(ListArray[Id[change.captionText.text], 2]);
        switch (type)
        {
            case "genre":
                genreValue = value;
                break;
            case "theme":
                themeValue = value;
                break;
            case "graphic":
                graphicValue = value;
                break;
        }
        RecalculateValues();
    }
}
