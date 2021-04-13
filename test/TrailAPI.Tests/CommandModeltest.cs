using System;
using TrailAPI.Models;
using Xunit;

namespace TrailAPI.Tests
{
    public class CommandModelTest: IDisposable
    {
         CommandModel testCommand;
         
        public CommandModelTest(){
             //Arrange
            testCommand = new CommandModel
            {
             HowTo= "Do something awesome",
             Platform= "Some platform",
             CommandLine="Some commandline"
            };
        }

        public void Dispose(){
            testCommand=null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
        
            //act
            testCommand.HowTo="Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests",testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform(){
              //act
            testCommand.Platform="xUnit";

            //Assert
            Assert.Equal("xUnit",testCommand.Platform);

        }

        
        [Fact]
        public void CanChangeCommandLine(){
              //act
            testCommand.CommandLine="dotnet test";

            //Assert
            Assert.Equal("dotnet test",testCommand.CommandLine);

        }

    }
}