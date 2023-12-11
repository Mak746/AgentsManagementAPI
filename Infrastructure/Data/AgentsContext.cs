using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AgentsContext : DbContext
    {
        public AgentsContext(DbContextOptions<AgentsContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
    }
}