using System.Collections.Generic;
using System.ServiceModel;
using TransformacaoDigital.MVC.Services.Dtos.NormasDto;

namespace TransformacaoDigital.MVC.Soaps
{
    [ServiceContract]
    public interface INormasSoap
    {
        [OperationContract]
        IEnumerable<NormaDto> GetNormas();
    }
}
