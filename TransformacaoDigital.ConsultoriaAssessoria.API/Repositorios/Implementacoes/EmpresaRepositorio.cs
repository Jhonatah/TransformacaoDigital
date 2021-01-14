using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransformacaoDigital.ConsultoriaAssessoria.API.Dtos;
using TransformacaoDigital.ConsultoriaAssessoria.API.Models;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Repositorios.Implementacoes
{
    public class EmpresaRepositorio : RepositorioBase, IEmpresaRepositorio
    {
        public EmpresaRepositorio(ConsultoriaAssessoriaContexto contexto) : base(contexto)
        {
        }

        public async Task AlterarAsync(Guid id, Empresa empresa)
        {
            var model = await Contexto.Empresas.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null) return;

            model.RazaoSocial = empresa.RazaoSocial;
            model.NomeFantasia = empresa.NomeFantasia;
            model.TipoEmpresaId = empresa.TipoEmpresaId;
            model.Email = empresa.Email;

            await Contexto.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Empresa empresa)
        {
            Contexto.Empresas.Add(empresa);

            await Contexto.SaveChangesAsync();
        }

        public async Task<object> LerPorIdAsync(Guid id)
        {
            return await
                Contexto.Contratos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Paginador<object>> ListarAsync(int pagina)
        {
            pagina = pagina < 1 ? 1 : pagina;
            int take = 10, skip = (pagina - 1) * take;

            var registros =
                await Contexto.Empresas.AsNoTracking()
                .OrderByDescending(x => x.DataCadastro)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var total = await Contexto.Empresas.CountAsync();

            return new Paginador<object>(registros, pagina, total);
        }

        public async Task<object> ListarTiposAsync()
        {
            return await
                Contexto.TiposEmpresas.AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Nome
                }).ToListAsync();
        }

        public async Task<bool> CNPJExisteAsync(string cnpj) 
            => await Contexto.Empresas.AnyAsync(x => x.CNPJ == cnpj);
    }
}
