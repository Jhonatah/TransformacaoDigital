using System.Net.Http;
using System.Threading.Tasks;
using TransformacaoDigital.Library;
using TransformacaoDigital.Library.Enumerados;
using TransformacaoDigital.Web.API.RoutesApis;
using TransformacaoDigital.Web.API.Services.Dtos;

namespace TransformacaoDigital.Web.API.Services.Implementacoes
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        public AutenticacaoService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, Enums.HttpClientesEnums.Autenticacao) { }


        public async Task<ResponseObj<object>> LoginAsync(string email, string senha)
        {
            var token = new GeneratorTokenLogin(email, senha);
            return await PostAsync<object>(RoutesAutenticacao.LOGIN, new { token.Token });
        }
    }

    class GeneratorTokenLogin
    {
        public GeneratorTokenLogin(string login, string senha)
        {
            GerarToken(login, senha);
        }

        private void GerarToken(string login, string senha)
        {
            var temp = string.Concat(login, "|[[.]]|", senha);
            Token = Encriptador.Get().Encriptar(EncriptEnum.c7b70d19b7db400c84de2b570b49c1fd.GetName(), temp);
        }

        public string Token { get; private set; }
    }
}
