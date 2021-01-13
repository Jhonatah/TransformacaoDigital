using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes
{
    public class ConsultoriaAssessoriaContexto : DbContext
    {
        public ConsultoriaAssessoriaContexto([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected ConsultoriaAssessoriaContexto()
        {
        }

        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<TipoContrato> TiposContratos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaContrato> EmpresasContratos { get; set; }
        public DbSet<TipoEmpresa> TiposEmpresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contrato>(configuration => configuration.ToTable("Contrato", "ConsultoriasAssessorias"));
            modelBuilder.Entity<TipoContrato>(buildAction => buildAction.ToTable("TipoContrato", "ConsultoriasAssessorias"));
            modelBuilder.Entity<Empresa>(configuration => configuration.ToTable("Empresa", "ConsultoriasAssessorias"));
            modelBuilder.Entity<EmpresaContrato>(configuration => configuration.ToTable("EmpresaContrato", "ConsultoriasAssessorias"));
            modelBuilder.Entity<TipoEmpresa>(buildAction => buildAction.ToTable("TipoEmpresa", "ConsultoriasAssessorias"));
        }
    }
}
