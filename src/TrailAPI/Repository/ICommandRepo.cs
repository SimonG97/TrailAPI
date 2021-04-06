using System.Collections.Generic;
using TrailAPI.Models;

namespace TrailAPI.Respository
{
    public interface ICommandRepo
    {
        IEnumerable<CommandModel> GetAllCommands();
        CommandModel GetCommandById(int id);
    }
}