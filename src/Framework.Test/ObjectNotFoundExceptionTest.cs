using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Framework.Exceptions;

namespace Framework.Test
{
    public class ObjectNotFoundExceptionTest
    {

        [Fact]
        public void ConstructorMessageShouldPopulateExceptionMessage()
        {
            var message = "This is a really interesting message";
            var exception = new ObjectNotFoundException(message);

            Assert.Equal(message, exception.Message);
        }

    }
}
