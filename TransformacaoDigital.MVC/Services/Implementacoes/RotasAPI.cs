namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public sealed class RotasAPI
    {
        public const string Autenticacao_Usuario = "autenticacao-api/usuario";

        public const string ProcessosIndustriais_Colaboradores = "processosindustriais-api/usuarios";
        public const string ProcessosIndustriais_Colaboradores_EmailExiste = "processosindustriais-api/usuarios/emailexiste/{0}";
        public const string ProcessosIndustriais_Colaboradores_LerPorID = "processosindustriais-api/usuarios/{0}";
        public const string ProcessosIndustriais_Colaboradores_Desativar = "processosindustriais-api/usuarios/{0}";
        public const string ProcessosIndustriais_Colaboradores_Retivar = "processosindustriais-api/usuarios/{0}/reativar";

        public const string ProcessosIndustriais_TiposColaboradores = "processosindustriais-api/tipousuarios";
        public const string ProcessosIndustriais_Perfis = "processosindustriais-api/perfis";

        public const string Normas = "normas-api/normas";
        public const string Normas_PorId = "normas-api/normas/{0}";
        public const string Normas_Tipos = "normas-api/tiponormas";
    }
}