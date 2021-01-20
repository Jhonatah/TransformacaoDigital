using System.Threading.Tasks;

namespace TransformacaoDigital.Web.API.Services
{
    public interface IAutenticacaoService
    {
        Task LoginAsync(string email, string senha);
    }
}