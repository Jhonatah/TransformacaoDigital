using System.Net;

namespace TransformacaoDigital.MVC.Services.Dtos
{
    public struct ResponseBase<T>
    {
        public ResponseBase(T objResult, HttpStatusCode httpStatusCode, bool sucesso)
        {
            ObjResult = objResult;
            HttpStatusCode = httpStatusCode;
            Sucesso = sucesso;
        }

        public T ObjResult { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public bool Sucesso { get; private set; }
    }
}
