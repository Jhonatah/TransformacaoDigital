using System;
using System.Collections.Generic;
using System.Linq;

namespace TransformacaoDigital.ConsultoriaAssessoria.API.Dtos
{
    public class Paginador<T> where T : class
    {
        private readonly List<T> _registros;

        public Paginador(IEnumerable<T> registros, int paginaAtual, int totalRegistros, int totalPorPagina = 10)
        {
            _registros = registros?.ToList() ?? new List<T>();

            PaginaAtual = paginaAtual < 1 ? 1 : paginaAtual;

            TotalPorPagina = totalPorPagina;
            TotalRegistros = totalRegistros;

            SetTotalDePaginas();
        }

        public int PaginaAtual { get; private set; }
        public int TotalDePaginas { get; private set; }
        public int TotalPorPagina { get; private set; }
        public int TotalRegistros { get; private set; }

        public IEnumerable<T> Registros { get { return _registros; } }

        private void SetTotalDePaginas()
        {
            if (TotalRegistros <= 0)
                TotalDePaginas = 0;

            TotalDePaginas = (int)Math.Ceiling((decimal)TotalRegistros / (decimal)TotalPorPagina);
        }
    }

    public static class PaginadorExtensoes
    {
        public static int PaginaMenorQueUm(this int pagina) => pagina < 1 ? 1 : pagina;
    }
}
