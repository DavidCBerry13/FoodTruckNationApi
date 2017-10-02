using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;

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
            missingColors.Count.ShouldBeEquivalentTo(2);
            missingColors.Should().Contain("Blue");
            missingColors.Should().Contain("White");
            missingColors.Should().NotContain("Red");
            missingColors.Should().NotContain("Green");
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
            missingItems.Count.ShouldBeEquivalentTo(2);
            missingItems.Should().Contain(c => c.Color == "Silver" ); // Has a silver car
            missingItems.Should().Contain(c => c.Color == "Yellow");  // Has a yellow car
            missingItems.Should().NotContain(c => c.Color == "Red"); // No Red Cars
            missingItems.Should().NotContain(c => c.Color == "Green");  // No Green Cars
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
            missingItems.Count.ShouldBeEquivalentTo(2);
            missingItems.Should().Contain("White"); // There are no white cars
            missingItems.Should().Contain("Blue"); // There are no blue cars
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
            matchingItems.Count.ShouldBeEquivalentTo(2);
            matchingItems.Should().NotContain(car => car.Color == "Silver"); // Silver is not in the color list
            matchingItems.Should().NotContain(car => car.Color == "Yellow"); // Yellow is not in the color list

            matchingItems.Should().Contain(car => car.Color == "Red", "A red car is in the list and red is in the list of colors");
            matchingItems.Should().Contain(car => car.Color == "Green", "A green car is in the list and green is in the list of colors");
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
            matchingItems.Count.ShouldBeEquivalentTo(2);

            matchingItems.Should().Contain("Red", "A red car is in the list and red is in the list of colors");
            matchingItems.Should().Contain("Green", "A green car is in the list and green is in the list of colors");

            matchingItems.Should().NotContain("Blue", "because there are no Blue cars");
            matchingItems.Should().NotContain("White", "because there are no white cars");
        }



    }
}
