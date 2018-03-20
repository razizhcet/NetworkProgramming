using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketProgramming
{
    class ShowIP
    {
        static void Main(string[] args)
        {
            string name = args.Length < 1 ? Dns.GetHostName() : args[0];
            try
            {
                IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;
                foreach (IPAddress addr in addrs)
                    Console.WriteLine("{0}/{1}", name, addr);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            Console.ReadKey();
        }
    }
}
