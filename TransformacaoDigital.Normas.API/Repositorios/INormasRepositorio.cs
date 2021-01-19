using System;
using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.Dtos;
using TransformacaoDigital.Normas.API.Models;

namespace TransformacaoDigital.Normas.API.Repositorios
{
    public interface INormasRepositorio
    {
        Task<Paginador<object>> ListarAsync(int pagina);
        Task<object> ListarTiposNormasAsync();

        Task<object> LerPorIdAsync(Guid id);

        Task CadastrarAsync(Norma norma);
        void Cadastrar(Norma norma);
        Task AlterarAsync(Guid id, Norma norma);
    }
}