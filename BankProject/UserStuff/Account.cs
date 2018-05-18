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

        public Account()
        {

        }

        public Account(double balance)
        {
            Balance = balance;
        }

        public void Deposit(double c)
        {
            if (c < 0)
                throw new ArgumentException("Cannot use a negative value");

            Balance += c;
        }

        public void Withdraw(double c)
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
