using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class AlterarColaboradorViewModel
    {
        public AlterarColaboradorViewModel()
        {
            Perfis = new List<SelectListItem>();
            TiposUsuarios = new List<SelectListItem>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo")]
        public int TipoUsuarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(10, ErrorMessage = "Nome muito curto")]
        public string Nome { get; set; }

        public string Email { get; set; }

        public IEnumerable<SelectListItem> Perfis { get; set; }
        public IEnumerable<SelectListItem> TiposUsuarios { get; set; }
    }
}
