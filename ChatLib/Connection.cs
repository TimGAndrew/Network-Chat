using System;
using System.Net.Sockets;

/// <summary>
/// A Library to handle Chat connections
/// </summary>
namespace ChatLib
{
    /// <summary>
    /// An abstract super class to handle Client or Server connections.
    /// </summary>
    public abstract class Connection
    {
        //Set up a TCP Listener and client:
        public TcpListener server = null;
        public TcpClient client = null;

        //set up a stream to send by:
        public NetworkStream stream = null;

        /// <summary>
        /// A method to recieve a message from the network stream
        /// </summary>
        /// <returns>The string in the network stream</returns>
        public string RecieveMessage()
        {
            String RecieveMessage = "";
            try
            {
                // Buffer to store the response bytes.
                Byte[] data = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                if (stream.DataAvailable)
                {
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    RecieveMessage = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                }
            }
            catch
            {
                //do nothing
            }
            return RecieveMessage;
        }//end RecieveMessage()

        /// <summary>
        /// A method to send a message to the network stream.
        /// </summary>
        /// <param name="Message">The message to send.</param>
        public void SendMessage(String Message)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] SendMessage = System.Text.Encoding.ASCII.GetBytes(Message);

            //check if the stream is available:
            if (stream.CanWrite)
            {
                // Send the message to the connected TcpServer.
                stream.Write(SendMessage, 0, SendMessage.Length);
            }
        }//end SendMessage()

        /// <summary>
        /// An Abstract Quit method to handle quitting the server or client
        /// </summary>
        public abstract void Quit();

    }//end Connection
}//end ChatLib
