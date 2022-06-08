using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace SynonymsAPI.Models
{
    public class SynonymsContext : DbContext
    {
        public SynonymsContext(DbContextOptions<SynonymsContext> options) : base(options)
        { }

        public DbSet<Synonyms> Synonims { get; set; }

    }

    
    public class Synonyms
    {
        private static int id_counter = 0;

        public int Id { get; set; }
        public string Word { get; set; }
        public string Synonym { get; set; }

        public Synonyms()
        {
            this.Id = System.Threading.Interlocked.Increment(ref id_counter);
        }
    }
}
