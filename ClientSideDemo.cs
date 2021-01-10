using System;
using System.Net;
using System.Net.Sockets;

namespace ClientSideDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- C# Client Side ---");
            Console.WriteLine();

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdd = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAdd, 27015);
            socket.Connect(remoteEP);

            Console.WriteLine("Type message to send: ");
            string message = Console.ReadLine();
            message += char.MinValue;

            while (!string.IsNullOrEmpty(message))
            {
                message += char.MinValue;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(message);
                socket.Send(byData);

                byte[] buffer = new byte[1024];
                int iRx = socket.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                string recv = new System.String(chars);
                Console.WriteLine($"Reply: {recv}");

                Console.WriteLine();
                Console.WriteLine("Type message to send: ");
                message = Console.ReadLine();                
            }
        }
    }
}
