using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cats_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cats_api.Data
{
    public class FelineContext : DbContext
    {
        //Klassen som ska användas för att representera datalagringen
        public DbSet<Feline> Felines { get; set; }
        public FelineContext(DbContextOptions options) : base(options)
        {
        }
    }
}