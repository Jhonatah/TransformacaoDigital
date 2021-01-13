using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Dtos;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes
{
    public class ContratoRepositorio : RepositorioBase, IContratoRepositorio
    {
        public ContratoRepositorio(ConsultoriaAssessoriaContexto contexto) : base(contexto)
        {
        }

        public async Task AlterarAsync(Guid id, Contrato contrato)
        {
            var model = await Contexto.Contratos.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null) return;

            model.Nome = contrato.Nome;
            model.Ativo = contrato.Ativo;
            model.TipoContratoId = contrato.TipoContratoId;

            await Contexto.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Contrato contrato)
        {
            Contexto.Contratos.Add(contrato);

            await Contexto.SaveChangesAsync();
        }

        public async Task<object> LerPorIdAsync(Guid id)
        {
            return await
                Contexto.Contratos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Paginador<object>> ListarAsync(int pagina)
        {
            pagina = pagina < 1 ? 1 : pagina;
            int take = 10, skip = (pagina - 1) * take;

            var registros =
                await Contexto.Contratos.AsNoTracking()
                .OrderByDescending(x => x.DataCadastro)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await Contexto.Contratos.CountAsync();

            return new Paginador<object>(registros, pagina, total);
        }

        public async Task<object> ListarTiposAsync()
        {
            return await
                Contexto.TiposContratos.AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Nome
                }).ToListAsync();
        }
    }
}