
using BlazorWasm.Fruits.Models;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWasm.Fruits.Pages
{
    public partial class Home
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private IEnumerable<FruitModel> data;
        RadzenDataGrid<FruitModel> grid;

        protected override async Task OnInitializedAsync()
        {
            var response = await HttpClient.GetFromJsonAsync<FruitModel[]>("data/fruits.json");
            data = response.ToList();
        }
    }
}
