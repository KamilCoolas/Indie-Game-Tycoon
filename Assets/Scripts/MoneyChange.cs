using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assets.Scripts
{
    internal class MoneyChange
    {
        private int amount;
        private string source;
        public MoneyChange(int amountToAdd, string incomeSource)
        {
            amount = amountToAdd;
            source = incomeSource;
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
