using Server.Lobbies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        private static bool s_Running = false;

        static void Main(string[] args)
        {
            Console.Title = "TSE Server";
            s_Running = true;

            Thread main = new Thread(new ThreadStart(MainThread));
            main.Start();

            Server.Start(50, 26950);
            LobbyPool.Init(25);
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main Thread started. Running at {Constants.TICKS_PER_SEC} ticks per second");
            DateTime nextLoop = DateTime.Now;

            while(s_Running)
            {
                while(nextLoop < DateTime.Now)
                {
                    Logic.Update();
                    nextLoop = nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if(nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
