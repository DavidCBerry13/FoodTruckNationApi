using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Test
{
    public class StringCollectionValidationAttributeTests
    {

        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenWrongTypePassed()
        {
            // Arrange
            String regex = @"^\w{1,10}$";
            var value = "Something that is not a collection of strings";

            // Act
            var validation = new StringCollectionValidationAttribute(regex);

            // Assert            
            Assert.Throws(typeof(InvalidOperationException), () => validation.IsValid(value));
        }



        [Fact]
        public void TestListValidatesWhenAllObjectsAreValid()
        {
            // Arrange
            String regex = @"^\w{1,10}$";
            var items = new List<String>() { "Red", "White", "Blue" };

            // Act
            var validation = new StringCollectionValidationAttribute(regex);
            bool isValid = validation.IsValid(items);

            // Assert            
            Assert.True(isValid);
        }


        [Fact]
        public void TestListFailsValidationWhenAnObjectsIsValid()
        {
            // Arrange
            String regex = @"^\w{1,10}$";
            var items = new List<String>() { "New York", "Chicago", "Dallas" };

            // Act
            var validation = new StringCollectionValidationAttribute(regex);
            bool isValid = validation.IsValid(items);

            // Assert            
            Assert.False(isValid);
        }



        [Fact]
        public void TestListPassesValidationWhenEmptyArrayIsPassed()
        {
            // Arrange
            String regex = @"^\w{1,10}$";
            String[] items = new String[] { };

            // Act
            var validation = new StringCollectionValidationAttribute(regex);
            bool isValid = validation.IsValid(items);

            // Assert            
            Assert.True(isValid);
        }


        [Fact]
        public void IsvalidShouldThrowInvalidOperationExceptionWhenNullValueIsPassed()
        {
            // Arrange
            String regex = @"^\w{1,10}$";
            String[] items = null;

            // Act
            var validation = new StringCollectionValidationAttribute(regex);

            // Assert            
            Assert.Throws(typeof(InvalidOperationException), () => validation.IsValid(items));
        }


    }
}
