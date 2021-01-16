using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TransformacaoDigital.Autenticacao.API
{
    public class ConfiguracaoJWTSetting
    {
        private const string _secret = "fjspdfj0fJPOFJIPEHF3F0HPIfoiEBHFKJF3nflknljfdbkldfnWEBF";

        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string[] ValidAudiences { get; set; }
        public string SecurityKey { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
