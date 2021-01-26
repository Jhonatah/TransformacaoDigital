using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransformacaoDigital.MVC.ViewModels
{
    public class AlterarEmpresaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int TipoEmpresaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> TiposEmpresas { get; set; }
    }
}
