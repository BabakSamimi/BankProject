/*
 * Created by Babak Samimi. 2018.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    class Account : IAccount
    {
        public double Balance { get; set; }
        public string AccountID { get; protected set; }

        public Account()
        {
            Balance = 0.0;
        }

        public Account(double balance, string id)
        {
            Balance = balance;
            AccountID = id;
        }

        public void Deposit(double c)
        {
            if (c < 0)
                throw new ArgumentException("Cannot use a negative value");

            Balance += c;
        }

        public virtual void Withdraw(double c)
        {
            if (c < 0)
                throw new ArgumentException("Cannot use a negative value");

            if ((Balance - c) < 0)
            {
                throw new BalanceException("Cannot withdraw more than you have.");
                
            }

            Balance -= c;
        }

    }
}
