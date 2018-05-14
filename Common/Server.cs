using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Common
{
    public class Server
    {
        private List<Client> clients;
        private IPAddress address;
        private short port;
        private Socket socket;
        private TcpListener tcpListener;
        private Object dummy_lock = new Object();
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
            Client temp;
            byte[] temp_id;

            tcpListener.Start();

            // Handle incoming connections in a new thread
            new Thread(() => 
            {
                while (Running)
                {
 
                    try
                    {
                        socket = tcpListener.AcceptSocket(); // Await connection
                        temp_id = new byte[4];
                        socket.Receive(temp_id);
                        clients.Add(new Client(BitConverter.ToInt32(temp_id, 0)));
                        Debug.WriteLine("Client id: " + (BitConverter.ToInt32(temp_id, 0)));
                        
                    }
                    catch // Something weird happened, discard the connection
                    {
                        Console.WriteLine(socket.RemoteEndPoint + " couldn't connect.");
                        continue;
                    }
                }

            }).Start();
            
        }

        public void Stop()
        {
            Running = false;
        }

        private void SendUserData()
        {
            
        }

    }
}
