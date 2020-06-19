using Api.Models;
using Microsoft.AspNetCore.Mvc;
using mvc.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace mvc.Components
{
    public class TodoTags : ViewComponent
    {
        //Hitta hashtags med todoid
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var tags = await GetItemAsync(id);

            return View(tags);
        }

        private async Task<List<Tags>> GetItemAsync(int? id)
        {
            TheWebApi _api = new TheWebApi();
            List<Tags> tags = new List<Tags>();
            HttpClient http = _api.Initial();
            HttpResponseMessage res = await http.GetAsync("api/Todos/Tags/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                tags = JsonConvert.DeserializeObject<List<Tags>>(result);
            }

            return tags;
        }
    }
}
