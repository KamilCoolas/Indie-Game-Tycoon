using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseButton1 : MonoBehaviour
{
    public Button CloseBtn;
    public Canvas GameRelCanvas;
    void Start()
    {
        Button btn = CloseBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameLogic.isGameReleased = false;
        GameRelCanvas.enabled = false;
    }
}
