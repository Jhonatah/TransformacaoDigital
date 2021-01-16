using Newtonsoft.Json;
using TransformacaoDigital.Library.Dtos;
using TransformacaoDigital.Library.Enumerados;

namespace TransformacaoDigital.Library
{
    public class GeradorTokenCrossServices
    {
        public string GetToken(string tokenAutorizacao, string origemDominio)
        {
            var cross = new CrossToken(tokenAutorizacao, origemDominio);
            var serializado = JsonConvert.SerializeObject(cross);

            return Encriptador.Get().Encriptar(EncriptEnum.ea16acb359604973ba2b17498ba2d8dc.GetName(), serializado);
        }
    }
}
