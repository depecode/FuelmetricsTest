using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelMetricsTest.Model;

namespace FuelMetricsTest.Model
{
    public class ParkDbContext : DbContext
    {
      
            public ParkDbContext(DbContextOptions<ParkDbContext> options)
                : base(options)
            {
            }
            public DbSet<Park> Parks { get; set; }
            public DbSet<FuelMetricsTest.Model.Rule> Rule { get; set; }

    }
}
