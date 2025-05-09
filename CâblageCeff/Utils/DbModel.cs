using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CâblageCeff.Models;
using Microsoft.EntityFrameworkCore;

namespace CâblageCeff.Utils
{
    public class DbModel : DbContext
    {
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Patch> Patchs { get; set; }
        public string path { get; }

        public DbModel()
            => path = "P:\\Partage Tous-Tous\\Projet_CâblageCeff\\Cablage.db";
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite($"Data Source={path}");
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patch>()
                .HasOne(p => p.Panel)
                .WithMany(p => p.Patchs)
                .HasForeignKey(p => p.PanelId);

            modelBuilder.Entity<Patch>()
                .Property(p => p.Nom)
                .IsRequired();

            modelBuilder.Entity<Panel>()
                .Property(p => p.NbrPort)
                .HasDefaultValue(0);

            modelBuilder.Entity<Panel>()
                .Property(p => p.Nom)
                .IsRequired();
        }
    }
}
