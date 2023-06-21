using Microsoft.EntityFrameworkCore;
using MVC_CRUD_using_asp.net_core__.net_6_.Models.Domain;

namespace MVC_CRUD_using_asp.net_core__.net_6_.Repositary
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ActorModel> Actors { get; set; }
    }
}
