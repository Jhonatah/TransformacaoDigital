using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TransformacaoDigital.Autenticacao.API.Models;

namespace TransformacaoDigital.Autenticacao.API.Repositorios.Implementacoes
{
    public class UsuariosContexto : DbContext
    {
        public UsuariosContexto([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected UsuariosContexto()
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(configuration => configuration.ToTable("Usuario", "Usuarios"));
            modelBuilder.Entity<Perfil>(buildAction => buildAction.ToTable("Perfil", "Usuarios"));
        }
    }
}
