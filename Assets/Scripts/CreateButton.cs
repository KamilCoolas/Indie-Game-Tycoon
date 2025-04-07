using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public Button CreateBtn;
    public Button CreateNewGameButton;
    public Canvas CreateCanvas;
    public TMP_Text genreDropdown;
    public TMP_Text themeDropdown;
    public TMP_Text graphicsDropdown;
    public TMP_InputField title;
    public GameObject GameLogicObject;
    void Start()
    {
        GameLogic logic = GameLogicObject.GetComponent<GameLogic>();
        Button btn = CreateBtn.GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClick(logic); });
    }
    public void OnClick(GameLogic logic)
    {
        GameLogic.gameInProgress = new string[] { title.text, genreDropdown.text, themeDropdown.text, graphicsDropdown.text, NewGameValuation.estCostValue.ToString(), NewGameValuation.estDurValue.ToString() };
        logic.UpdateMoney(-NewGameValuation.estCostValue, title.text);
        logic.IncomeCostTextGenerator();
        GameLogic.isGameInProgress = true;
        CreateCanvas.enabled = false;
        CreateNewGameButton.interactable = false;
        logic.UpdateMoneyTurnText();
    }
}
