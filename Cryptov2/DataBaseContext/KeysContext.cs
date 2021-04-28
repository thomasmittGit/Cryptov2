using Cryptov2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptov2.DataBaseContext
{
    public class KeysContext : DbContext
    {
        public KeysContext(DbContextOptions<KeysContext> options) : base(options)
        {
        }

        public DbSet<Keys> Keys { get; set; }
    }
}
