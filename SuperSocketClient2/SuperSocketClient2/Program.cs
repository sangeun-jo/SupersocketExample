using SuperSocketClient;
using System;

namespace SuperSocketClient2
{
    public class Program
    {

        static void Main(string[] args)
        {
            ClientSocket socket = new ClientSocket();

            string address = "127.0.0.1";
            int port = 18732;

            if (socket.conn(address, port))
            {
                Console.WriteLine($"{DateTime.Now}. 서버에 접속 중");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now} 서버에 접속 실패");
            }

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
        }
        
    }
}
