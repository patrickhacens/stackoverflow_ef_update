using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Db : DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public DbSet<AssetFile> AssetFiles { get; set; }

        public Db() : 
            base(new DbContextOptionsBuilder()
                .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=teste;Integrated Security=true")
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .LogTo(d=> Console.WriteLine(d))
                .Options)
        {
        }
    }
}
