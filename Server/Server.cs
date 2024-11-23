using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> Clients { get; private set; }

        private static TcpListener m_Listener;

        public static void Start(int maxPlayers, int port)
        {
            MaxPlayers = maxPlayers;
            Port = port;

            Console.WriteLine("Starting server...");
            InitialiseClients();

            m_Listener = new TcpListener(IPAddress.Any, Port);
            m_Listener.Start();
            m_Listener.BeginAcceptTcpClient(TCPAttemptConnect, null);

            Console.WriteLine($"Server started on port {Port}");
        }

        private static void TCPAttemptConnect(IAsyncResult result)
        {
            TcpClient client = m_Listener.EndAcceptTcpClient(result);
            m_Listener.BeginAcceptTcpClient(TCPAttemptConnect, null);
            Console.WriteLine($"Connection attempt from {client.Client.RemoteEndPoint}");

            for(int i = 1; i <= MaxPlayers; i++)
            {
                if (Clients[i].TCP.Socket == null)
                {
                    Clients[i].TCP.Connect(client);
                    return;
                }
            }

            Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect: Server full");
        }

        private static void InitialiseClients()
        {
            Clients = new Dictionary<int, Client>();
            for(int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new Client(i));
            }
        }
    }
}
