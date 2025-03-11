using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class UpdateMoneyTurn
    {
        public static void UpdateMoneyTurnText()
        {
            TMP_Text mText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();
            TMP_Text tText = GameObject.Find("TurnText").GetComponent<TMP_Text>();
            mText.text = "Money: " + GameLogic.money + "$";
            tText.text = "Turn: " + GameLogic.turn;
        }
        public static void UpdateMoney(int moneyToAdd)
        {
            GameLogic.money += moneyToAdd;
            UpdateMoneyTurnText();
        }
    }
}
