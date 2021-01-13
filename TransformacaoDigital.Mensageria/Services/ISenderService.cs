namespace TransformacaoDigital.Mensageria.Services
{
    public interface ISenderService
    {
        void Send(QueueEnum queue, object objMessage);
    }
}