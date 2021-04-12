using TrailAPI.Models;
using Xunit;

namespace TrailAPI.Tests
{
    public class CommandModelTest
    {
        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange
            var testCommand = new CommandModel
            {
             HowTo= "Do something awesome",
             Platform= "xUnit",
             CommandLine="dotnet test"
            };

            //act
            testCommand.HowTo="Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests",testCommand.HowTo);
        }

    }
}