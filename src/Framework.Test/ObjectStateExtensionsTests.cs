using System;
using Xunit;

namespace Framework.Test
{
    public class ObjectStateExtensionsTests
    {


        [Fact]
        public void TestNewStateIsActive()
        {
            // Arrange + Act
            var isActive = ObjectState.NEW.IsActiveState();

            // Assert
            Assert.True(isActive);
        }


        public void TestUnchangedStateIsActive()
        {
            // Act
            var isActive = ObjectState.UNCHANGED.IsActiveState();

            // Assert
            Assert.True(isActive);
        }


        public void TestModifiedStateIsActive()
        {
            // Act
            var isActive = ObjectState.MODIFIED.IsActiveState();

            // Assert
            Assert.True(isActive);
        }


        public void TestDeletedStateIsNotActive()
        {
            // Act
            var isActive = ObjectState.DELETED.IsActiveState();

            // Assert
            Assert.False(isActive);
        }


    }
}
