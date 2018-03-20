using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientMultiSocket1
{
    class Client
    {
        static void Main(string[] args)
        {
            TcpClient socketForServer;
            try
            {
                socketForServer = new TcpClient("192.168.1.4", 10);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            NetworkStream networkStream = socketForServer.GetStream();
            System.IO.StreamReader streamReader =
            new System.IO.StreamReader(networkStream);
            System.IO.StreamWriter streamWriter =
            new System.IO.StreamWriter(networkStream);
            Console.WriteLine("*******This is client program who is connected to localhost on port No:10*****");

            try
            {
                // read the data from the host and display it
                string outputString;
                {
                    outputString = streamReader.ReadLine();
                    Console.WriteLine("Message Recieved by server:" + outputString);

                    Console.WriteLine("Type your message to be recieved by server:");
                    Console.WriteLine("type:");
                    string str = Console.ReadLine();
                    while (str != "exit")
                    {
                        streamWriter.WriteLine(str);
                        streamWriter.Flush();
                        Console.WriteLine("type:");
                        str = Console.ReadLine();
                    }
                    if (str == "exit")
                    {
                        streamWriter.WriteLine(str);
                        streamWriter.Flush();

                    }

                }
            }
            catch
            {
                Console.WriteLine("Exception reading from Server");
            }
            // tidy up
            networkStream.Close();
            Console.WriteLine("Press any key to exit from client program");
            Console.ReadKey();
        }
    }
}
