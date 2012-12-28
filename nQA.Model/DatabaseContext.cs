using System.Data.Entity;
using nQA.Model.Entities;

namespace nQA.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("nQADatabase")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}