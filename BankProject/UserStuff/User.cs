using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.UserStuff
{
    class User
    {
        private List<Account> accounts;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            email = Email;
        }



    }
}
