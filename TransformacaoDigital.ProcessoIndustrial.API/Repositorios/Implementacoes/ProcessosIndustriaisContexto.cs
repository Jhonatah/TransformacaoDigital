using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TransformacaoDigital.ProcessoIndustrial.API.Models;

namespace TransformacaoDigital.ProcessoIndustrial.API.Repositorios.Implementacoes
{
    public class ProcessosIndustriaisContexto : DbContext
    {
        public ProcessosIndustriaisContexto([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Perfil>(buildAction => buildAction.ToTable("Perfil", "Usuarios"));
            modelBuilder.Entity<TipoUsuario>(buildAction => buildAction.ToTable("TipoUsuario", "Usuarios"));
            modelBuilder.Entity<Usuario>(configuration => configuration.ToTable("Usuario", "Usuarios"));
        }
    }
}