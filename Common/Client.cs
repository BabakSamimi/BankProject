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
        public Socket socket;

        private object dummy_lock = new object();
        private bool run_state = false;

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
            Running = true;

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
            else
            {
                Debug.WriteLine("Could not connect to the server.");
                Running = false;
            }

           

        }

        public void Disconnect()
        {
            Running = false;
            tcpClient.Close();
        }

        private void PermanentStream()
        {
            while(Running)
            {
                try
                {
                    stream = tcpClient.GetStream();
                    socket.Send(new byte[1]);
                }
                catch { }
            }
        }

        public void SendRegistrationData(byte[] userData)
        {
            stream = tcpClient.GetStream();
            byte[] dataMessage = new byte[userData.Length + 1];
            dataMessage[dataMessage.Length] = 2;
            try
            {
                stream.Write(dataMessage, 0, dataMessage.Length);
                stream.Close();
            }
            catch
            {
                
            }
            
        }
    }
}
