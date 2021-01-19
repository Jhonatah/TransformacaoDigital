using System.Threading.Tasks;

namespace TransformacaoDigital.Mensageria.Services
{
    public interface IReceiveService
    {
        void Subscribe(IEventHandler handler);
    }
}
