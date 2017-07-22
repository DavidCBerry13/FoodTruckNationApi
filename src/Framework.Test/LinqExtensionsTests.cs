using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace Framework.Test
{
    public class LinqExtensionsTests
    {


        private class Car
        {
            public String Make { get; set; }
            public String Model { get; set; }
            public String Color { get; set; }
        }

        [Fact]
        public void TestWhereNotExistsSimpleStrings()
        {
            // Arrange
            var colors = new List<String>() { "Red", "Blue", "Green", "White" };
            var target = new List<String>() { "Red", "Green" };

            // Act
            var missingColors = colors.WhereNotExists(target, (x, y) => (x == y)).ToList();

            // Assert
            Assert.Equal(2, missingColors.Count);
            Assert.True(missingColors.Contains("Blue"));
            Assert.True(missingColors.Contains("White"));
        }


        [Fact]
        public void TestWhereNotExistsObjectsNotInStringList()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car() { Make = "Ford", Model="Escape", Color = "Silver"},
                new Car() { Make = "Mazda", Model="CX5", Color = "Red"},
                new Car() { Make = "Chevrolet", Model="Corvette", Color = "Yellow"},
                new Car() { Make = "Toyota", Model="Camry", Color = "Green"}
            };
            var colors = new List<String>() { "Red", "Blue", "Green", "White" };
            

            // Act
            var missingItems = cars.WhereNotExists(colors, (x, y) => (x.Color == y)).ToList();

            // Assert
            Assert.Equal(2, missingItems.Count);
            Assert.Equal(1, missingItems.Count(c => c.Color == "Silver"));
            Assert.Equal(1, missingItems.Count(c => c.Color == "Yellow"));
            Assert.Equal(0, missingItems.Count(c => c.Color == "Red"));
            Assert.Equal(0, missingItems.Count(c => c.Color == "Green"));
        }


        [Fact]
        public void TestWhereNotExistsStringsNotInObjectList()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car() { Make = "Ford", Model="Escape", Color = "Silver"},
                new Car() { Make = "Mazda", Model="CX5", Color = "Red"},
                new Car() { Make = "Chevrolet", Model="Corvette", Color = "Yellow"},
                new Car() { Make = "Toyota", Model="Camry", Color = "Green"}
            };
            var colors = new List<String>() { "Red", "Blue", "Green", "White" };


            // Act
            var missingItems = colors.WhereNotExists(cars, (x, y) => (x == y.Color)).ToList();

            // Assert
            Assert.Equal(2, missingItems.Count);
            Assert.True(missingItems.Contains("White"));
            Assert.True(missingItems.Contains("Blue"));
        }



        [Fact]
        public void TestWhereExistsObjectsInStringList()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car() { Make = "Ford", Model="Escape", Color = "Silver"},
                new Car() { Make = "Mazda", Model="CX5", Color = "Red"},
                new Car() { Make = "Chevrolet", Model="Corvette", Color = "Yellow"},
                new Car() { Make = "Toyota", Model="Camry", Color = "Green"}
            };
            var colors = new List<String>() { "Red", "Blue", "Green", "White" };


            // Act
            var matchingItems = cars.WhereExists(colors, (x, y) => (x.Color == y)).ToList();

            // Assert
            Assert.Equal(2, matchingItems.Count);
            Assert.Equal(0, matchingItems.Count(c => c.Color == "Silver"));
            Assert.Equal(0, matchingItems.Count(c => c.Color == "Yellow"));
            Assert.Equal(1, matchingItems.Count(c => c.Color == "Red"));
            Assert.Equal(1, matchingItems.Count(c => c.Color == "Green"));
        }


        [Fact]
        public void TestWhereExistsStringsInObjectList()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car() { Make = "Ford", Model="Escape", Color = "Silver"},
                new Car() { Make = "Mazda", Model="CX5", Color = "Red"},
                new Car() { Make = "Chevrolet", Model="Corvette", Color = "Yellow"},
                new Car() { Make = "Toyota", Model="Camry", Color = "Green"}
            };
            var colors = new List<String>() { "Red", "Blue", "Green", "White" };


            // Act
            var matchingItems = colors.WhereExists(cars, (x, y) => (x == y.Color)).ToList();

            // Assert
            Assert.Equal(2, matchingItems.Count);
            Assert.True(matchingItems.Contains("Red"));
            Assert.True(matchingItems.Contains("Green"));
        }



    }
}
