using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class NovoColaboradorViewModel
    {
        public NovoColaboradorViewModel()
        {
            Perfis = new List<SelectListItem>();
            TiposUsuarios = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name ="Perfil")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo")]
        public int TipoUsuarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(10, ErrorMessage ="Nome muito curto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage ="Email inválido")]
        [Remote(action: "ValidarEmailExistente", controller: "Colaboradores")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> Perfis { get; set; }
        public IEnumerable<SelectListItem> TiposUsuarios { get; set; }
    }
}