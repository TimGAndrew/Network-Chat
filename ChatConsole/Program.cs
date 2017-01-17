using System;
using ChatLib;

namespace Chat
{ 
    /// <summary>
    /// The Main Program Driver of the console Chat Program
    /// </summary>
    /// 
    class Program
    {
        public static bool ServerMode = false;

        /// <summary>
        /// An method to display the command line information about the program
        /// </summary>
        static void InfoMessage()
        {
            Console.WriteLine("\t\t\tChat.exe - a simple network chat program");
            Console.WriteLine("\t\t\t\tUsage:");
            Console.WriteLine("\t\t\t\tChat.exe > runs the program as a client");
            Console.WriteLine("\t\t\t\tChat.exe -server > runs the program as a server");
            Console.WriteLine("\t\t\t\tChat.exe -help (or -?) > Displays this information\n");

            Console.WriteLine("\t\t\t\t\tConnection Operations:");
            Console.WriteLine("\t\t\t\t\tT > Talk mode (enter to send)");
            Console.WriteLine("\t\t\t\t\t\tNo text entered - exits talk mode");
            Console.WriteLine("\t\t\t\t\t\t/quit - Quit Chat");
#if DEBUG
            Console.Write("Press any key to exit.");
            Console.Read();
            #endif
        } //end InfoMessage()


        /// <summary>
        /// A method to check the command line arguments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void CheckArgs(string[] args)
        {
            //If there is more than one command-line argument:
            if (args.Length > 1)
            {
                Console.WriteLine("\t\tToo many arguments!\n");
                InfoMessage();
                Environment.Exit(0);
            }
            //If there is one command-line argument:
            else if (args.Length == 1)
            {
                //If argument is '-help' or '-?'
                if (args[0].ToLower().Equals("-help") || args[0].ToLower().Equals("-?"))
                {
                    InfoMessage();
                    Environment.Exit(0);
                }
                //if Argument is not '-server'
                else if (!args[0].ToLower().Equals("-server"))
                {
                    Console.WriteLine("\t\tIncorrect argument!\n");
                    InfoMessage();
                    Environment.Exit(0);
                }
                //if argument is '-server'
                else
                   ServerMode = true;
            }
        }// end CheckArgs()


        /// <summary>
        /// The main entry point for the console chat
        /// </summary>
        /// <param name="args">Arguments for the chat (eg. -server,  -help,  -?)</param>
        static void Main(string[] args)/*SUGGESTIONS: add -ip 127.0.0.1 -port 12345*/
        {
            CheckArgs(args);

            //Set up a server and client /*SUGGESTIONS:Could have been improved*/
            Client client = null;
            Server server = null;

            //Display and start Chat:
            if (ServerMode)
            {
                //As Server:
                Console.WriteLine("Server waiting for Client chat connection..");
                server = new Server("127.0.0.1", 12345);
            }
            else if (!ServerMode)
            {
                //As Client:
                Console.WriteLine("Client searching for Server chat connection..");
                client = new Client("127.0.0.1", 12345);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Connected!\n\t\t--Press '");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("T");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("' to enter talk mode, enter to send message, and /quit to exit chat---");
            Console.ResetColor();

            //Loop the program
            while (true)
            {
                string RecieveMessage = "";

                //Check if there is a message to receive:
                if (ServerMode)
                    RecieveMessage = server.RecieveMessage();
                else
                    RecieveMessage = client.RecieveMessage();

                //If so, write it:
                if (!RecieveMessage.Equals(""))
                {
                    //If the other connection has quit:
                    if (RecieveMessage.Equals(">>SERVER HAS QUIT<<") || RecieveMessage.Equals(">>CLIENT HAS QUIT<<"))
                    {
                        if (ServerMode)
                        {
                            server.Quit();
                        }
                        else
                        {
                            client.Quit();
                        }

                        //exit the program
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(RecieveMessage);
                        Console.WriteLine(">>CHAT ENDED<<");
                        Console.ResetColor();
                        return;
                    }
                    else
                    {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t\tThey Said >> " + RecieveMessage);
                    Console.ResetColor();

                    }
                }

                //Listen if the user wants to send text (presses T):
                if (Console.KeyAvailable)
                {
                     ConsoleKeyInfo keyinfo = Console.ReadKey(true);

                    //When T is pressed:
                    if (keyinfo.Key == ConsoleKey.T)
                    {
                        String SendMessage = "";
                        //Write the input arrows:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\tYou Said >> ");

                        //Get the input line:
                        SendMessage = Console.ReadLine();
                        Console.ResetColor();

                        //If the input is /quit: 
                        if (SendMessage.ToLower().Equals("/quit"))
                        {
                            if (ServerMode)
                            {
                                 server.SendMessage(">>SERVER HAS QUIT<<");
                                server.Quit();
                             }
                            else
                            {
                                client.SendMessage(">>CLIENT HAS QUIT<<");
                                client.Quit();
                            }

                            //exit the program
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(">>CHAT ENDED<<");
                            Console.ResetColor();
                            return;
                        }
                        //If the input is nothing:
                        else if (SendMessage.Equals(""))
                        {
                            //do nothing
                        }
                        //otherwise send the message:
                        else
                        {
                            if (ServerMode)
                            {
                                server.SendMessage(SendMessage);
                            }
                            else
                            {
                                client.SendMessage(SendMessage);
                            }
                        }
                    }
                }
            }
        } //End Main()
    } //End Program
} //End Chat 
