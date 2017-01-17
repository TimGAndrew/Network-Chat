using System;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// A Library to handle Server Connections
/// </summary>
namespace ChatLib
{
    /// <summary>
    /// A Chat Server Class.
    /// </summary>
    public class Server : Connection
    {
 
        /// <summary>
        /// The server constructor used to initialize a chat server and await for connection
        /// </summary>
        /// <param name="InternetAddress">The IP Address of the server (eg. "127.0.0.1")</param>
        /// <param name="NetworkPort">The port to listen for a connection on (eg. 1234)</param>
        public Server(String InternetAddress, Int32 NetworkPort)
        {
            try
            {
                //Set up variables to handle the incoming paramaters:
                IPAddress IP = IPAddress.Parse(InternetAddress);
                Int32 Port = NetworkPort;

                //Define the TCP Listener:
                server = new TcpListener(IP, Port);

                //Start listening for client requests.
                server.Start();

                //Await Connection:
                client = server.AcceptTcpClient();

                //set up a stream to send by:               
                stream = client.GetStream();
            }
            catch (SocketException e)
            {
                string error = ("SocketException: {0}"+ e);
            }
        }// end Server constructor.

        /// <summary>
        /// A quit method to handle the server quitting
        /// </summary>
        public override void Quit()
        {
            stream.Close();
            client.Close();
            server.Stop();
        }// end Quit()
    }// end Client
}// end ChatLib