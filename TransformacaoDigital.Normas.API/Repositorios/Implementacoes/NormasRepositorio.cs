using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.Normas.API.Dtos;
using TransformacaoDigital.Normas.API.Models;

namespace TransformacaoDigital.Normas.API.Repositorios.Implementacoes
{
    public class NormasRepositorio : RepositorioBase, INormasRepositorio
    {
        public NormasRepositorio(NormasContexto contexto) : base(contexto)
        {
        }

        public async Task AlterarAsync(Guid id, Norma norma)
        {
            var model = await Contexto.Normas.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null) return;

            model.TipoNormaId = norma.TipoNormaId;
            model.Nome = norma.Nome;
            model.Descricao = norma.Descricao;

            await Contexto.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Norma norma)
        {
            Contexto.Normas.Add(norma);
            await Contexto.SaveChangesAsync();
        }

        public async Task<object> LerPorIdAsync(Guid id)
        {
            return await Contexto.Normas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Paginador<object>> ListarAsync(int pagina)
        {
            pagina = pagina < 1 ? 1 : pagina;
            int skip = (pagina - 1) * 10, take = 10;

            var registros =
                await Contexto.Normas
                    .Select(x => new
                    {
                        x.Id,
                        x.Nome,
                        x.DataCadastro,
                        x.Descricao,
                        TipoNorma = new
                        {
                            x.TipoNorma.Id,
                            x.TipoNorma.Nome
                        }
                    })
                    .OrderByDescending(x => x.DataCadastro)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

            var total = await Contexto.Normas.CountAsync();

            return new Paginador<object>(registros, pagina, total);
        }

        public async Task<object> ListarTiposNormasAsync()
        {
            return await Contexto.TiposNormas.AsNoTracking()
                .Select(s => new
                {
                    s.Id,
                    s.Nome
                }).ToListAsync();
        }
    }
}