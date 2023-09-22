using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PetLife.Models;

namespace PetLife.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Vacina> Vacina { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
    }
}
