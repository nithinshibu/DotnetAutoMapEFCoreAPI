using DotnetAutoMapEFCoreAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetAutoMapEFCoreAPI.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Create the DbSet
        public DbSet<Ticket> Tickets { get; set; }
    }
}
