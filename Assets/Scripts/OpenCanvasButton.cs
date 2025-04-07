using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenCanvasButton : MonoBehaviour
{
    public Button Button;
    public Canvas Canvas;
    public TMP_InputField TextToDefault;
    public TMP_Dropdown DropdownToDefault;
    void Start()
    {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if (TextToDefault != null) TextToDefault.text = "";
        if (DropdownToDefault != null) DropdownToDefault.value = 0;
        Canvas.enabled = true;
    }
}
