using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateButton : MonoBehaviour
{
    public Button CreateBtn;
    public Canvas CreateCanvas;
    public TMP_Text genreDropdown;
    public TMP_Text themeDropdown;
    public TMP_Text graphicsDropdown;
    public TMP_InputField title;
    void Start()
    {
        Button btn = CreateBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameLogic.gameInProgress = new string[] { title.text, genreDropdown.text, themeDropdown.text, graphicsDropdown.text, NewGameValuation.estDurValue.ToString(), NewGameValuation.estCostValue.ToString() };
        GameLogic.money -= NewGameValuation.estCostValue;
        GameLogic.isGameInProgress = true;
        CreateCanvas.enabled = false;
    }
}
