using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.ProcessoIndustrial.API.Dtos;
using TransformacaoDigital.ProcessoIndustrial.API.Models;

namespace TransformacaoDigital.ProcessoIndustrial.API.Repositorios.Implementacoes
{
    public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
    {
        public UsuarioRepositorio(ProcessosIndustriaisContexto contexto) : base(contexto){ }

        public async Task AlterarAsync(Usuario model)
        {
            Contexto.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await Contexto.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Usuario model)
        {
            Contexto.Usuarios.Add(model);
            await Contexto.SaveChangesAsync();
        }

        public async Task<Paginador<object>> ListarAsync(int pagina = 1, int registrosPorPagina = 10)
        {
            pagina = pagina < 1 ? 1 : pagina;
            int skip = (pagina - 1) * 10, take = 10;

            var resultado = await
                Contexto.Usuarios
                    .Select(x => new
                    {
                        x.Id,
                        x.Nome,
                        x.Email,
                        x.Ativo,
                        TipoUsuario = new
                        {
                            Id = x.TipoUsuario.Id,
                            x.TipoUsuario.Nome
                        },
                        Perfil = new
                        {
                            Id = x.PerfilId,
                            x.Perfil.Nome
                        }
                    })
                    .OrderBy(x => x.Nome)
                    .Skip(skip)
                    .Take(registrosPorPagina)
                    .ToListAsync();

            var total = await Contexto.Usuarios.Select(x => x.Id).CountAsync();

            return new Paginador<object>(resultado, pagina, total, registrosPorPagina);
        }

        public async Task<object> LerPorIdDtoAsync(Guid usuarioId)
        {
            return await Contexto.Usuarios
                    .Select(x => new
                    {
                        x.Id,
                        x.Nome,
                        x.Email,
                        x.Ativo,
                        TipoUsuario = new
                        {
                            Id = x.TipoUsuario.Id,
                            x.TipoUsuario.Nome
                        },
                        Perfil = new
                        {
                            Id = x.PerfilId,
                            x.Perfil.Nome
                        }
                    })
                    .FirstOrDefaultAsync(x => x.Id == usuarioId);
        }

        public async Task<Usuario> LerPorIdAsync(Guid usuarioId)
        {
            return await Contexto.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
        }

        public async Task<object> ListarPerfisAsync()
        {
            return await Contexto
                .Perfis.Select(s => new
                {
                    s.Id,
                    s.Nome
                }).OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<object> ListarTipoUsuariosAsync()
        {
            return await Contexto
                .TiposUsuarios.Select(s => new
                {
                    s.Id,
                    s.Nome
                }).OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await Contexto.Usuarios.AnyAsync(x => x.Email == email);
        }
    }
}