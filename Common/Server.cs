using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Collections.Concurrent;

namespace Common
{
    public class Server
    {
        private List<Client> clients;
        private IPAddress address;
        private short port;
        private TcpListener tcpListener;

        private object dummy_lock = new object();
        private bool run_state = false;

        public bool Running // Make sure we lock threads using Running in order to change its value in a thread-safe manner
        {
            get
            {
                lock (dummy_lock) return run_state;
            }
            private set
            {
                lock (dummy_lock) run_state = value;
            }
        }

        private object client_lock = new object();
        public List<Client> GetClients
        {
            get
            {
                lock (client_lock) return clients;
            }
            set
            {
                lock (client_lock) clients = value;
            }
        }



        public Server(string address, short port)
        {
            this.address = IPAddress.Parse(address);
            this.port = port;

            clients = new List<Client>();
            tcpListener = new TcpListener(this.address, this.port);

        }

        public void Start()
        {
            Running = true;

            tcpListener.Start();

            // Handle incoming connections in a new thread
            new Thread(() => 
            {
                while (Running)
                {

                    try
                    {
                        Socket cliSocket = tcpListener.AcceptSocket(); // Await connection
                        byte[] temp_id = new byte[4];
                        cliSocket.Receive(temp_id);
                        // Create a new client object and set its received ID.
                        Client temp = new Client(BitConverter.ToInt32(temp_id, 0))
                        {
                            socket = cliSocket
                        };

                        //temp.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true); // Keep socket alive
                        //temp.socket.ReceiveTimeout = 2500; // This indicates that the server will wait 2,5 seconds when waiting for data to be received from the client
                        GetClients.Add(temp);

                        Debug.WriteLine("Client id: " + temp.Id);
                        Console.WriteLine(">> Client sucessfully connected with the ID: " + temp.Id);
                        
                    }
                    catch // Something weird happened, discard the connection
                    {
                        Console.WriteLine( "Incoming Socket couldn't connect.");
                        continue;
                    }
                }

            }).Start();

            Thread.Sleep(1000);
            UpdateClients();

 
        }

        public void Stop()
        {
            Running = false;
            tcpListener.Stop();
        }

        private bool IsConnected(Client cli)
        {
            if (cli.socket.Poll(1000, SelectMode.SelectRead) && (cli.socket.Available == 0))
            {
                return false;
            }
            else
                return true;
        }
       
        // Checks if we've received any data
        private bool DataReceived(ref Socket s, ref byte[] data, ref int length)
        {
            try
            {
                length = s.Receive(data);

                if (length == 0) // Receive() returns the size of the byte array, if it's 0 then we haven't received anything
                {
                    return false;
                }
                else return true;
            }
            catch
            {
                return false;
            }
            
        }

        private void SaveUserSSN(string ssn)
        {
            if (!File.Exists("SSNDB.xml"))
            {

                XmlWriter xmlWriter = XmlWriter.Create("SSNDB.xml", new XmlWriterSettings() { Indent = true, });

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Unique");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();
            }

            XmlDocument doc = new XmlDocument();

            doc.Load("SSNDB.xml");

            XmlNode uniqueNode = doc.SelectSingleNode("/Unique");
            XmlElement SSN = doc.CreateElement("SSN");
            SSN.InnerText = ssn;
            uniqueNode.AppendChild(SSN);

            doc.Save("SSNDB.xml");
            
        }

        private bool UserAlreadyExists(string ssn)
        {
            // We can verify if a user already exist through their Social Security Number (SSN) - Personnummer
           
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("UserData/" + ssn + ".xml"); // If we can load the file, then the SSN already exists
                Debug.WriteLine("User Exists.");
                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool PasswordIsEqual(string password, string ssn)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("UserData/" + ssn + ".xml");

            string hashedPassword = Crypto.GetSHA256FromString(password);

            XmlNode passwordNode = doc.SelectSingleNode("/User/UserInfo/Password");
            
            if(hashedPassword == passwordNode.InnerText)
            {
                Debug.WriteLine("Password is equal");
                return true;
            }
            return false;
        }

