using Crypto2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptov2.DataBaseContext
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options) : base(options)
        {
        }

        public DbSet<Crypto> Crypto { get; set; }
    }
}
