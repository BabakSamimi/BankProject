using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using Common;

namespace BankProject.UserStuff
{ 
    class User
    {
        public  List<Account> Accounts { get; private set; }
        public  List<Transaction> TransactionHistory { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string SocialSecurityNumber { get; private set; } // YYYYMMDD-XXXX
        private string password;
        

        public User(string firstName, string lastName, string email, string ssn, string pword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SocialSecurityNumber = ssn;
            password = Crypto.GetSHA256FromString(pword);

            Accounts = new List<Account>();
            TransactionHistory = new List<Transaction>();


        }

        public User(string firstName, string lastName, string ssn, string pword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = "NaN"; // Not available
            SocialSecurityNumber = ssn;
            password = Crypto.GetSHA256FromString(pword);

            Accounts = new List<Account>();
            TransactionHistory = new List<Transaction>();
        }

        // Used to reconstruct an User-object from an XML-file
        public User(string firstName, string lastName, string ssn, string email, List<Account> accList, List<Transaction> transactionList)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email; 
            SocialSecurityNumber = ssn;

            Accounts = accList;
            TransactionHistory = transactionList;
        }

        public static User CreateObjectFromXml(byte[] xml)
        {
            // The byte array will be encoded in UTF8
            string xmlString = Encoding.UTF8.GetString(xml);

            string fName;
            string lName;
            string securityNumber;
            string mail;

            List<Transaction> transactionList = new List<Transaction>();
            List<Account> accountList = new List<Account>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            // Get all the nodes so we can create a User-object for the client
            XmlNode firstNameNode = doc.SelectSingleNode("/User/UserInfo/FirstName");
            XmlNode lastNameNode = doc.SelectSingleNode("/User/UserInfo/LastName");
            XmlNode ssnNode = doc.SelectSingleNode("/User/UserInfo/SocialSecurityNumber");
            XmlNode mailNode = doc.SelectSingleNode("/User/UserInfo/Email");
            XmlNodeList salaryAccountsNodes = doc.SelectNodes("/User/Accounts/SalaryAccounts");
            XmlNodeList savingsAccountsNodes = doc.SelectNodes("/User/Accounts/SavingsAccounts");
            XmlNodeList transactionsNode = doc.SelectNodes("/User/Transactions/Transaction");

            fName = firstNameNode.InnerText;
            lName = lastNameNode.InnerText;
            securityNumber = ssnNode.InnerText;
            mail = mailNode.InnerText;

            foreach(XmlNode node in doc.SelectNodes("/User/Accounts/SalaryAccounts/SalaryAccount"))
            {
                accountList.Add(new SalaryAccount(Double.Parse(node.InnerText), node.Attributes["ID"].Value));
            }

            foreach (XmlNode node in doc.SelectNodes("/User/Accounts/SavingsAccounts/SavingsAccount"))
            {
                accountList.Add(new SavingAccount(Double.Parse(node.InnerText), node.Attributes["ID"].Value));
            }

            string from = null;
            string to = null;
            string amount = null;
            DateTime date = DateTime.MinValue;

            foreach (XmlNode node in transactionsNode)
            {

                from = node["From"].InnerText;
                to = node["To"].InnerText;
                amount = node["Amount"].InnerText;
                date = DateTime.ParseExact(node.Attributes["Date"].Value, "yyyy-MM-dd HH:mm:ss", null);

                transactionList.Add(new Transaction(from, to, amount, date));
            }

            return new User(fName, lName, securityNumber, mail, accountList, transactionList);

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
                    xmlWriter.WriteElementString("Password", password);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
             
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }

                Debug.WriteLine(sw.ToString());
                return sw.ToString();
            }
        }
    }
}
