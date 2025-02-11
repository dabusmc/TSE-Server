using Server.Game.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class LevelManager
    {
        private static List<Level> s_Levels;

        public static void Init()
        {
            s_Levels = new List<Level>();
            s_Levels.Add(new TestLevel());
        }

        /// <summary>
        /// Get a Level from its ID
        /// </summary>
        /// <param name="index">The ID of the Level</param>
        /// <returns>The corresponding Level for the ID if the ID is valid, otherwise null</returns>
        public static Level GetLevel(int index)
        {
            if(index < 0 || index >= s_Levels.Count)
            {
                Console.WriteLine($"Level index invalid: {index}");
                return null;
            }

            return s_Levels[index];
        }
    }
}
