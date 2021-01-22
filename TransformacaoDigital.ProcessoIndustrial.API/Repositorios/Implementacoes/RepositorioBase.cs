namespace TransformacaoDigital.ProcessoIndustrial.API.Repositorios.Implementacoes
{
    public abstract class RepositorioBase
    {
        protected ProcessosIndustriaisContexto Contexto { get; init; }

        public RepositorioBase(ProcessosIndustriaisContexto contexto)
        {
            Contexto = contexto;
        }
    }
}