using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robot_maze_map;


namespace robot_maze
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                robot_maze_map.Map _map = new Map();

                DisplayMap(_map);
            }
            while (Console.ReadKey().KeyChar != 'q');


            
        }

        private static void DisplayMap(Map map)
        {
            foreach ( Map.MapBlock block in map )
            {
                Console.SetCursorPosition(block.Coordinates.X, block.Coordinates.Y);

                string displayCharacter = "E";
                switch ( block.Type )
                {
                    case Map.MapBlockType.Empty:
                        displayCharacter = ".";
                        break;
                    case Map.MapBlockType.Wall :
                        displayCharacter = "#";
                        break;
                    case Map.MapBlockType.Buttcrack:
                        displayCharacter = "B";
                        break;
                }

                Console.Write(displayCharacter);
            }

            Console.Out.Flush();
        }
    }
}
