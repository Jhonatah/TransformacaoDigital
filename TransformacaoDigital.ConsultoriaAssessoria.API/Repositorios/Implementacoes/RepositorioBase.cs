namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes
{
    public abstract class RepositorioBase
    {
        protected ConsultoriaAssessoriaContexto Contexto { get; init; }

        public RepositorioBase(ConsultoriaAssessoriaContexto contexto)
        {
            Contexto = contexto;
        }
    }
}