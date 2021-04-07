using Microsoft.EntityFrameworkCore;
using TrailAPI.Models;

namespace TrailAPI.Data{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext>options):base(options)
        {

        }
        public DbSet<CommandModel> CommandItems {get;set;}
    }
}