using System.Collections.Generic;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.Services.Dtos.NormasDto;

namespace TransformacaoDigital.MVC.Soaps.Implementacoes
{
    public class NormaSoap : BaseSoap, INormasSoap
    {
        private readonly INormaService normaService;

        public NormaSoap(IServicosHttpBase servicosBase, 
            IAutenticacaoService autenticacaoService,
            INormaService normaService) : base(servicosBase, autenticacaoService)
        {
            this.normaService = normaService;

            Autenticar();
        }

        public IEnumerable<NormaDto> GetNormas()
        {
            var task = Task.Factory.StartNew(() => normaService.ListarAsync(1));
            task.Wait();

            return task.Result.Result.Registros;
        }
    }
}
