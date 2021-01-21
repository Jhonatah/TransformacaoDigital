using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using TransformacaoDigital.Autenticacao.API.Dtos;
using TransformacaoDigital.Autenticacao.API.Models;

namespace TransformacaoDigital.Autenticacao.API.Services
{
    public class JWTService
    {
        public JWTService(
            IOptions<ConfiguracaoJWTSetting> tokenConfigurations)
        {
            _tokenConfigurations = tokenConfigurations.Value;
        }

        private readonly ConfiguracaoJWTSetting _tokenConfigurations;

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _tokenConfigurations.ValidIssuer,
                ValidAudience = _tokenConfigurations.ValidAudience,
                IssuerSigningKey = _tokenConfigurations.SymmetricSecurityKey, // The same key as the one that generate the token
                ValidAudiences = _tokenConfigurations.ValidAudiences
            };
        }

        public TokenDto GerarToken(Usuario usuario)
        {
            DateTime dataCriacao = DateTime.UtcNow;
            DateTime dataExpiracao = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: _tokenConfigurations.ValidIssuer,
                audience: _tokenConfigurations.ValidAudience,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName, usuario.Nome),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim("UserType", "registrado"), //Claim personalizada para diponibilizar acesso em rotas na api gateway
                    new Claim("PerfilNome", usuario.Perfil.Nome),
                    new Claim("PerfilId", usuario.Perfil.Id.ToString())
                },
                expires: dataExpiracao,
                notBefore: dataCriacao,
                signingCredentials: _tokenConfigurations.SigningCredentials);

            return new TokenDto
            {
                UsuarioId = usuario.Id,
                Autenticado = true,
                DataCriacao = dataCriacao,
                DataExpiracao = dataExpiracao,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public bool TokenValido(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(token.Replace("Bearer ", ""), validationParameters, out validatedToken);
            return true;
        }
    }

    public static class JWTServiceExtensions
    {

        public static string GetClaim(this ClaimsPrincipal claimsPrincipal, string jwtClaim)
        {
            var claim = claimsPrincipal.Claims.Where(c => c.Type == jwtClaim.ToString()).FirstOrDefault();

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }
    }
}
