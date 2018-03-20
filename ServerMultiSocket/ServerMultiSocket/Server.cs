using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerMultiSocket
{
    class Server
    {
        static TcpListener tcpListener;
        static void Main(string[] args)
        {
            IPAddress ipad = IPAddress.Parse("192.168.1.4");
            tcpListener = new TcpListener(ipad, 10);
            tcpListener.Start();
            Console.WriteLine("************This is Server program************");
            Console.WriteLine("How many clients are going to connect to this server?:");
            int numberOfClientsYouNeedToConnect = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
            {
                Thread newThread = new Thread(new ThreadStart(Listeners));
                newThread.Start();
            }
        }
        static void Listeners()
        {

            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                Console.WriteLine("Client:" + socketForClient.RemoteEndPoint + " now connected to server.");
                NetworkStream networkStream = new NetworkStream(socketForClient);
                System.IO.StreamWriter streamWriter =
                new System.IO.StreamWriter(networkStream);
                System.IO.StreamReader streamReader =
                new System.IO.StreamReader(networkStream);

                //here we send message to client
                Console.WriteLine("type your message to be recieved by client:");
                string theString = Console.ReadLine();
                streamWriter.WriteLine(theString);
                Console.WriteLine(theString);
                streamWriter.Flush();



                //here we recieve client's text if any.
                while (true)
                {
                    string theString1 = streamReader.ReadLine();
                    Console.WriteLine("Message recieved by client:" + theString1);
                    if (theString == "exit")
                        break;
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();


            }
            socketForClient.Close();
            Console.WriteLine("Press any key to exit from server program");
            Console.ReadKey();
        }
    }
}
