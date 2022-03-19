using MarsRover.Lib;
using System;
using System.Linq;

namespace MarsRover.ConsoleApp
{
    internal class Program
    {

        /*
Test Input:
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
Expected Output:
1 3 N
5 1 E   

5 5
3 3 E 
MRMMRMMRMMRM
0 0 N
MMMMMRMMMMMRMMMMMRMMMMMR
        
         */
        static void Main(string[] args)
        {
            Console.Write("Plateau size : ");
            var plateauBoundariesInput = Console.ReadLine();
            var plateauWidth = Convert.ToInt32(plateauBoundariesInput.Split(' ')[0]);
            var plateauHeight = Convert.ToInt32(plateauBoundariesInput.Split(' ')[1]);

            var plateau = new Plateau(plateauWidth, plateauHeight);
            while (true)
            {
                Console.Write("Rover status : ");
                var roverStatusInput = Console.ReadLine();
                var roverX = Convert.ToInt32(roverStatusInput.Split(' ')[0]);
                var roverY = Convert.ToInt32(roverStatusInput.Split(' ')[1]);
                var roverDirection = Enum.Parse<Direction>(roverStatusInput.Split(' ')[2]);
                var roverStatus = new Status(roverX, roverY, roverDirection);
                var rover = new Rover(plateau, roverStatus);

                Console.Write("Actions : ");
                var actionsInput = Console.ReadLine();
                var actions = actionsInput.ToCharArray().Select(x => Enum.Parse<RoverAction>(x.ToString()));
                foreach (var action in actions)
                {
                    try
                    {
                        rover.Move(action);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
                var status = rover.GetStatus();
                Console.WriteLine($"Result : {status}");
                Console.WriteLine("******************");
            }

        }
    }
}
