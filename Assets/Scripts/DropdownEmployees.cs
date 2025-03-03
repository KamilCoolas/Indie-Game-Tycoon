using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownEmployees : MonoBehaviour
{
    public TMP_Text dropdownText;
    public TMP_Text Arcade;
    public TMP_Text EndRun;
    public TMP_Text Platf;
    public TMP_Text Space;
    public TMP_Text Pirates;
    public TMP_Text Fantasy;
    public TMP_Text d2;
    public TMP_Text d25;
    public TMP_Text d3;
    public Image IArcade;
    public Image IEndRun;
    public Image IPlatf;
    public Image ISpace;
    public Image IPirates;
    public Image IFantasy;
    public Image Id2;
    public Image Id25;
    public Image Id3;

    void Update()
    {
        if (dropdownText.text != "-")
        {
            string[] empName = dropdownText.text.Split(".");
            int empId = Convert.ToInt32(empName[0]) - 1;
            string[] aArcade = GameLogic.employees[empId, 1].Split(".");
            string[] aEndRun = GameLogic.employees[empId, 2].Split(".");
            string[] aPlatf = GameLogic.employees[empId, 3].Split(".");
            string[] aSpace = GameLogic.employees[empId, 4].Split(".");
            string[] aPirates = GameLogic.employees[empId, 5].Split(".");
            string[] aFantasy = GameLogic.employees[empId, 6].Split(".");
            string[] ad2 = GameLogic.employees[empId, 7].Split(".");
            string[] ad25 = GameLogic.employees[empId, 8].Split(".");
            string[] ad3 = GameLogic.employees[empId, 9].Split(".");
            Arcade.text = "lvl " + aArcade[0];
            EndRun.text = "lvl " + aEndRun[0];
            Platf.text = "lvl " + aPlatf[0];
            Space.text = "lvl " + aSpace[0];
            Pirates.text = "lvl " + aPirates[0];
            Fantasy.text = "lvl " + aFantasy[0];
            d2.text = "lvl " + ad2[0];
            d25.text = "lvl " + ad25[0];
            d3.text = "lvl " + ad3[0];
            IArcade.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aArcade[1]), 20);
            IEndRun.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aEndRun[1]), 20);
            IPlatf.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aPlatf[1]), 20);
            ISpace.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aSpace[1]), 20);
            IPirates.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aPirates[1]), 20);
            IFantasy.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(aFantasy[1]), 20);
            Id2.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(ad2[1]), 20);
            Id25.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(ad25[1]), 20);
            Id3.rectTransform.sizeDelta = new Vector2(Convert.ToInt32(ad3[1]), 20);
        }
        else
        {
            Arcade.text = "lvl -";
            EndRun.text = "lvl -";
            Platf.text = "lvl -";
            Space.text = "lvl -";
            Pirates.text = "lvl -";
            Fantasy.text = "lvl -";
            d2.text = "lvl -";
            d25.text = "lvl -";
            d3.text = "lvl -";
            IArcade.rectTransform.sizeDelta = new Vector2(0, 20);
            IEndRun.rectTransform.sizeDelta = new Vector2(0, 20);
            IPlatf.rectTransform.sizeDelta = new Vector2(0, 20);
            ISpace.rectTransform.sizeDelta = new Vector2(0, 20);
            IPirates.rectTransform.sizeDelta = new Vector2(0, 20);
            IFantasy.rectTransform.sizeDelta = new Vector2(0, 20);
            Id2.rectTransform.sizeDelta = new Vector2(0, 20);
            Id25.rectTransform.sizeDelta = new Vector2(0, 20);
            Id3.rectTransform.sizeDelta = new Vector2(0, 20);
        }
    }
}