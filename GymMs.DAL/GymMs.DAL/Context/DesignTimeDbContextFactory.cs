using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymMs.DAL.GymMs.DAL.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GymDbContext> 
    {
        public GymDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GymDbContext>();
            optionsBuilder.UseSqlServer("server=.;database=GymDb;trusted_connection=true;encrypt=false;MultipleActiveResultSets=true");
            return new GymDbContext(optionsBuilder.Options);
        }
    }
}
