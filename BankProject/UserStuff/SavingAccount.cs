using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    class SavingAccount : Account
    {
        public static double InterestRate { get; private set; }
        private static int internID = 0;

        public SavingAccount(double balance) : base(balance, internID.ToString())
        {
            internID++;
        }

        public SavingAccount(double balance, string id) : base(balance, id)
        {
            internID++;
            InterestRate = 1.0;
        }

        public override string ToString()
        {
            return "Savings account " + internID;
        }

        public override void Withdraw(double c)
        {
            if (c < 0)
                throw new ArgumentException("Cannot use a negative value");

            if ((Balance - c) < 0)
            {
                throw new BalanceException("Cannot withdraw more than you have.");

            }

            Balance -= (c * (1.0 - (InterestRate / 100)));

        }

    }
}
