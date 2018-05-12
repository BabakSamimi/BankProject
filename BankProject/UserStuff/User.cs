using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BankProject.UserStuff
{
    class User
    {
        private List<Account> accounts;
        private List<Transaction> transactionHistory;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string SocialSecurityNumber { get; private set; } // YYYYMMDD-XXXX
        private string password;
        

        public User(string firstName, string lastName, string email, string scn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SocialSecurityNumber = scn;

            accounts = new List<Account>();
            transactionHistory = new List<Transaction>();

        }

        public User(string firstName, string lastName, string scn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = "NaN";
            SocialSecurityNumber = scn;

            accounts = new List<Account>();
            transactionHistory = new List<Transaction>();
        }


    }
}