        private string GetUserXml(string ssn)
        {
            using (XmlReader xr = XmlReader.Create("UserData/" + ssn + ".xml", new XmlReaderSettings() { IgnoreWhitespace = true}))
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlWriter xw = XmlWriter.Create(sw))
                    {
                        xw.WriteNode(xr, false);
                    }
                    return sw.ToString();
                }
            }
        }

        // This method is used to create a new entry in the database
        private void CreateXmlDocument(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            string ssn = doc.SelectSingleNode("/User/UserInfo/SocialSecurityNumber").InnerText;

            XmlNode userNode = doc.SelectSingleNode("/User"); // Acquire the main node

            //Create a new element for all the accounts
            XmlElement accounts = doc.CreateElement("Accounts"); 
            userNode.AppendChild(accounts);

            XmlElement transactions = doc.CreateElement("Transactions");
            userNode.AppendChild(transactions);

            XmlNode transactionsNode = doc.SelectSingleNode("/User/Transactions");
            XmlElement singleTransaction = doc.CreateElement("Transaction");
            singleTransaction.SetAttribute("Date", DateTime.Now.ToString());
            XmlElement from;
            XmlElement to;
            XmlElement amount;

            from = doc.CreateElement("From");
            from.InnerText = "John Doe";

            to = doc.CreateElement("To");
            to.InnerText = "Mary Doe";

            amount = doc.CreateElement("Amount");
            amount.InnerText = "$100";
            singleTransaction.AppendChild(from);
            singleTransaction.AppendChild(to);
            singleTransaction.AppendChild(amount);

            transactionsNode.AppendChild(singleTransaction);
            userNode.AppendChild(transactionsNode);
            

            XmlNode accountNode = doc.SelectSingleNode("/User/Accounts");

            XmlElement salaryAccounts = doc.CreateElement("SalaryAccounts");
            accountNode.AppendChild(salaryAccounts);
            XmlElement savingsAccounts = doc.CreateElement("SavingsAccounts");
            accountNode.AppendChild(savingsAccounts);

            XmlNode savingsNode = doc.SelectSingleNode("/User/Accounts/SavingsAccounts");
            XmlElement firstSavingsAccount = doc.CreateElement("SavingsAccount");
            firstSavingsAccount.SetAttribute("ID", "1");
            firstSavingsAccount.InnerText = "0"; // 0 dollar
            savingsNode.AppendChild(firstSavingsAccount);

            XmlNode salaryNode = doc.SelectSingleNode("/User/Accounts/SalaryAccounts");
            XmlElement firstSalaryAccount = doc.CreateElement("SalaryAccount");
            firstSalaryAccount.SetAttribute("ID", "1");
            firstSalaryAccount.InnerText = "0"; // 0 dollar
            salaryNode.AppendChild(firstSalaryAccount);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("UserData/" + ssn + ".xml", settings);
            SaveUserSSN(ssn);

            doc.Save(writer);
        }

        // Update file when the user exits the clients
        private void UpdateUserXml(string xml, string ssn)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlDocument currentXml = new XmlDocument();
            currentXml.Load("UserData/" + ssn + ".xml");

            XmlNodeList salaryNodes = doc.SelectNodes("User/Accounts/SalaryAccounts/SalaryAccount");
            XmlNodeList savingsNodes = doc.SelectNodes("User/Accounts/SavingsAccounts/SavingsAccount");
            XmlNodeList transactionNodes = doc.SelectNodes("User/Transactions/Transaction");

            XmlNodeList currentSalaryNodes = currentXml.SelectNodes("User/Accounts/SalaryAccounts/SalaryAccount");
            XmlNodeList currentSavingsNodes = currentXml.SelectNodes("User/Accounts/SavingsAccounts/SavingsAccount");
            XmlNodeList currentTransactionNodes = currentXml.SelectNodes("User/Transactions/Transaction");

            // Remove current existing nodes
            for (int i = currentSalaryNodes.Count - 1; i >= 0; i--)
            {
                currentSalaryNodes[i].ParentNode.RemoveChild(currentSalaryNodes[i]);
            }

            for (int i = currentSavingsNodes.Count - 1; i >= 0; i--)
            {
                currentSavingsNodes[i].ParentNode.RemoveChild(currentSavingsNodes[i]);
            }
            for (int i = currentTransactionNodes.Count - 1; i >= 0; i--)
            {
                currentTransactionNodes[i].ParentNode.RemoveChild(currentTransactionNodes[i]);
            }

            // Insert the new data
            foreach (XmlNode node in salaryNodes)
            {
                currentXml.SelectSingleNode("User/Accounts/SalaryAccounts").AppendChild(currentXml.ImportNode(node, true));
            }

            foreach (XmlNode node in savingsNodes)
            {
                currentXml.SelectSingleNode("User/Accounts/SavingsAccounts").AppendChild(currentXml.ImportNode(node, true));
            }
            foreach (XmlNode node in transactionNodes)
            {
                currentXml.SelectSingleNode("User/Transactions").AppendChild(currentXml.ImportNode(node, true));
            }
            

            Debug.WriteLine("Updated." + currentXml.ToString());
            currentXml.Save("UserData/" + ssn + ".xml");

        }

        public void UpdateClients()
        {
            new Thread(() =>
            {
                while (Running)
                {
                    byte[] buffer;
                    try
                    {
                        // Check if there are any data to receive from clients
                        foreach (Client cli in GetClients)
                        {
                            buffer = new byte[2048];
                            int messageLength = 0;

                            if (IsConnected(cli) && DataReceived(ref cli.socket, ref buffer, ref messageLength))
                            {

                                Console.WriteLine("Received packet from: " + cli.Id + " with the packet ID: " + buffer[0]);

                                if (buffer[0] == 255) // Social Security Number-Check
                                {
                                    byte[] ssnArray = new byte[10];

                                    // Transfer relevant content of buffer to ssnArray
                                    for (int i = 0; i < 10; ++i)
                                    {

                                        ssnArray[i] = buffer[i + 1];

                                    }

                                    byte[] data = new byte[1];

                                    if (UserAlreadyExists(Encoding.UTF8.GetString(ssnArray).Replace("\0", "0"))) // we check if the ssn exists in the database
                                    {
                                        data[0] = 254; // Sending the value 254 tells the client that the SSN already exists.
                                    }
                                    else
                                    {
                                        data[0] = 253;
                                    }

                                    cli.socket.Send(data); // We send data to the client to tell the client that the Social Security Number they tried to pick is not valid.

                                }
                                else if (buffer[0] == 1) // Registration data
                                {
                                    byte[] userXml = new byte[buffer.Length - 1];

                                    // buffer 100, user 99
                                    for (int i = 0; i < userXml.Length; i++)
                                    {
                                        userXml[i] = buffer[i + 1];
                                    }

                                    string xmlString = Encoding.UTF8.GetString(userXml);

                                    CreateXmlDocument(xmlString);
                                }
                                else if (buffer[0] == 2) // Login data
                                {
                                    byte[] ssnArray = new byte[10];
                                    byte[] passwordArray = new byte[messageLength - 11];

                                    for(int i = 0; i < 10; ++i)
                                    {
                                        ssnArray[i] = buffer[i + 1];
                                    }

                                    for(int i = 0; i < passwordArray.Length; ++i)
                                    {
                                        passwordArray[i] = buffer[i + 11];
                                    }

                                    string ssnString = Encoding.UTF8.GetString(ssnArray).Replace("\0", "0"); // Replace trailing null-bytes to zero
                                    string password = Encoding.UTF8.GetString(passwordArray).Replace("\0", "0");

                                    byte[] data;

                                    if (UserAlreadyExists(ssnString))
                                    {
                                        if (PasswordIsEqual(password, ssnString))
                                        {
                                            byte[] xmlData = Encoding.UTF8.GetBytes(GetUserXml(ssnString));
                                            data = new byte[xmlData.Length + 1];
                                            xmlData.CopyTo(data, 1);
                                            data[0] = 49; // Value 49 will tell the verification was successful
                                            cli.socket.Send(data); // Send the data
                                        }
                                        else
                                        {
                                            data = new byte[1];
                                            data[0] = 50; // value 50 tells the client it's the wrong password
                                            cli.socket.Send(data);
                                        }
                                    }
                                    else
                                    {
                                        data = new byte[1];
                                        data[0] = 51; // Value 51 tells the client the SSN does not exist.
                                        cli.socket.Send(data);
                                    }
                                }
                                else if (buffer[0] == 10)
                                {
                                    byte[] xmlData = new byte[messageLength - 11];
                                    byte[] ssnArray = new byte[10];
                                    Array.Copy(buffer, 1, ssnArray, 0, 10);
                                    Array.Copy(buffer, 11, xmlData, 0, messageLength - 11);
                                    UpdateUserXml(Encoding.UTF8.GetString(xmlData), Encoding.UTF8.GetString(ssnArray));
                                }
                            }
                            else
                            {
                                Console.WriteLine(">> ID: " + cli.Id + " disconnected from the server");
                                GetClients.Remove(cli);
                            }
                            Thread.Sleep(100); // Wait a little before we move on to next iteration
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("An error occured: " + e.Message);
                    }
                    
                    }
            }).Start();
            
        }


        private void SendUserData(byte[] data, ref Socket client)
        {
            try
            {
                client.Send(data);
            }
            catch
            {
                Console.WriteLine(">> SendUserData() failed");
            }
        }

        public void RemoveCustomer(string ssn)
        {
            try
            {
                if (File.Exists("UserData/" + ssn + ".xml"))
                {
                    File.Delete("UserData/" + ssn + ".xml");
                    Console.WriteLine(">> Removed " + ssn + " successfully!");
                }
                else
                {
                    Console.WriteLine(">> Couldn't remove customer! This SSN doesn't probably exist!");
                }

            }
            catch
            {
                Console.WriteLine(">> Couldn't remove customer! This SSN doesn't probably exist!");
            }

        }

        public void ClientList()
        {
            Console.WriteLine(">> " + clients.Count + " clients connected.");

            foreach (Client cli in GetClients)
            {
                Console.WriteLine(">> ID: " + cli.Id);
            }
        }

    }
}
