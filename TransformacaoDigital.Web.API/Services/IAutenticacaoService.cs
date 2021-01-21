using System.Threading.Tasks;
using TransformacaoDigital.Web.API.Services.Dtos;

namespace TransformacaoDigital.Web.API.Services
{
    public interface IAutenticacaoService
    {
        Task<ResponseObj<object>> LoginAsync(string email, string senha);
    }
}