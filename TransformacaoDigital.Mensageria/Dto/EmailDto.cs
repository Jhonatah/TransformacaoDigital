namespace TransformacaoDigital.Mensageria.Dto
{
    public class EmailDto
    {
        /// <summary>
        ///     Separados por ;
        /// </summary>
        public string Destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
    }
}
