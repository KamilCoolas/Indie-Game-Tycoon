using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button CloseBtn;
    public Canvas CanvasToClose;
    void Start()
    {
        Button btn = CloseBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        CanvasToClose.enabled = false;
    }
}
