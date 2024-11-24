using Server.Lobbies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerHandle
    {
        /// <summary>
        /// Handles the incoming WelcomeReceived packet from client
        /// </summary>
        /// <param name="fromClient">The ID of the client that send this packet</param>
        /// <param name="packet">The packet data itself</param>
        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();
            string username = packet.ReadString();

            if (fromClient != clientID)
            {
                Console.WriteLine($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            Console.WriteLine($"{Server.Clients[fromClient].TCP.Socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}");

            Server.Clients[fromClient].Data.Username = username;

            // TODO: Send player into game
        }

        public static void FindAvailableLobby(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();

            if (fromClient != clientID)
            {
                Console.WriteLine($"Player \"{Server.Clients[fromClient].Data.Username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            int id = LobbyPool.GetNextAvailableLobby();
            if(id != -1)
            {
                LobbyPool.GetLobbyFromID(id).ConnectClient(fromClient);
                ServerSend.ConnectedToLobby(fromClient, LobbyPool.GetLobbyFromID(id));
                Console.WriteLine($"Player \"{Server.Clients[fromClient].Data.Username}\" (ID: {fromClient}) connected to lobby {id}");
            }
            else
            {
                // TODO: Send a packet to tell the user they couldn't connect to a lobby
            }
        }
    }
}
