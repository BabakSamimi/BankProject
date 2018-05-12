using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Common
{
    class Server
    {
        public List<Client> clients;
        private IPAddress address;
        private short port;
        private Socket socket;
        private TcpListener tcpListener;

        public Server(string address, short port)
        {
            this.address = IPAddress.Parse(address);
            this.port = port;

            clients = new List<Client>();
            tcpListener = new TcpListener(this.address, this.port);

        }

        public void Start()
        {
            Client temp;
            tcpListener.Start();

            // Handle incoming connections in a new thread
            new Thread(() => 
            {
                while (true)
                {
                    
                    try
                    {
                        socket = tcpListener.AcceptSocket(); // Await connection
                        
                    }
                    catch // Something weird happened, discard the connection
                    {
                        Console.WriteLine(socket.RemoteEndPoint + " couldn't connect.");
                        continue;
                    }
                    temp = new Client(socket.RemoteEndPoint);
                    clients.Add(temp);
                }

            });
            
        }

        private void SendUserData(Client client)
        {
            
        }

    }
}
