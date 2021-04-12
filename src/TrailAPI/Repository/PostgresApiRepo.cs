using System;
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
            if(cmd==null){
               throw new ArgumentNullException(nameof(cmd));
            }
           _context.CommandItems.Add(cmd);
        }

        public void DeleteCommand(CommandModel cmd)
        {
            if(cmd==null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.CommandItems.Remove(cmd);
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
            return (_context.SaveChanges()>=0);
        }

        public void UpdateCommand(CommandModel cmd)
        {
            //no code
        }
    }
}