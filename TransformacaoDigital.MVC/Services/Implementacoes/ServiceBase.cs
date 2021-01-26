using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransformacaoDigital.MVC.Services.Dtos;

namespace TransformacaoDigital.MVC.Services.Implementacoes
{
    public abstract class ServiceBase
    {
        protected readonly IServicosHttpBase ServicosBase;

        public ServiceBase(IServicosHttpBase servicosBase)
        {
            ServicosBase = servicosBase;

            ServicosBase.HttpClient.DefaultRequestHeaders.Accept.Clear();
            ServicosBase.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            SetToken();
        }

        private void SetToken()
        {
            var token = ServicosBase.UsuarioService.GetToken();

            if (string.IsNullOrEmpty(token)) return;

            AddAutorization("Bearer", token);
        }

        protected void AddAutorization(string chave, string token)
        {
            ServicosBase.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(chave, token);
        }

        private StringContent GetStringContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private ResponseBase<T> ErroResponse<T>()
        {
            return new ResponseBase<T>(default(T), System.Net.HttpStatusCode.BadRequest, false);
        }

        private async Task<ResponseBase<T>> RecuperarResponse<T>(HttpResponseMessage message)
        {
            var stringObj = await message.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<T>(stringObj);
            return new ResponseBase<T>(obj, message.StatusCode, (int)message.StatusCode > 199 && (int)message.StatusCode < 400);
        }

        public virtual async Task<ResponseBase<T>> PostAsync<T>(string url, object data)
        {
            try
            {
                var result = await ServicosBase.HttpClient.PostAsync(url, GetStringContent(data));
                return await RecuperarResponse<T>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<T>();
            }
        }
        public virtual async Task<ResponseBase<object>> PostAsync(string url, object data)
        {
            try
            {
                var result = await ServicosBase.HttpClient.PostAsync(url, GetStringContent(data));
                return await RecuperarResponse<object>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<object>();
            }
        }

        public virtual async Task<ResponseBase<T>> PutAsync<T>(string url, object data)
        {
            try
            {
                var result = await ServicosBase.HttpClient.PutAsync(url, GetStringContent(data));
                return await RecuperarResponse<T>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<T>();
            }
        }
        public virtual async Task<ResponseBase<object>> PutAsync(string url, object data)
        {
            try
            {
                var result = await ServicosBase.HttpClient.PutAsync(url, GetStringContent(data));
                return await RecuperarResponse<object>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<object>();
            }
        }

        public virtual async Task<ResponseBase<object>> DeleteAsync(string url)
        {
            try
            {
                var result = await ServicosBase.HttpClient.DeleteAsync(url);
                return await RecuperarResponse<object>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<object>();
            }
        }

        public virtual async Task<ResponseBase<T>> GetAsync<T>(string url)
        {
            try
            {
                var result = await ServicosBase.HttpClient.GetAsync(url);
                return await RecuperarResponse<T>(result);
            }
            catch (Exception ex)
            {
                ServicosBase.NotificacaoService.NotificarErro(ex);
                return ErroResponse<T>();
            }
        }
    }
}