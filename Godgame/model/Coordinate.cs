namespace Godgame.model
{
    enum Direction
    {
        up, down, left, right
    }
    //static class DirectionMethods{public static }
    struct Coordinate
    {
        public readonly int x;
        public readonly int y;

        public Coordinate(int x_, int y_)
        {
            x = x_;
            y = y_;
        }

        public Coordinate(Coordinate coor)
        {
            x = coor.x;
            y = coor.y;
        }

        public static bool operator <(Coordinate a, Coordinate b)
        {
            return a.x < b.x || (a.x == b.x && a.y < b.y);
        }

        public static bool operator >(Coordinate a, Coordinate b)
        {
            return a.x > b.x || (a.x == b.x && a.y > b.y);
        }

        public Coordinate getNeighbour(Direction direction)
        {
            switch (direction)
            {
                case Direction.up:
                    return new Coordinate(x, y - 1);
                case Direction.down:
                    return new Coordinate(x, y + 1);
                case Direction.left:
                    return new Coordinate(x - 1, y);
                case Direction.right:
                    return new Coordinate(x + 1, y);
                default: return new Coordinate(0, 0);
            }
        }
    }
}
