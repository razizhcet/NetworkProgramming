using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProgram
{
    class Client
    {
        static Socket sck;
        static void Main(string[] args)
        {
            Console.WriteLine("Client");
            Console.Write("Enter IP Address : ");
            string ip = Console.ReadLine();
            Console.Write("Enter Port Number : ");
            int port = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Connecting.....");
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ed = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                sck.Connect(ed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main(args);
            }

            Byte[] bt = new Byte[255];
            int l = sck.Receive(bt, 0, bt.Length, 0);
            Array.Resize(ref bt, l);
            Console.WriteLine(Encoding.Default.GetString(bt));
            //Byte[] data = Encoding.ASCII.GetBytes(text);
            //sck.Send(data);
            String str = "", str2 = "";
            while (!str.Equals("stop"))
            {
                Console.Write("Enter Text : ");
                str = Console.ReadLine();
                Byte[] data1 = Encoding.ASCII.GetBytes(str);
                sck.Send(data1);
                Console.WriteLine("Send Successful");

                int r = sck.Receive(bt, 0, bt.Length, 0);
                Array.Resize(ref bt, r);
                str2 = Encoding.Default.GetString(bt);
                Console.WriteLine("Server says: " + str2);
            }
            //Console.WriteLine("Data send");
            Console.WriteLine("Press any key");
            Console.Read();
            sck.Close();
        }
    }

}


