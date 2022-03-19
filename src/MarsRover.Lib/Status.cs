using System;

namespace MarsRover.Lib
{
    public class Status
    {
        public Status(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Direction}";
        }
        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }


        public override bool Equals(object obj)
        {
            return 
                obj is Status status &&
                status.X == X &&
                status.Y == Y &&
                status.Direction == Direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Direction);
        }
    }
}
