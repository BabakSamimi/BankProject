using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    interface IAccount
    {
        double Balance { get; set; }
        void Withdraw(double c);
        void Deposit(double c);

    }
}
