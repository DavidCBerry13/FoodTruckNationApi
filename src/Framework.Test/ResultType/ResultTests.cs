using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Framework.Entity;
using Framework.ResultType;

namespace Framework.Test.ResultType
{
    public class ResultTests
    {


        [Fact]
        public void Test_CreatingSuccessResult_HasSuccessAsTrue()
        {
            // Arrange and Act
            var result = Result.Success();

            // Assert
            result.IsSuccess.Should().Be(true);
        }


        [Fact]
        public void Test_CreatingTypedSuccessResult_HasSuccessAsTrue()
        {
            // Arrange and Act
            var result = Result.Success<string>("George Washington");

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().BeOfType<string>();
            result.Value.Should().Be("George Washington");
        }

        [Fact]
        public void Test_CreatingFailureResult_HasSuccessAsFalseAndMessage()
        {
            // Arrange
            var message = "It did not work";

            // Act
            var result = Result.Failure(message);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Message.Should().Be(message);
        }

        [Fact]
        public void Test_CreatingFailureResultWithErrorClass_HasSuccessAsFalseAndMessage()
        {
            // Arrange
            var message = "The droids you were looking for were not found";

            // Act
            var result = Result.Failure<ObjectNotFoundError>(new ObjectNotFoundError(message));

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().BeOfType<ObjectNotFoundError>();
            result.Error.Message.Should().Be(message);
        }


        [Fact]
        public void Test_CreatingTypedFailureResult_HasSuccessAsFalse_Message_AndNoValue()
        {
            // Arrange
            var message = "It did not work";

            // Act
            var result = Result.Failure<string>(message);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Message.Should().Be(message);
            result.Value.Should().BeNull();
        }


        [Fact]
        public void Test_CreatingTypedFailureResultWithErrorClass_HasSuccessAsFalseAndMessage_AndNoValue()
        {
            // Arrange
            var message = "The droids you were looking for were not found";

            // Act
            var result = Result.Failure<string, ObjectNotFoundError>(new ObjectNotFoundError(message));

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().BeOfType<ObjectNotFoundError>();
            result.Error.Message.Should().Be(message);
            result.Value.Should().BeNull();
        }

    }
}
