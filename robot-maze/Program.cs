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
        static robot_maze_map.Map _map = new robot_maze_map.Map();
        static robot_maze_map.Map.MapBlockCoordinates _currentCoords = new Map.MapBlockCoordinates(0, 0);

        static void Main(string[] args)
        {
            DisplayMap(_map);

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _currentCoords.X -= 1;
                        break;
                    case ConsoleKey.UpArrow:
                        _currentCoords.Y -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _currentCoords.X += 1;
                        break;
                    case ConsoleKey.DownArrow:
                        _currentCoords.Y += 1;
                        break;
                }

                if (!_map.InBounds(_currentCoords))
                {
                    _currentCoords = new Map.MapBlockCoordinates(0, 0);
                }

                DisplayMap(_map);

                
                List<Map.MapBlock> blocks;

                switch (keyInfo.KeyChar)
                {
                    case 'g':
                        _map = new Map();
                        DisplayMap(_map);
                        break;

                    case '1':
                        blocks = _map.SurroundingBlocks(_currentCoords, 1);

                        DisplayBlocks(blocks, '*');

                        break;
                    case '3':
                        blocks = _map.SurroundingBlocks(_currentCoords, 3);

                        DisplayBlocks(blocks, '*');

                        break;
                    case '5':
                        blocks = _map.SurroundingBlocks(_currentCoords, 5);

                        DisplayBlocks(blocks, '*');

                        break;
                    case 'q':
                        Environment.Exit(0);
                        break;
                }
            }
                
                    
                        
        }

        private static void DisplayBlocks(List<Map.MapBlock> blocks, char charToDisplay)
        {
            foreach ( Map.MapBlock block in blocks )
            {
                Console.SetCursorPosition(block.Coordinates.X, block.Coordinates.Y);
                Console.Write(charToDisplay);
            }

            Console.Out.Flush();
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

            Console.SetCursorPosition(_currentCoords.X, _currentCoords.Y);
            Console.Write("@");

            Console.Out.Flush();
        }
    }
}
