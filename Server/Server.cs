using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            var ipHost = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddr = ipHost.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddr, 7777);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting connection ... ");

                    Socket client = listener.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[8];

                    int data = client.Receive(bytes);

                    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, data));

                    //client.Shutdown(SocketShutdown.Both);
                    //client.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}