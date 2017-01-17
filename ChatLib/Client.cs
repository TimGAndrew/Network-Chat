using System;
using System.Net.Sockets;

/// <summary>
/// A Library to handle Client Connections
/// </summary>
namespace ChatLib
{
    /// <summary>
    /// A Chat Client Class.
    /// </summary>
    public class Client : Connection
    {

        /// <summary>
        /// The client constructor used to initialize a chat server and connect to a server connection
        /// </summary>
        /// <param name="InternetAddress">The IP Address of the server (eg. "127.0.0.1")</param>
        /// <param name="NetworkPort">The port to listen for a connection on (eg. 1234)</param>
        public Client(String InternetAddress, Int32 NetworkPort)
        {
            while (client == null)
            { 
                try
                {
                    //Set up a new TCP Client
                    client = new TcpClient(InternetAddress, NetworkPort);

                    //set up a stream to send by:
                    stream = client.GetStream();
                }
                catch (ArgumentNullException e)
                {
                    //Do nothing
                }
                catch (SocketException e)
                {
                    //Do nothing
                }
            }
        }//End Client constructor

        /// <summary>
        /// A quit method to handle the client quitting
        /// </summary>
        public override void Quit()
        {
            stream.Close();
            client.Close();
        }// end Quit()
    }// end Client
}// end ChatLib
