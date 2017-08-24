using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_maze_map
{
    public class Map
    {
        Random ran = new Random();

        static int mapSize = 20;

        public enum MapBlock
        {
            Empty,
            Wall,
            Buttcrack
        }

        private MapBlock[,] _map = new MapBlock[mapSize, mapSize] ;

        Map()
        {
            initMapArray();
            BuildWalls();
            PlaceButtCrack();
        }

        private void PlaceButtCrack()
        {
            _map[ran.Next(mapSize), ran.Next(mapSize)] = MapBlock.Buttcrack;
        }

        private void BuildWalls()
        {
            
            int xStart = ran.Next(mapSize);
            int xEnd = ran.Next(mapSize);
            int yStart = ran.Next(mapSize);
            int yEnd = ran.Next(mapSize);

            // Reverse them if they are wrong ordered for sane for loops ~
            SwapValues(ref xStart, ref xEnd);
            SwapValues(ref yStart, ref yEnd);

            for( int x = xStart; x < xEnd; x++)
            {
                for ( int y = yStart; y < yEnd; y++ )
                {
                    _map[x, y] = MapBlock.Wall;
                }
            }
        }

        private static void SwapValues(ref int xStart, ref int xEnd)
        {
            if (xEnd < xStart)
            {
                int temp = xStart;
                xStart = xEnd;
                xEnd = temp;
            }
        }

        private void initMapArray()
        {
            for ( int x =0; x < mapSize; x++ )
            {
                for ( int y = 0; y < mapSize; y++ )
                {
                    _map[x, y] = MapBlock.Empty;                       
                    
                }
            }
        }
    }
}
