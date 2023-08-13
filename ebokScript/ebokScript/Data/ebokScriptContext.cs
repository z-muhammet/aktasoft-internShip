using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebokScript.Models;

namespace ebokScript.Data
{
    public class ebokScriptContext : DbContext
    {
        public ebokScriptContext (DbContextOptions<ebokScriptContext> options)
            : base(options)
        {
        }

        public DbSet<ebokScript.Models.messageScript> messageScript { get; set; } = default!;
    }
}
