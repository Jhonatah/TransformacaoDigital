using System;
using System.Net.Http;
using System.Threading.Tasks;
using TransformacaoDigital.Web.API.Enums;
using TransformacaoDigital.Web.API.Services.Dtos;

namespace TransformacaoDigital.Web.API.Services
{
    public abstract class ServiceBase : IDisposable
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly HttpClientesEnums _httpClientesEnums;
        private HttpClient _httpCliente;
        private bool disposedValue;

        protected ServiceBase(IHttpClientFactory httpClientFactory, HttpClientesEnums httpClientesEnum)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientesEnums = httpClientesEnum;
            _httpCliente = _httpClientFactory.CreateClient(HttpClientesEnums.GetName<HttpClientesEnums>(_httpClientesEnums));
        }

        protected void SetToken(string token)
        {
            _httpCliente.DefaultRequestHeaders.Authorization =
                 new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<ResponseObj<T>> GetAsync<T>(string urlComplemento)
        {
            throw new Exception();
        }

        protected async Task<ResponseObj<T>> PostAsync<T>(string urlComplemento, object data)
        {
            throw new Exception();
        }

        protected async Task<ResponseObj<T>> PutAsync<T>(string urlComplemento, object data)
        {
            throw new Exception();
        }

        protected async Task<ResponseObj<T>> DeleteAsync<T>(string urlComplemento, object data)
        {
            throw new Exception();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_httpClientFactory != null)
                        _httpClientFactory = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}