namespace TransformacaoDigital.Mensageria
{
    internal class ConfigurationServerRabbitMQ
    {
        private static ConfigurationServerRabbitMQ _instancia;

        public static ConfigurationServerRabbitMQ Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ConfigurationServerRabbitMQ();

                return _instancia;
            }
        }

        public static void SetConfig(string hostName, int port, string userName, string password)
        {
            _instancia = new ConfigurationServerRabbitMQ
            { 
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password
            };
        }

        public string HostName { get; private set; } = "localhost";
        public int Port { get; private set; } = 5672;
        public string UserName { get; private set; } = "guest";
        public string Password { get; private set; } = "guest";
    }
}
