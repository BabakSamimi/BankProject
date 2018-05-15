using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace BankServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 6060);

            server.Start();

            new Thread(() => 
            {
                while(server.Running)
                {
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "list":
                            server.ClientList();
                            break;

                        case "stop":
                            server.Stop();
                            break;

                        default:
                            Console.WriteLine("Command not recognizable");
                            continue;
                    }
                }


            }).Start();
        }
    }
}
