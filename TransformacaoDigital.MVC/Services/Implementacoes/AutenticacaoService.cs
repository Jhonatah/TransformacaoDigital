using System;
using System.Text;
using System.Threading.Tasks;
using TransformacaoDigital.Library;
using TransformacaoDigital.Library.Enumerados;
using TransformacaoDigital.MVC.Services.Dtos.AutenticacaoDtos;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        public AutenticacaoService(IServicosHttpBase servicosBase) : base(servicosBase)
        {
        }

        public async Task<TokenDto> LoginAsync(string email, string senha)
        {
            var token = 
                Encriptador.Get().Encriptar(EncriptEnum.c7b70d19b7db400c84de2b570b49c1fd.GetName(), string.Concat(email, "|[[.]]|", senha));

            var tokenBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var response = await PostAsync<TokenDto>(string.Concat(RotasAPI.AutentucacaoUsuario, $"?token={tokenBase64}"), null);

            if (response.Sucesso == false)
            {
                ServicosBase.NotificacaoService.NotificarErro("Usuário ou senha inválido!");
                return null;
            }

            return response.ObjResult;
        }
    }
}