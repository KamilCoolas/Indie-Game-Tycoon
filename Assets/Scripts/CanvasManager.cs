using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas developmentCanvas;
    public Canvas allGameCanvas;
    private List<Canvas> _listOfAllCanvas = new List<Canvas>();
    void Start()
    {
        _listOfAllCanvas.Add(developmentCanvas);
        _listOfAllCanvas.Add(allGameCanvas);
    }
    public void CloseAllCanvas()
    {
        foreach (Canvas canvas in _listOfAllCanvas)
        {
            canvas.enabled = false;
        }
    }
}
