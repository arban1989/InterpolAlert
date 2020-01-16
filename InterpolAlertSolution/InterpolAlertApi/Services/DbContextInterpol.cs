using InterpolAlertApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class DbContextInterpol : DbContext
    {
        public DbContextInterpol(DbContextOptions<DbContextInterpol> options)
            : base(options){}

        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Autore> Autori { get; set; }
        public DbSet<AutoriEventi> AutoriEventi { get; set; }
        public DbSet<Esito> Esiti { get; set; }
        public DbSet<Fazione> Fazioni { get; set; }
        public DbSet<Gravita> Gravita { get; set; }
        public DbSet<Localita> Localita { get; set; }
        public DbSet<Mandante> Mandanti { get; set; }
        public DbSet<TipoEvento> TipoEventi { get; set; }
        public DbSet<TipoVittima> TipoVittima { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoriEventi>()
                        .HasKey(ae => new { ae.IdAutore, ae.IdEvento });
            modelBuilder.Entity<AutoriEventi>()
                        .HasOne(a => a.Autore)
                        .WithMany(ae => ae.AutoriEventi)
                        .HasForeignKey(a => a.IdAutore);
            modelBuilder.Entity<AutoriEventi>()
                        .HasOne(e => e.Evento)
                        .WithMany(ae => ae.AutoriEventi)
                        .HasForeignKey(e => e.IdEvento);

        }
    }
}
