using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

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
            entity.ObjectState.Should().Be(ObjectState.NEW);
        }

        [Fact]
        public void TestSetObjectModifiedChangesStateForUnchangedObject()
        {
            // Arrange
            BaseEntity entity = new BaseEntity(ObjectState.UNCHANGED);

            // Act
            entity.SetObjectModified();

            // Assert
            entity.ObjectState.Should().Be(ObjectState.MODIFIED);
        }


    }
}
