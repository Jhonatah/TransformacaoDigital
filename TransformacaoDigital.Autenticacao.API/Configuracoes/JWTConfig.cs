using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TransformacaoDigital.Autenticacao.API.Configuracoes
{
    public static class JWTConfig
    {
        public static void SetJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfiguracaoJWTSetting>(configuration.GetSection("ConfiguracaoJWT"));

            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                //o.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    //ValidateIssuer = true,
                //    ValidAudiences = new List<string>()
                //    {
                //      "http://localhost:62633",
                //      "http://localhost:61870"
                //    },
                //    ValidIssuers = new List<string>()
                //    {
                //        "http://localhost:61870"
                //    },
                //    ValidateAudience = false
                //};
            });

            services.AddAuthorization(options =>
            {
                /*Aqui podemos adicionar as 'policies'  
                 *  options.AddPolicy(ClaimsAuth.Cliente, policy => policy.RequireClaim(ClaimsAuth.Perfil, ClaimsAuth.Cliente));
                 */

                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }

    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly IOptions<ConfiguracaoJWTSetting> _jwtAuthentication;

        public ConfigureJwtBearerOptions(IOptions<ConfiguracaoJWTSetting> jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
        }

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            var jwtAuthentication = _jwtAuthentication.Value;

            options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
            options.IncludeErrorDetails = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = false,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidIssuer = jwtAuthentication.ValidIssuer,
                ValidAudience = jwtAuthentication.ValidAudience,
                IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                NameClaimType = ClaimTypes.NameIdentifier,
                ValidAudiences = jwtAuthentication.ValidAudiences
            };
        }
    }
}
