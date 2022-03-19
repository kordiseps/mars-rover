using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Lib.Tests
{
    public class PlateauTests
    {
        /////////////////////////////// SHOULD CREATE INSTANCE /////////////////////////////
        [Fact]
        public void Should_Create_Instance()
        {
            var width = 2;
            var height = 3;
            Plateau plateau = new Plateau(width, height);
            Assert.NotNull(plateau);
            Assert.Equal(width, plateau.Width);
            Assert.Equal(height, plateau.Height);
        }

        /////////////////////////////// SHOULD THROW EXCEPTION /////////////////////////////
        [Theory]
        [MemberData(nameof(Get_Params_For_Should_Throw_Exception))]
        public void Should_Throw_Exception(int width, int height)
        {

            void act() => new Plateau(width, height);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);


            Assert.NotNull(exception.Message);
            Assert.StartsWith("Invalid length", exception.Message);
        }
        public static IEnumerable<object[]> Get_Params_For_Should_Throw_Exception()
        {
            yield return new object[] { 0, 0 };
            yield return new object[] { 1, 0 };
            yield return new object[] { 0, 1 };
            yield return new object[] { 0, -1 };
            yield return new object[] { -1, 0 };
            yield return new object[] { -1, -1 };
        }
    }
}
