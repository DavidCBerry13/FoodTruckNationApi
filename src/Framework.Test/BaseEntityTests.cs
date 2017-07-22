using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Test
{
    public class BaseEntityTests
    {

        [Fact]
        public void TestSetObjectModifiedLeavesNewObjectInNewState()
        {
            // Arrange
            BaseEntity entity = new BaseEntity(ObjectState.NEW);

            // Act
            entity.SetObjectModified();

            // Assert
            Assert.Equal(ObjectState.NEW, entity.ObjectState);
        }

        [Fact]
        public void TestSetObjectModifiedChangesStateForUnchangedObject()
        {
            // Arrange
            BaseEntity entity = new BaseEntity(ObjectState.UNCHANGED);

            // Act
            entity.SetObjectModified();

            // Assert
            Assert.Equal(ObjectState.MODIFIED, entity.ObjectState);
        }


    }
}
