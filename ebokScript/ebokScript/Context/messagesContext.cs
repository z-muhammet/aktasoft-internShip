using ebokScript.Models;
using System.Data.Entity;
using ebokScript.Models;

namespace ebokScript.Context
{
    public class messagesContext : DbContext
    {
        public DbSet<messageScript>? message { get; set; }
        public DbSet<PageScript>? pages { get; set; }

    }
}
