using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;


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
            Email = "NaN"; // Not available
            SocialSecurityNumber = scn;

            accounts = new List<Account>();
            transactionHistory = new List<Transaction>();
        }

        public string CreateXmlObject() 
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(sw))
                {
                    xmlWriter.WriteStartDocument();

                    xmlWriter.WriteStartElement("User");
                    xmlWriter.WriteStartElement("UserInfo");
                    xmlWriter.WriteElementString("FirstName", FirstName);
                    xmlWriter.WriteElementString("LastName", LastName);
                    xmlWriter.WriteElementString("Email", Email);
                    xmlWriter.WriteElementString("SocialSecurityNumber", SocialSecurityNumber);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
             
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                }

                //Debug.WriteLine(sw.ToString());
                return sw.ToString();
            }
        }
    }
}
