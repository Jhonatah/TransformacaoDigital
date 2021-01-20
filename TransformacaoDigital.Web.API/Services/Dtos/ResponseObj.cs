using System.Net;

namespace TransformacaoDigital.Web.API.Services.Dtos
{
    public class ResponseObj<T>
    {

        public ResponseObj(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;

            Sucesso = (int)httpStatusCode <= 399;
        }
        public HttpStatusCode HttpStatusCode { get; private set; }

        public string MensagemErro { get; set; }
        public T Result { get; set; }

        public bool Sucesso { get; private set; }
    }
}