using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    [Serializable]
    class Transaction
    {
        private string from;
        private string to;
        private string amount;

        public Transaction()
        {

        }

        public Transaction(string f, string t, string a)
        {
            from = f;
            to = t;
            amount = a;
        }

    }
}
