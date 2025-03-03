using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button CloseBtn;
    public Canvas CreateCanvas;
    void Start()
    {
        Button btn = CloseBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        CreateCanvas.enabled = false;
    }
}
