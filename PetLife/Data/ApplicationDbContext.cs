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
        public DbSet<PetLife.Models.Animal> Animal { get; set; }
        public DbSet<PetLife.Models.Vacina> Vacina { get; set; }
        public DbSet<PetLife.Models.Medicamento> Medicamento { get; set; }
    }
}
