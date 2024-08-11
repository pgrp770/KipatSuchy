using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KipatSuchy.Models;

namespace KipatSuchy.Data
{
    public class KipatSuchyContext : DbContext
    {
        public KipatSuchyContext (DbContextOptions<KipatSuchyContext> options)
            : base(options)
        {
        }

        public DbSet<Manager> Manager { get; set; } = default!;
        public DbSet<Threat> Threats { get; set; } = default!;
        public DbSet<Response> Responses { get; set; } = default!;
    }
}
