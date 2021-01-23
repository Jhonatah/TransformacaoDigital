using System;
using TransformacaoDigital.MVC.Services.Dtos;

namespace TransformacaoDigital.MVC.Services
{
    public interface IUsuarioService
    {
        string GetToken();
        Guid GetId();

        CredenciaToken GetCredencial();
    }
}
