using System.Collections.Generic;
using TrailAPI.Models;

namespace TrailAPI.Respository
{
    public interface ICommandRepo
    {
        void CreateCommand(CommandModel cmd);
        void DeleteCommand(CommandModel cmd);
        IEnumerable<CommandModel> GetAllCommands();
        CommandModel GetCommandById(int id);
        bool SaveChanges();
        void UpdateCommand(CommandModel cmd);
    }
}