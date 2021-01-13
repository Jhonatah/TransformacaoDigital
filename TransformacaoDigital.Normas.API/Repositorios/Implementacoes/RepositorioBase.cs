namespace TransformacaoDigital.Normas.API.Repositorios.Implementacoes
{
    public abstract class RepositorioBase
    {
        protected NormasContexto Contexto { get; init; }

        public RepositorioBase(NormasContexto contexto)
        {
            Contexto = contexto;
        }
    }
}
