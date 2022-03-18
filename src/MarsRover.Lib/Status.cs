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
    }
}
