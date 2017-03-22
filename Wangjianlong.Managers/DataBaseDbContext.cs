using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class DataBaseDbContext:DbContext
    {
        public DataBaseDbContext() : base("name=DbConnection")
        {
            Database.SetInitializer<DataBaseDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Daily> Dailys { get; set; }
        public DbSet<Fitment> Fitments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<FitmentItem> FitmentItems { get; set; }
    }
}
