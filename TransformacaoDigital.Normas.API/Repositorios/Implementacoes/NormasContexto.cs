using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TransformacaoDigital.Normas.API.Models;

namespace TransformacaoDigital.Normas.API.Repositorios.Implementacoes
{
    public class NormasContexto : DbContext
    {
        public NormasContexto([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected NormasContexto()
        {
        }

        public DbSet<Norma> Normas { get; set; }
        public DbSet<TipoNorma> TiposNormas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TipoNorma>(configuration => configuration.ToTable("TipoNorma", "Normas"));
            modelBuilder.Entity<Norma>(buildAction => buildAction.ToTable("Norma", "Normas"));
        }
    }
}