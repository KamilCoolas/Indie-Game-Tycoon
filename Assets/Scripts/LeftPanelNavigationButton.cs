using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanelNavigationButton : MonoBehaviour
{
    public Button Button;
    public Canvas Canvas;
    public Canvas MainCanvas;
    public TMP_InputField TextToDefault;
    public TMP_Dropdown DropdownToDefault;
    public string textShort;
    public string textLong;

    void Start()
    {
        Button.GetComponentInChildren<TMP_Text>().text = textShort;
        CanvasManager canvasManager = MainCanvas.GetComponent<CanvasManager>();
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClick(canvasManager); });
    }
    public void OnClick(CanvasManager canvasManager)
    {
        if (TextToDefault != null) TextToDefault.text = "";
        if (DropdownToDefault != null) DropdownToDefault.value = 0;
        canvasManager.CloseAllCanvas();
        Canvas.enabled = true;
    }
    public void OnEnter()
    {
        RectTransform rect = Button.GetComponent<RectTransform>();
        Button.GetComponentInChildren<TMP_Text>().text = textLong;
        rect.anchoredPosition = new Vector2(80.0f, rect.anchoredPosition.y);
        rect.sizeDelta = new Vector2(150.0f, 50.0f);
    }
    public void OnLeave()
    {
        RectTransform rect = Button.GetComponent<RectTransform>();
        Button.GetComponentInChildren<TMP_Text>().text = textShort;
        rect.anchoredPosition = new Vector2(30.0f, rect.anchoredPosition.y);
        rect.sizeDelta = new Vector2(50.0f, 50.0f);
    }
}
