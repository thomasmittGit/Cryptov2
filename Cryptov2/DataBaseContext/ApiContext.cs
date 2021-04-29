using Cryptov2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptov2.DataBaseContext
{
    public class ApiContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Crypto> Crypto { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Keys> Keys { get; set; }
    }
}
