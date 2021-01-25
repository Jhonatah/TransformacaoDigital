using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class NovaNormaViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoNormaId { get; set; }

        public IEnumerable<SelectListItem> TiposNormas { get; set; }
    }
}