using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();
            string username = packet.ReadString();

            Console.WriteLine($"{Server.Clients[fromClient].TCP.Socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}");

            if(fromClient != clientID)
            {
                Console.WriteLine($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
            }
            
            // TODO: Send player into game
        }
    }
}
