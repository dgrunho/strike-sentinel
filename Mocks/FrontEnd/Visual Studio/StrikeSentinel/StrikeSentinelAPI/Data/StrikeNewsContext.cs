using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StrikeSentinelAPI.Models
{
    public class StrikeNewsContext : DbContext
    {
        public StrikeNewsContext (DbContextOptions<StrikeNewsContext> options)
            : base(options)
        {
        }

        public DbSet<StrikeSentinelAPI.Models.StrikeNews> StrikeNews { get; set; }
    }
}
