using System;

namespace MarsRover.Lib
{
    public class Rover
    {
        private Status status;
        private readonly Plateau plateau;

        public Rover(Plateau plateau, Direction direction, int x, int y)
        {
            this.plateau = plateau;
            this.status = new Status(x, y, direction);
        }

        public void Move(RoverAction action)
        {
            var nextStatus = NextStatus(action);
            if (!CanMoveOnPlateau(nextStatus.X, nextStatus.Y))
            {
                throw new Exception($"Couldn't move : {action}");
            }
            status = nextStatus;
        }

        public Status GetStatus()
        {
            return status;
        }

        private bool CanMoveOnPlateau(int nextX, int nextY)
        {
            if (nextX < 0 || nextY < 0 || nextX > plateau.Width || nextY > plateau.Height)
            {
                return false;
            }
            return true;
        }
        private Status NextStatus(RoverAction action)
        {
            var nextX = status.X;
            var nextY = status.Y;
            var nextDirection = status.Direction;

            switch (action)
            {
                case RoverAction.L://left
                    switch (status.Direction)
                    {
                        case Direction.E:// east + left = north
                            nextDirection = Direction.N;
                            break;
                        case Direction.N:// north + left = west
                            nextDirection = Direction.W;
                            break;
                        case Direction.W:// west + left = south
                            nextDirection = Direction.S;
                            break;
                        case Direction.S:// south + left = east
                            nextDirection = Direction.E;
                            break;
                    }

                    break;
                case RoverAction.R:// right
                    switch (status.Direction)
                    {
                        case Direction.E: // east + right = south
                            nextDirection = Direction.S;
                            break;
                        case Direction.N:// north + right = east
                            nextDirection = Direction.E;
                            break;
                        case Direction.W:// west + right = north
                            nextDirection = Direction.N;
                            break;
                        case Direction.S:// south + right = west
                            nextDirection = Direction.W;
                            break;
                    }
                    break;
                case RoverAction.M://move forward
                    switch (status.Direction)
                    {
                        case Direction.E:// east + move forward = x ++
                            nextX++;
                            break;
                        case Direction.N:// north + move forward = y ++
                            nextY++;
                            break;
                        case Direction.S:// south + move forward = y --
                            nextY--;
                            break;
                        case Direction.W:// west + move forward = x --
                            nextX--;
                            break;
                    }
                    break;
            }

            return new Status(nextX, nextY, nextDirection);
        }

    }
}
