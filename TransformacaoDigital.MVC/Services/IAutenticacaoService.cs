using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos.AutenticacaoDtos;

namespace TransformacaoDigital.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<TokenDto> LoginAsync(string email, string senha);
    }
}