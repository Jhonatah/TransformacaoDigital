using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Services
{
    public interface IUsuarioService
    {
        Guid GerUserId(string token);
    }

    public class UsuarioService : IUsuarioService
    {
        public Guid GerUserId(string token)
        {
            var handler = new JwtSecurityToken(token);

            var claim = handler.Claims.GetClaim(JwtRegisteredClaimNames.Jti);

            if (string.IsNullOrEmpty(claim)) return Guid.Empty;

            return Guid.Parse(claim);
        }
    }

    public static class JWTServiceExtensions
    {
        public static string GetClaim(this IEnumerable<Claim> claimsPrincipal, string jwtClaim)
        {
            var claim = claimsPrincipal.Where(c => c.Type == jwtClaim.ToString()).FirstOrDefault();

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }
    }
}
