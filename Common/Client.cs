using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Common
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public CurrentClientState ClientState { get; set; }
        public EndPoint EndPoint { get; set; }

        public enum CurrentClientState
        {
            Connected,
            Disconnected,

        }


        public Client(EndPoint endPoint)
        {
            EndPoint = endPoint;
            ClientState = CurrentClientState.Disconnected;
            tcpClient = new TcpClient();

        }

        public void Connect(string endpoint, short port)
        {
            if (ClientState == CurrentClientState.Disconnected)
            {
                tcpClient.Connect(endpoint, port);
                ClientState = CurrentClientState.Connected;
            }
        }
    }
}
