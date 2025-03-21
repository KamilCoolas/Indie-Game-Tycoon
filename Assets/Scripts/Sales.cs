using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public class Sales
    {
        private int turn;
        private int soldThisWeek;
        private int price;
        private int incomeThisWeek;
        public Sales(int turn, int soldThisWeek, int price)
        {
            this.turn = turn;
            this.soldThisWeek = soldThisWeek;
            this.price = price;
            incomeThisWeek = soldThisWeek * price;
        }
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }
        public int SoldThisWeek
        {
            get { return soldThisWeek; }
            set { soldThisWeek = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public int IncomeThisWeek
        {
            get { return incomeThisWeek; }
            set { incomeThisWeek = value; }
        }
    }
}
