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
        public const string ProcessosIndustriais_Colaboradores_AlterarSenha = "processosindustriais-api/usuarios/{0}/alterarsenha";

        public const string ProcessosIndustriais_TiposColaboradores = "processosindustriais-api/tipousuarios";
        public const string ProcessosIndustriais_Perfis = "processosindustriais-api/perfis";

        public const string Normas = "normas-api/normas";
        public const string Normas_PorId = "normas-api/normas/{0}";
        public const string Normas_Tipos = "normas-api/tiponormas";

        public const string ConsultoriaAssessoria_Empresa = "consultoriaassessoria-api/empresas";
        public const string ConsultoriaAssessoria_Empresa_Tipos = "consultoriaassessoria-api/tiposempresas";
        public const string ConsultoriaAssessoria_EmpresaPorId = "consultoriaassessoria-api/empresas/{0}";
        public const string ConsultoriaAssessoria_Empresa_ListarContratos = "consultoriaassessoria-api/empresas/{0}/contratos";

        public const string ConsultoriaAssessoria_Contrato = "consultoriaassessoria-api/contratos";

        public const string ConsultoriaAssessoria_TipoContrato = "consultoriaassessoria-api/tiposcontratos";
    }
}