using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateNewGameButton : MonoBehaviour
{
    public Button CreateNewButton;
    public Canvas CreateCanvas;
    public TMP_InputField Title;
    void Start()
    {
        Button btn = CreateNewButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        Title.text = "";
        CreateCanvas.enabled = true;
        
    }
    void Update()
    {
        if (GameLogic.isGameInProgress)
        {
            CreateNewButton.interactable = false;
        }
        else CreateNewButton.interactable = true;
    }
}