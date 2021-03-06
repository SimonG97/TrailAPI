using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TrailAPI.Controllers;
using TrailAPI.Models;
using TrailAPI.Profiles;
using TrailAPI.Respository;
using Xunit;
using TrailAPI.Dtos;
using System;

namespace TrailAPI.Tests
{
    public class TrailControllerTests : IDisposable
    {
        Mock<ICommandRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public TrailControllerTests(){
          mockRepo= new Mock<ICommandRepo>();
          realProfile= new CommandsProfile();
          configuration = new MapperConfiguration(cfg=>
          cfg.AddProfile(realProfile));
          mapper= new Mapper(configuration);
          
        }
        
        public void Dispose(){
            mockRepo= null;
            mapper= null;
            configuration=null;
            realProfile=null;
        }

        [Fact]
        public void GetCommandItems_Returns200Ok_WhenDBIsEmpty(){
        //arrange
         mockRepo.Setup(Respository=>Respository.GetAllCommands()).Returns(GetCommands(0));
         var controller= new TrailController(mockRepo.Object,mapper);
        //Act
          var result=controller.GetAllCommands();
        //assert
          Assert.IsType<OkObjectResult>(result.Result);
        }
        
        private List<CommandModel> GetCommands(int num)
        {
            var commands= new List<CommandModel>();
            if(num>0){
                commands.Add(new CommandModel{
                    Id=0,
                    HowTo="How to generate a migration",
                    CommandLine="dotnet ef migrations add <Name of Migration>",
                    Platform=".Net Core EF"
                });
            }
            return commands;
        }
        
        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResouce()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller= new TrailController(mockRepo.Object,mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);

        }

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResource(){
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new TrailController(mockRepo.Object,mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new TrailController(mockRepo.Object,mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);

        }

        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided(){
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=>null);
            var controller = new TrailController(mockRepo.Object, mapper);

            //Act
            var result= controller.GetCommandById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);


        }

        [Fact]
        public void GetCommandById_Returns200OK_WhenValidIDProvided(){
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new CommandModel{
              Id=1,
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
            });
            var controller = new TrailController(mockRepo.Object, mapper);

            //Act
            var result=controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_ReturnsCorrectType_WhenValidIDProvided(){
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new CommandModel{
              Id=1,
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
            });
            var controller = new TrailController(mockRepo.Object, mapper);

            //Act
            var result=controller.GetCommandById(1);

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreateCommand_ReturnsCorrecResourceType_WhenValidObjectSubmitted(){
          //Arrange
          mockRepo.Setup(repo =>
          repo.GetCommandById(1)).Returns(new CommandModel{
              Id=1,
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
          });
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.CreateCommand(new CommandCreateDto{});

          //Assert
          Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
          //Arrange
          mockRepo.Setup(repo =>
          repo.GetCommandById(1)).Returns(new CommandModel{
              Id=1,
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
          });
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.CreateCommand(new CommandCreateDto{});

          //Assert
          Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        
        
        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
          //Arrange
          mockRepo.Setup(repo =>
          repo.GetCommandById(1)).Returns(new CommandModel{
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
          });
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.UpdateCommand(1,new CommandUpdateDto{});

          //Assert
          Assert.IsType<NoContentResult>(result);
        }

        
        [Fact]
        public void CreateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
          //Arrange
         mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=>null);
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.UpdateCommand(0,new CommandUpdateDto{});

          //Assert
          Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
          //Arrange
          mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=>null);
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.PartialCommandUpdate(0, 
          new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto>{});

          //Assert
          Assert.IsType<NotFoundResult>(result);

        }

         [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
          //Arrange
          mockRepo.Setup(repo =>
          repo.GetCommandById(1)).Returns(new CommandModel{
              HowTo="mock",
              Platform="Mock",
              CommandLine="Mock"
          });
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.DeleteCommand(1);

          //Assert
          Assert.IsType<NoContentResult>(result);
        }

         [Fact]
        public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
          //Arrange
         mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(()=>null);
          var controller= new TrailController(mockRepo.Object,mapper);

          //Act
          var result= controller.DeleteCommand(0);

          //Assert
          Assert.IsType<NotFoundResult>(result);
        }
    }
}