using System.Collections.Generic;
using System.Linq;
using TrailAPI.Data;
using TrailAPI.Models;

namespace TrailAPI.Respository{
    public class PostgresApiRepo : ICommandRepo
    {
        private readonly DBContext _context;
        public PostgresApiRepo(DBContext context){
           _context=context;
        }
        public void CreateCommand(CommandModel cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(CommandModel cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommandModel> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }

        public CommandModel GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p=>p.Id==id);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(CommandModel cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}