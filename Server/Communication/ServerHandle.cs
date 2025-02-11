using Server.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.WriteLine($"\"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            Console.WriteLine($"{Server.Clients[clientID].TCP.Socket.Client.RemoteEndPoint} connected successfully and is now player {clientID}");

            Server.Clients[clientID].Data.Username = username;

            Console.WriteLine($"Sending level to player {clientID}...");
            ServerSend.BeginLevel(clientID, Program.World.GetCurrentLevel());
        }

        /// <summary>
        /// Handles the incoming LevelReady packet from client
        /// </summary>
        /// <param name="fromClient">The ID of the client that send this packet</param>
        /// <param name="packet">The packet data itself</param>
        public static void LevelReady(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();

            if (fromClient != clientID)
            {
                Console.WriteLine($"\"{Server.Clients[clientID].Data.Username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            if(LevelManager.GetLevel(Program.World.GetCurrentLevel()).GetObjects().Count == 0)
            {
                ServerSend.EndLevel(clientID, Program.World.GetCurrentLevel());
            }
            else
            {
                ServerSend.SendLevelObject(clientID, 0);
            }
        }

        /// <summary>
        /// Handles the incoming LevelReceived packet from client
        /// </summary>
        /// <param name="fromClient">The ID of the client that send this packet</param>
        /// <param name="packet">The packet data itself</param>
        public static void LevelReceived(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();

            if (fromClient != clientID)
            {
                Console.WriteLine($"\"{Server.Clients[clientID].Data.Username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            Console.WriteLine($"Level fully sent to player {clientID}!");
        }

        /// <summary>
        /// Handles the incoming LevelObjectReceived packet from client
        /// </summary>
        /// <param name="fromClient">The ID of the client that send this packet</param>
        /// <param name="packet">The packet data itself</param>
        public static void LevelObjectReceived(int fromClient, Packet packet)
        {
            int clientID = packet.ReadInt();

            if (fromClient != clientID)
            {
                Console.WriteLine($"\"{Server.Clients[clientID].Data.Username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientID})!");
                return;
            }

            int objID = packet.ReadInt();

            if (objID == LevelManager.GetLevel(Program.World.GetCurrentLevel()).GetObjects().Count - 1)
            {
                ServerSend.EndLevel(clientID, Program.World.GetCurrentLevel());
            }
            else
            {
                ServerSend.SendLevelObject(clientID, objID + 1);
            }
        }
    }
}
