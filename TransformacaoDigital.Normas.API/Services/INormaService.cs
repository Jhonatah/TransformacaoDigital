using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.ViewModels;

namespace TransformacaoDigital.Normas.API.Services
{
    public interface INormaService
    {
        Task CadastrarAsync(NormaViewModel norma);
        void Cadastrar(NormaViewModel norma);
    }
}