using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    public class Client
    {
        private TcpClient tcpClient;
        Session session;
        private NetworkStream stream;

        private object dummy_lock = new object();
        private bool run_state = true;

        public bool Running // If Running is true, then the client is connected to the server, if false - then it is not connected to the server
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

        public Client()
        {
            
            tcpClient = new TcpClient();
            session = new Session();

        }

        public Client(int id)
        {
            Id = id;

        }

        public int Id { get; set; }

        // Connect to the server
        public void Connect(string endpoint, short port)
        {

            if (Running)
            {
                try
                {
                    tcpClient.Connect(endpoint, port);
                    Debug.WriteLine("Connected");
                    stream = tcpClient.GetStream();
                    session.CreateId();
                    stream.Write(BitConverter.GetBytes(session.Id), 0, 4); // Send the server the clients session ID
                    Debug.WriteLine("Client id from client side: " + session.Id);
                    stream.Close();
                }
                catch
                {
                    Debug.WriteLine("Could not connect");
                    Running = false;
                }
                
            }

        }

        public void Disconnect()
        {
            Running = false;
        }

        public void SendRegistrationData()
        {

        }
    }
}
