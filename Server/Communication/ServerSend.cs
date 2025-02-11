using Server.Game;
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
        /// Sends the BeginLevel packet to a client
        /// </summary>
        /// <param name="toClient">The client to send to</param>
        /// <param name="levelID">The level ID that is going to be sent</param>
        public static void BeginLevel(int toClient, int levelID)
        {
            using(Packet packet = new Packet((int)ServerPackets.BeginLevel))
            {
                Level lvl = LevelManager.GetLevel(levelID);

                if (lvl != null)
                {
                    packet.WriteInt(levelID);
                    packet.WriteInt(lvl.GetObjects().Count);
                    packet.WriteInt(toClient);

                    SendTCPData(toClient, packet);
                }
            }
        }

        /// <summary>
        /// Sends the EndLevel packet to a client
        /// </summary>
        /// <param name="toClient">The client to send to</param>
        /// <param name="levelID">The level ID that has been sent</param>
        public static void EndLevel(int toClient, int levelID)
        {
            using(Packet packet = new Packet((int)ServerPackets.EndLevel))
            {
                packet.WriteInt(levelID);
                packet.WriteInt(toClient);

                SendTCPData(toClient, packet);
            }
        }

        public static void SendLevelObject(int toClient, int objID)
        {
            using(Packet packet = new Packet((int)ServerPackets.SendLevelObject))
            {
                LevelObject obj = LevelManager.GetLevel(Program.World.GetCurrentLevel()).GetObjects()[objID];

                packet.WriteInt(objID);
                packet.WriteString(obj.Name);
                packet.WriteVector3(obj.Position);
                packet.WriteVector3(obj.Rotation);
                packet.WriteVector3(obj.Scale);
                packet.WriteInt(obj.GetObjectComponents().Count);
                
                for(int i = 0; i < obj.GetObjectComponents().Count; i++)
                {
                    ObjectComponent curr = obj.GetObjectComponents()[i];
                    packet.WriteInt((int)curr.Type);

                    switch(curr.Type)
                    {
                        case ObjectComponentType.SpriteRenderer:
                            {
                                SpriteRendererData data = (SpriteRendererData)curr.Data;

                                packet.WriteInt((int)data.Sprite);
                                packet.WriteVector4(data.Color);
                                packet.WriteBool(data.FlipX);
                                packet.WriteBool(data.FlipY);

                                break;
                            }
                    }
                }

                packet.WriteInt(toClient);

                SendTCPData(toClient, packet);
            }
        }
    }
}
