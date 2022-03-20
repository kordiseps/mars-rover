using System;

namespace MarsRover.Lib
{
    public class Rover
    {
        private Status status;
        private readonly Plateau plateau;

        public Rover(Plateau plateau, Status status)
        {
            this.plateau = plateau;
            if(!IsValidForPlateau(status.X, status.Y))
            {
                throw new ArgumentException($"Invalid status for rover to start : {status}");
            }
            this.status = status;
        }

        public void Move(RoverAction action)
        {
            var nextStatus = NextStatus(action);
            if (!IsValidForPlateau(nextStatus.X, nextStatus.Y))
            {
                throw new ArgumentException($"Invalid action for rover to move : {action}");
            }
            status = nextStatus;
        }

        public Status GetStatus()
        {
            return status;
        }

        private bool IsValidForPlateau(int x, int y)
        {
            if (x < 0 || y < 0 || x > plateau.Width || y > plateau.Height)
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
