using Framework.ApiUtil.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.ApiUtil.Tests.Results
{
    public class ConflictResultTests
    {
        [Fact]
        public void ConflictResultHasStatusCode409()
        {
            // Arrange
            var result = new ConflictResult();

            // Assert
            Assert.Equal(409, result.StatusCode);
        }

        [Fact]
        public void ConflictObjectResultHasStatusCode409()
        {
            // Arrange
            var result = new ConflictObjectResult("Some Message");

            // Assert
            Assert.Equal(409, result.StatusCode);
        }

    }
}
