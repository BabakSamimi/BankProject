using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Concurrent;

namespace Common
{
    public class Server
    {
        private Dictionary<Socket, Client> clients;
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
        public Dictionary<Socket, Client> GetClients
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

            clients = new Dictionary<Socket, Client>();
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

                    //Client temp;
                    //byte[] temp_id;
                    //Socket cliSocket = null;

                    try
                    {
                        Socket cliSocket = tcpListener.AcceptSocket(); // Await connection
                        byte[] temp_id = new byte[4];
                        cliSocket.Receive(temp_id);
                        // Create a new client object and set its received ID.
                        Client temp = new Client(BitConverter.ToInt32(temp_id, 0));
                        GetClients.Add(cliSocket, temp);

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

            new Thread(new ThreadStart(Update)).Start();
            
        }

        public void Stop()
        {
            Running = false;
            tcpListener.Stop();
        }

        private bool IsConnected(Socket s)
        {
            if (s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0))
            {
                return false;
            }
            else
                return true;
        }
       
        private void Update()
        {
            byte[] data = new byte[1024];

            while (Running)
            {
                Thread.Sleep(1000);

                foreach (KeyValuePair<Socket, Client> cli in GetClients)
                {

                    if (IsConnected(cli.Key))
                    {
                        Debug.WriteLine(cli.Value.Id + " is still connected");
                    }
                }
            }
        }

        private void SendUserData()
        {
            
        }

        public void ClientList()
        {
            Console.WriteLine(clients.Count + " clients connected.");

            foreach (KeyValuePair<Socket, Client> cli in GetClients)
            {
                Console.WriteLine(">> ID: " + cli.Value.Id);
            }
        }

    }
}
