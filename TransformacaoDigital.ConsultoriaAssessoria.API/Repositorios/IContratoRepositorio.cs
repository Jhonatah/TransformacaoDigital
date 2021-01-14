using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Dtos;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios
{
    public interface IContratoRepositorio
    {
        Task<Paginador<object>> ListarAsync(int pagina);
        Task<object> ListarTiposAsync();
        Task<object> ListarEmpresasAsync(Guid contratoId);

        Task<object> LerPorIdAsync(Guid id);

        Task CadastrarAsync(Contrato contrato);
        Task AlterarAsync(Guid id, Contrato contrato);
    }
}