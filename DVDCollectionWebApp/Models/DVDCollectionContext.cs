using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;

namespace DVDCollectionWebApp.Models
{
    public class DVDCollectionContext : DbContext
    {
        public DbSet<DVD> DVDs { get; set; }

        public DVDCollectionContext(DbContextOptions<DVDCollectionContext> options)
            : base(options)
        {
        }
    }
}

