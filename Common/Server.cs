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
       
        private bool DataReceived(ref Socket s, ref byte[] data)
        {
            if (s.Receive(data) == 0)
            {
                return false;
            }
            else
                return true;
        }

        public void UpdateClients()
        {
            new Thread(() =>
            {
                while (Running)
                {
                    byte[] buffer;

                    // Check if there are any data to receive from clients
                    foreach (Client cli in GetClients)
                    {
                        buffer = new byte[1024];

                        if (IsConnected(cli))
                        {
                            if (DataReceived(ref cli.socket, ref buffer))
                            {
                                if (buffer[0] == 1)
                                {
                                    Debug.WriteLine("We successfully validated that this is a reg datapacket");
                                }
                            }
                        }
                        else
                        {
                            GetClients.Remove(cli);
                        }
                        Thread.Sleep(100); // Wait a little before we move on to next iteration
                    }
                }

            }).Start();
            
        }

        /*
         * ID 
         * 
         * 
         * 
         */

        private void SendUserData()
        {
            
        }

        public void ClientList()
        {
            Console.WriteLine(clients.Count + " clients connected.");

            foreach (Client cli in GetClients)
            {
                Console.WriteLine(">> ID: " + cli.Id);
            }
        }

    }
}
