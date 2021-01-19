using System.Threading.Tasks;

namespace TransformacaoDigital.Mensageria.Services
{
    public interface IEventHandler
    {
        int ExecutionMillisecondsDelay { get; }
        QueueEnum QueueToProcess { get; }
        CommandResponse HandlerMessage(string message);
    }

    public struct CommandResponse
    {
        public static CommandResponse OK_Result => new CommandResponse(true);
        public static CommandResponse Fail_Result => new CommandResponse(false);

        public CommandResponse(bool success)
        {
            Ok = success;
            Fail = !success;
        }

        public bool Ok { get; private set; }
        public bool Fail { get; private set; }
    }
}