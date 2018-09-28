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

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Strike> Strike { get; set; }
        public virtual DbSet<StrikeNews> StrikeNews { get; set; }
        public virtual DbSet<StrikeStatus> StrikeStatus { get; set; }
        public virtual DbSet<User> User { get; set; }

    }
}
