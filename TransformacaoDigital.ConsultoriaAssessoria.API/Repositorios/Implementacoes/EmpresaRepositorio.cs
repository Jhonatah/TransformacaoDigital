using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Dtos;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes
{
    public class EmpresaRepositorio : RepositorioBase, IEmpresaRepositorio
    {
        public EmpresaRepositorio(ConsultoriaAssessoriaContexto contexto) : base(contexto)
        {
        }

        public Task AlterarAsync(Guid id, Empresa empresa)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarAsync(Empresa empresa)
        {
            throw new NotImplementedException();
        }

        public Task<object> LerPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Paginador<object>> ListarAsync(int pagina)
        {
            throw new NotImplementedException();
        }

        public Task<object> ListarTiposAsync()
        {
            throw new NotImplementedException();
        }
    }
}
