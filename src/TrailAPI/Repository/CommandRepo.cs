using System.Collections.Generic;
using TrailAPI.Models;

namespace TrailAPI.Respository
{

    public class CommandRepo : ICommandRepo
    {
        public IEnumerable<CommandModel> GetAllCommands()
        {
            var commands = new List<CommandModel>
        {
            new CommandModel{
                Id=0, HowTo="How to generate a migration",
                CommandLine="dotnet ef migrations add <Name of Migration>",
                Platform=".Net Core EF"
                },
                new CommandModel
                {
                  Id=1, HowTo="Run Migrations",
                  CommandLine="dotnet ef database update",
                  Platform=".Net Core EF"
                },
                new CommandModel{
                   Id=2, HowTo="List active migrations",
                   CommandLine="dotnet ef migrations list",
                   Platform=".Net Core EF"
                }
        };
            return commands;
        }

        public CommandModel GetCommandById(int id)
        {
            return new CommandModel
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            };
        }
        public void CreateCommand (CommandModel cmd){
            throw new System.NotImplementedException();
        }
        public void DeleteCommand (CommandModel cmd){
            throw new System.NotImplementedException();
        }
        public bool SaveChanges(){
            throw new System.NotImplementedException();
        }
        public void UpdateCommand (CommandModel cmd){
            throw new System.NotImplementedException();
        }
    }
}