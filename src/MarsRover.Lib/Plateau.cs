using System;

namespace MarsRover.Lib
{
    public class Plateau
    {
        public Plateau(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentException($"Invalid length for a {nameof(Plateau)}. Width :'{width}', Height :'{height}'");
            }
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
