using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services;
using TransformacaoDigital.MVC.Services.Dtos;

namespace TransformacaoDigital.MVC.Soaps.Implementacoes
{
    public abstract class BaseSoap
    {
        private readonly IServicosHttpBase servicosBase;
        private readonly IAutenticacaoService autenticacaoService;
        private string Bearer;

        protected BaseSoap(
            IServicosHttpBase servicosBase,
            IAutenticacaoService autenticacaoService)
        {
            this.autenticacaoService = autenticacaoService;
            this.servicosBase = servicosBase;

            this.servicosBase.HttpClient.DefaultRequestHeaders.Accept.Clear();
            this.servicosBase.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void Autenticar()
        {
            var task = Task.Factory.StartNew(() => autenticacaoService.LoginAsync("soap@sigo.com", "soap@sigo.com"));
            task.Wait();

            Bearer = task.Result.Result.Token;
            servicosBase.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Bearer);
        }
        private async Task<ResponseBase<T>> RecuperarResponse<T>(HttpResponseMessage message)
        {
            var stringObj = await message.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<T>(stringObj);
            return new ResponseBase<T>(obj, message.StatusCode, (int)message.StatusCode > 199 && (int)message.StatusCode < 400);
        }

        public virtual async Task<ResponseBase<T>> GetAsync<T>(string url)
        {
            if(string.IsNullOrEmpty(Bearer))
            {
                Autenticar();
            }

            try
            {
                var result = await servicosBase.HttpClient.GetAsync(url);
                return await RecuperarResponse<T>(result);
            }
            catch (Exception ex)
            {
                servicosBase.NotificacaoService.NotificarErro(ex);
                return default(ResponseBase<T>);
            }
        }
    }
}
