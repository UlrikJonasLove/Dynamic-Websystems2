using Api.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace mvc.Helpers
{
    public class TheWebApi
    {
        public HttpClient Initial()
        {
            var token = "";
            if (TokenManager.loc.Exists("securityToken"))
                token = TokenManager.loc.Get<string>("securityToken");

            var webApi = new HttpClient();
            webApi.BaseAddress = new Uri("https://localhost:44336/");
            webApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return webApi;
        }
    }
}
