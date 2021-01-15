using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TransformacaoDigital.Autenticacao.API.Models;

namespace TransformacaoDigital.Autenticacao.API.Repositorios.Implementacoes
{
    public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
    {
        public UsuarioRepositorio(UsuariosContexto contexto) : base(contexto)
        {
        }

        public async Task<bool> AutenticarAsync(string email, string senha)
        {
            return await Contexto.Usuarios.AnyAsync(x => x.Email == email && x.Senha == senha);
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            Contexto.Usuarios.Add(usuario);
            await Contexto.SaveChangesAsync();
        }

        public async Task<Usuario> LerEmailAsync(string email)
        {
            return await Contexto.Usuarios.AsNoTracking()
                .Include(x => x.Perfil).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario> LerPorIdAsync(Guid id)
        {
            return await Contexto.Usuarios.AsNoTracking()
                .Include(x => x.Perfil).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
