using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.DB
{
    public class MyContext : DbContext
    {
        private string ConectionString =
            "server=***.***.**.***; database=***; user=***; password=***;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConectionString);
        }
        public DbSet<Acaunt> Acaunts { get; set; }
        public DbSet<EnterControl> EnterControls { get; set; }
    }
}
