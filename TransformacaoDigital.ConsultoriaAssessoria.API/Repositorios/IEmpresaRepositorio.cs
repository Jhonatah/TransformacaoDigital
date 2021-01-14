using System;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Dtos;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios
{
    public interface IEmpresaRepositorio
    {
        Task<Paginador<object>> ListarAsync(int pagina);
        Task<object> ListarTiposAsync();

        Task<object> LerPorIdAsync(Guid id);
        Task<bool> CNPJExisteAsync(string cnpj);

        Task CadastrarAsync(Empresa empresa);
        Task AlterarAsync(Guid id, Empresa empresa);
    }
}