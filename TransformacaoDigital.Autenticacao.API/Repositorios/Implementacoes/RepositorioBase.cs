namespace TransformacaoDigital.Autenticacao.API.Repositorios.Implementacoes
{
    public abstract class RepositorioBase
    {
        protected UsuariosContexto Contexto { get; init; }

        public RepositorioBase(UsuariosContexto contexto)
        {
            Contexto = contexto;
        }
    }
}
