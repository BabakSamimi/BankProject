using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    class Transaction
    {
        public DateTime Date { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Amount { get; private set; }

        public Transaction()
        {

        }

        public Transaction(string from, string to, string amount, DateTime date)
        {
            From = from;
            To = to;
            Amount = amount;
            Date = date;
        }

    }
}
