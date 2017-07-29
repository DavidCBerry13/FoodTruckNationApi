using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Test
{
    public class ResourceAlreadyExistsExceptionTest
    {
        [Fact]
        public void ConstructorMessageShouldPopulateExceptionMessage()
        {
            var message = "This is a really interesting message";
            var exception = new ResourceAlreadyExistsException(message);

            Assert.Equal(message, exception.Message);
        }

    }
}
