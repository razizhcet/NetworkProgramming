using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerProgram
{


    class Server
    {
        static Byte[] Buffer { get; set; }
        static Socket sck;
        static void Main(string[] args)
        {
            Console.WriteLine("Server");
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(0, 8888));
            sck.Listen(100);
            Console.WriteLine("The server is running at port 8888...");
            Console.WriteLine("Waiting for a connection.....");

            Socket Accepted = sck.Accept();
            Console.WriteLine("Connection accepted from " + Accepted.RemoteEndPoint);

            Byte[] con = Encoding.Default.GetBytes("Connected Successfull");
            Accepted.Send(con, 0, con.Length, 0);

            String str = "", str2 = "";
            while (!str.Equals("stop"))
            {
                Buffer = new Byte[Accepted.SendBufferSize];
                int bytesread = Accepted.Receive(Buffer);
                Byte[] formattd = new Byte[bytesread];
                for (int i = 0; i < bytesread; i++)
                {
                    formattd[i] = Buffer[i];
                }
                str = Encoding.ASCII.GetString(formattd);
                Console.WriteLine("client says: " + str);
                Console.Write("Enter Text : ");
                str2 = Console.ReadLine();
                Byte[] buf = Encoding.Default.GetBytes(str2);
                Accepted.Send(buf, 0, buf.Length, 0);
                Console.WriteLine("Send Successfull");

            }

            // string strData = Encoding.ASCII.GetString(formattd);
            // Console.WriteLine(strData + "\r\n");
            Console.Read();
            sck.Close();
            Accepted.Close();
        }


    }


}
