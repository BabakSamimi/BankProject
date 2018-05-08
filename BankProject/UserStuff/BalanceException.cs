using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    class BalanceException : Exception
    {
        public BalanceException()
        {

        }

        public BalanceException(string message) : base (message)
        {

        }

        public BalanceException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
