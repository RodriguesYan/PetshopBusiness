using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetshopDB.Models;
using PetshopGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetshopGateway.Petshop
{
    public interface IApi
    {
        public Task<LoginResult> PostCreateUser(dynamic model);
    }

    public class Api// : IApi
    {
        private readonly HttpClient _httpClient;
        private readonly string JwtToken;
        private IHttpContextAccessor httpContext;

        //HttpClient _httpClient;


        //public HttpClient _httpClient;

        //Se for add token no cookie, tem que usar o httpcontext
        //Porem, nao da pra usar aqui
        //Entao, tem que injetar nas dependencias o seguinte:
        //private readonly IHttpContextAcessor _acessor
        //depois usar ele assim: acessor.HttpContext
        //Depois por no startup
        //services.AddHttpContextAccessor();
        //
        //public string Token { get; set; }

        //public Api(HttpClient httpCLient)
        //{
        //    _httpClient = httpCLient;
        //}

        public Api(string token = null)
        {
            JwtToken = token;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:59841/api/v1.0/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "authentication/json");
            if(token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public object Template()
        {
            //Pegar o token do cookie
            //var token = HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            //Bom por num metodo privado. Ai é só chamar quando precisar do token
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer",
                "Token");

            //Usar isso num projeto gateway
            //HttpResponseMessage result = await httpClient.PostAsync($"ClientUser/");
            //result.IsSuccessStatusCode => segundo plano
            //result.EnsureSuccessStatusCode(); => se der result != 200, gera uma excessão
            //var user =  await result.Content.ReadAsAsync<ClientUser>(); =>deserializa o json nesse objeto

            //Post
            //HttpContent content = CreateMultipartFormDataContent(model)
            return new { };
        }

        public string SetQuotesToSendToApi(string value)
        {
            return $"\"{value}\"";
        }

        public async Task<LoginResult> CreateUser(dynamic model)
        {
            return await ManageUser(model, "ClientUser");
        }

        public async Task<LoginResult> ExecuteLogin(dynamic model)
        {
            return await ManageUser(model, "ClientUser/login");
        }

        private async Task<LoginResult> ManageUser(dynamic model, string uri)
        {
            //Ver se vou precisar retornar somente o token ou tb o objeto criado
            //HttpContent content = CreateMultipartFormDataContent(model);=> usar isso só se for mandar imagem ou algo do tipo pro DB

            //var result = await _httpCLient.PostAsJsonAsync<dynamic>("CreateUser/login", model);
            LoginResponse resultTest;

            if (model.Name != null)
            {
                resultTest = new LoginResponse
                {
                    Login = model.Email,
                    Name = model.Name,
                    Password = model.Password,
                };
            }
            else
            {
                resultTest = new LoginResponse
                {
                    Login = model.Email,
                    Password = model.Password,
                };
            }

            var result = await _httpClient.PostAsJsonAsync(uri, resultTest);

            if (result.IsSuccessStatusCode)
            {
                return new LoginResult
                {
                    Succeeded = result.IsSuccessStatusCode,
                    ErrorMessage = "",
                    LoginProperties = await result.Content.ReadAsAsync<LoginProperties>() //.ReadAsync()//da pra por o tipo <Class>
                };
            }

            string errorJson = await result.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<dynamic>(errorJson);

            return new LoginResult
            {
                Succeeded = result.IsSuccessStatusCode,
                //ErrorMessage = await result.Content.ReadAsStringAsync(),
                ErrorMessage = message.message,
            };
        }

        public async Task<VMAddress> GetAddress(int UserId)
        {
            var result = await _httpClient.GetAsync($"ClientUser/GetAddress?userId={UserId}");
            var teste = await result.Content.ReadAsAsync<VMAddress>();

            return teste;
        }

        public async Task<string> CreateAddress(VMAddress address, string token)
        {
            var result = await _httpClient.PostAsJsonAsync("ClientUser/CreateAddress", address);

            return "Success";
        }

        public async Task<string> EditAddress(VMAddress address, string token)
        {
            var result = await _httpClient.PutAsJsonAsync($"ClientUser/EditAddress/{address.AddressId}", address);

            return "Success";
        }

        private HttpContent CreateMultipartFormDataContent(ClientUser model)
        {
            var content = new MultipartFormDataContent();

            //Obs: nao pode add content que for opcional, senao da toco
            content.Add(new StringContent(model.Login), SetQuotesToSendToApi("Login"));
            content.Add(new StringContent(model.InsertDate.ToString()), SetQuotesToSendToApi("InsertDate"));
            content.Add(new StringContent(model.Password), SetQuotesToSendToApi("Password"));

            //if(model.ClientUserId > 0)
            //{ =|> se for pra alterar, tem que passar o id
            //    content.Add(new StringContent(model.ClientUserId.ToString()), SetQuotesToSendToApi("ClientUserId"));
            //}

            //content.Add(new StringContent(model.Login), "\"Login \"");

            //Pesquisar como fazer requisição mandando um cara no corpo, nao no form
            //Se for usar o form (que nem ta usando agora), tem que por no argumento de entrada do metodo da api, a tag [FromFom]

            return content;
        }

        private void SetHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public LoginProperties LoginProperties { get; set; }
    }

    public class LoginProperties
    {
        public ClientUser User { get; set; }
        public string Token { get; set; }
    }

    public class LoginResponse
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string FormValue { get; set; }
    }
}
