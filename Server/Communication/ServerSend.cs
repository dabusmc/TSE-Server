using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerSend
    {
        /// <summary>
        /// Send packet to a single client
        /// </summary>
        /// <param name="toClient">The client ID to send to</param>
        /// <param name="packet">The packet to send</param>
        private static void SendTCPData(int toClient, Packet packet)
        {
            packet.WriteLength();
            Server.Clients[toClient].TCP.SendData(packet);
        }

        /// <summary>
        /// Send packet to all clients
        /// </summary>
        /// <param name="packet">The packet to send</param>
        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.Clients[i].TCP.SendData(packet);
            }
        }

        /// <summary>
        /// Send packet to all except one client
        /// </summary>
        /// <param name="exceptClient">The client ID to not send to</param>
        /// <param name="packet">The packet to send</param>
        private static void SendTCPDataToAllExcept(int exceptClient, Packet packet)
        {
            packet.WriteLength();
            for(int i = 1; i <= Server.MaxPlayers; i++)
            {
                if(i != exceptClient)
                {
                    Server.Clients[i].TCP.SendData(packet);
                }
            }
        }

        /// <summary>
        /// Sends the Welcome packet to a client
        /// </summary>
        /// <param name="toClient">The client to send to</param>
        /// <param name="msg">The welcome message to send</param>
        public static void Welcome(int toClient, string msg)
        {
            using(Packet packet = new Packet((int)ServerPackets.Welcome))
            {
                packet.WriteString(msg);
                packet.WriteInt(toClient);

                SendTCPData(toClient, packet);
            }
        }

        /// <summary>
        /// Sends the ConnectedToLobby packet to a client
        /// </summary>
        /// <param name="toClient">The client to send to</param>
        /// <param name="lobby">The Lobby that they have connected to</param>
        public static void ConnectedToLobby(int toClient, Lobby lobby)
        {
            using(Packet packet = new Packet((int)ServerPackets.ConnectedToLobby))
            {
                packet.WriteInt(lobby.GetID());
                packet.WriteInt(toClient);

                packet.WriteInt(lobby.Count() - 1);
                for(int i = 0; i < lobby.Count(); i++)
                {
                    int clientID = lobby.GetClientInLobby(i);

                    if (clientID == toClient)
                        continue;

                    packet.WriteInt(clientID);
                    packet.WriteString(Server.Clients[clientID].Data.Username);
                }

                SendTCPData(toClient, packet);

                PlayerJoinedLobby(toClient, lobby);
            }
        }

        public static void PlayerJoinedLobby(int clientWhoJoined, Lobby lobby)
        {
            using(Packet packet = new Packet((int)ServerPackets.PlayerJoinedLobby))
            {
                packet.WriteInt(lobby.GetID());

                packet.WriteInt(clientWhoJoined);
                packet.WriteString(Server.Clients[clientWhoJoined].Data.Username);

                SendTCPDataToAllExcept(clientWhoJoined, packet);
            }
        }
    }
}
