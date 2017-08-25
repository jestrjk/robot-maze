using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_maze_map
{
    public class Map :IEnumerable<Map.MapBlock>
    {
        Random ran = new Random();

        public int MapSize {
            get { return _mapSize; }
            private set { _mapSize = value; }
        }

        int _mapSize = 20;

        public enum MapBlockType
        {
            Empty,
            Soil,
            Grass,
            Bush,
            Tree,
            Wall,
            Buttcrack
        }

        private MapBlockType[,] _map;

        public Map(int mapSize = 20)
        {
            _mapSize = mapSize;
            _map = new MapBlockType[mapSize, mapSize];

            initMapArray();
            BuildWalls();
            PlaceButtCrack();
        }

        public class MapBlock
        {
            public MapBlock(MapBlockType type, MapBlockCoordinates coords)
            {
                Coordinates = coords;
                Type = type;
            }

            public MapBlockType Type { get; private set; }
            public MapBlockCoordinates Coordinates { get; private set; }

        }
        public class MapBlockCoordinates
        {
            public MapBlockCoordinates(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        public void AgeMap( int ticks = 1)
        {
            foreach ( MapBlock block in this )
            {

            }
        }

        public List<MapBlock> SurroundingBlocks( MapBlockCoordinates currentCoords, int radius )
        {
            double radiusSquared = Math.Pow( radius, 2 ) ;

            List<MapBlock> surroundingBlocks = new List<MapBlock>();

            int xStart = currentCoords.X - radius;
            int yStart = currentCoords.Y - radius;
            int xEnd = currentCoords.X + radius;
            int yEnd = currentCoords.Y + radius;

            for ( int x = xStart; x <= xEnd; x++ )
                for ( int y = yStart; y <= yEnd; y++ )
                {
                    double dxSquared = Math.Pow( (currentCoords.X - x), 2);
                    double dySquared = Math.Pow((currentCoords.Y - y) ,2);

                    if ( (dxSquared + dySquared) > radiusSquared ) continue;
                    if (InBounds(x, y)) surroundingBlocks.Add(new MapBlock(this._map[x, y], new MapBlockCoordinates(x, y)));
                }

            return surroundingBlocks;
        }

        public bool InBounds ( int X, int Y )
        {
            MapBlockCoordinates coords = new MapBlockCoordinates(X, Y);
            return InBounds(coords);
        }
        public bool InBounds ( MapBlockCoordinates coords )
        {
            if (coords.X < 0) return false;
            if (coords.X >= this.MapSize) return false;
            if (coords.Y < 0) return false;
            if (coords.Y >= this.MapSize) return false;

            return true;
        }
       
        private void PlaceButtCrack()
        {
            _map[ran.Next(_mapSize), ran.Next(_mapSize)] = MapBlockType.Buttcrack;
        }

        private void BuildWalls()
        {
            
            int xStart = ran.Next(_mapSize);
            int xEnd = ran.Next(_mapSize);
            int yStart = ran.Next(_mapSize);
            int yEnd = ran.Next(_mapSize);

            // Reverse them if they are wrong ordered for sane for loops ~
            SwapValues(ref xStart, ref xEnd);
            SwapValues(ref yStart, ref yEnd);

            for( int x = xStart; x < xEnd; x++)
            {
                for ( int y = yStart; y < yEnd; y++ )
                {
                    _map[x, y] = MapBlockType.Wall;
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
            for ( int x =0; x < _mapSize; x++ )
            {
                for ( int y = 0; y < _mapSize; y++ )
                {
                    _map[x, y] = MapBlockType.Empty;                       
                    
                }
            }
        }

        IEnumerator<MapBlock> IEnumerable<MapBlock>.GetEnumerator()
        {
            for ( int x = 0; x < MapSize; x++ )
                for (int y = 0; y < MapSize; y++ )
                {
                    yield return new MapBlock(_map[x, y], new MapBlockCoordinates(x, y));
                }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
