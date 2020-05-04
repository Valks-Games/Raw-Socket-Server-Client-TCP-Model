using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using UnityEngine;

class Client : MonoBehaviour
{
    private static Socket client;

    void Start()
    {
        Application.targetFrameRate = 30;
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
    }

    public static void Connect()
    {
        try
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 7777);
            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(localEndPoint);

                Debug.Log($"Socket connected to -> {client.RemoteEndPoint}");
                
                byte[] data = Encoding.ASCII.GetBytes("1<EOF>");
                client.Send(data);

                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }

            // Manage of Socket's Exceptions 
            catch (ArgumentNullException ane)
            {
                Debug.Log($"ArgumentNullException : {ane}");
            }

            catch (SocketException se)
            {

                Debug.Log($"SocketException : {se}");
            }

            catch (Exception e)
            {
                Debug.Log($"Unexpected exception : {e}");
            }
        }

        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public static bool IsConnected()
    {
        return client.Connected;
    }
}