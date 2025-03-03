using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReleasedGamesButton : MonoBehaviour
{
    public Button ReleasedGame;
    public Canvas ReleasedGamesCanvas;
    void Start()
    {
        Button btn = ReleasedGame.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        ReleasedGamesCanvas.enabled = true;
    }
}