using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Lib.Tests
{
    public class RoverTests
    {
        /////////////////////////////// SHOULD CREATE INSTANCE /////////////////////////////
        [Fact]
        public void Should_Create_Instance()
        {
            Plateau plateau = new Plateau(5, 10);

            var roverX = 2;
            var roverY = 3;
            var roverDirection = Direction.W;

            var roverStatus = new Status(roverX, roverY, roverDirection);
            var rover = new Rover(plateau, roverStatus);
            Assert.NotNull(rover);
            Assert.Equal(roverX, rover.GetStatus().X);
            Assert.Equal(roverY, rover.GetStatus().Y);
            Assert.Equal(roverDirection, rover.GetStatus().Direction);
        }

        /////////////////////////////// SHOULD THROW EXCEPTION /////////////////////////////
        [Theory]
        [MemberData(nameof(Get_Params_For_Should_Throw_Exception))]
        public void Should_Throw_Exception(Plateau plateau, Status roverStatus)
        {

            void act() => new Rover(plateau, roverStatus);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.NotNull(exception.Message);
            Assert.StartsWith("Invalid status for rover to start", exception.Message);
        }
        public static IEnumerable<object[]> Get_Params_For_Should_Throw_Exception()
        {
            var plateau = new Plateau(5, 5);
            yield return new object[] { plateau, new Status(6, 5, Direction.W) };
            yield return new object[] { plateau, new Status(5, 6, Direction.W) };
            yield return new object[] { plateau, new Status(6, 6, Direction.W) };
            yield return new object[] { plateau, new Status(-1, 4, Direction.W) };
            yield return new object[] { plateau, new Status(4, -1, Direction.W) };
            yield return new object[] { plateau, new Status(-1, -1, Direction.W) };
        }


        /////////////////////////////// SHOULD MOVE ON PLATEAU ///////////////////////////// 
        [Theory]
        [MemberData(nameof(Get_Params_For_Should_Move_On_Plateau))]
        public void Should_Move_On_Plateau(Plateau plateau, Status roverStatus, RoverAction[] actions)
        {
            var rover = new Rover(plateau, roverStatus);
            Assert.NotNull(rover);
            foreach (var action in actions)
            {
                try
                {
                    rover.Move(action);
                }
                catch (Exception ex)
                {
                    throw new Xunit.Sdk.XunitException("Expected no exception, but got: " + ex.Message);
                }
            }
            var currentStatus = rover.GetStatus();
            Assert.NotNull(currentStatus);
        }

        public static IEnumerable<object[]> Get_Params_For_Should_Move_On_Plateau()
        {
            var plateau = new Plateau(5, 5);

            for (int x = 0; x < plateau.Width; x++)
            {
                for (int y = 0; y < plateau.Height; y++)
                {
                    yield return new object[] { plateau, new Status(x, y, Direction.E), new[] { RoverAction.M, } };
                }
            }

        }


        /////////////////////////////////// SHOULD MOVE TO /////////////////////////////////
        [Theory]
        [MemberData(nameof(Get_Params_For_Should_Move_To))]
        public void Should_Move_To(Plateau plateau, Status roverStatus, RoverAction[] actions, Status lastStatus)
        {
            var rover = new Rover(plateau, roverStatus);
            Assert.NotNull(rover);
            foreach (var action in actions)
            {
                try
                {
                    rover.Move(action);
                }
                catch (Exception ex)
                {
                    throw new Xunit.Sdk.XunitException("Expected no exception, but got: " + ex.Message);
                }
            }
            var currentStatus = rover.GetStatus();
            Assert.NotNull(currentStatus);
            Assert.Equal(lastStatus, currentStatus);
        }
        public static IEnumerable<object[]> Get_Params_For_Should_Move_To()
        {
            var plateau = new Plateau(5, 5);

            /*Example inputs on excercise*/
            var moves = new[] { RoverAction.L, RoverAction.M, RoverAction.L, RoverAction.M, RoverAction.L, RoverAction.M, RoverAction.L, RoverAction.M, RoverAction.M, };
            var beginningStatus = new Status(1, 2, Direction.N);
            var lastStatus = new Status(1, 3, Direction.N);
            yield return new object[] { plateau, beginningStatus, moves, lastStatus };

            moves = new[] { RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.R, RoverAction.R, RoverAction.M };
            beginningStatus = new Status(3, 3, Direction.E);
            lastStatus = new Status(5, 1, Direction.E);
            yield return new object[] { plateau, beginningStatus, moves, lastStatus };


            /*Make square ClockWise: 3,3,E => M,R,M,M,R,M,M,R,M,M,R,M*/
            moves = new[] { RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M };
            beginningStatus = new Status(3, 3, Direction.N);
            yield return new object[] { plateau, beginningStatus, moves, beginningStatus };


            /*Go on boundary: 0,0,N => M,M,M,M,M,R,M,M,M,M,M,R,M,M,M,M,M,R,M,M,M,M,M,R*/
            moves = new[] { RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.R, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.M, RoverAction.R, };
            beginningStatus = new Status(0, 0, Direction.N);
            yield return new object[] { plateau, beginningStatus, moves, beginningStatus };

        }


        /////////////////////////////// SHOULDN'T MOVE ON PLATEAU ///////////////////////////// 
        [Theory]
        [MemberData(nameof(Get_Params_For_Shouldnt_Move_On_Plateau))]
        public void Shouldnt_Move_On_Plateau(Plateau plateau, Status roverStatus, RoverAction action)
        {
            var rover = new Rover(plateau, roverStatus);
            Assert.NotNull(rover);

            void act() => rover.Move(action);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.NotNull(exception.Message);
            Assert.StartsWith("Invalid action for rover to move", exception.Message);
        }

        public static IEnumerable<object[]> Get_Params_For_Shouldnt_Move_On_Plateau()
        {
            var plateau = new Plateau(5, 5);

            //left boundary
            var xStart = 0;
            for (int y = 0; y < plateau.Height; y++)
            {
                yield return new object[] { plateau, new Status(xStart, y, Direction.W), RoverAction.M };
            }

            //right boundary
            var xEnd = plateau.Width;
            for (int y = 0; y < plateau.Height; y++)
            {
                yield return new object[] { plateau, new Status(xEnd, y, Direction.E), RoverAction.M };
            }

            //bottom boundary
            var yStart = 0;
            for (int x = 0; x < plateau.Width; x++)
            {
                yield return new object[] { plateau, new Status(x, yStart, Direction.S), RoverAction.M };
            }

            //top boundary
            var yEnd = plateau.Height;
            for (int x = 0; x < plateau.Width; x++)
            {
                yield return new object[] { plateau, new Status(x, yEnd, Direction.N), RoverAction.M };
            }
        }

    }

}
