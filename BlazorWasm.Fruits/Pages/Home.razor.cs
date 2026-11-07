using BlazorWasm.Fruits.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorWasm.Fruits.Pages;

public partial class Home
{
    [Inject] public HttpClient HttpClient { get; set; } = default!;
    private IReadOnlyList<FruitModel> data = [];
    private string SearchTerm { get; set; } = string.Empty;
    private bool IsLoading = true;
    private string? LoadError;
    private IEnumerable<FruitModel> FilteredData => data.Where(fruit => string.IsNullOrWhiteSpace(SearchTerm) || fruit.Name.Contains(SearchTerm.Trim(), StringComparison.OrdinalIgnoreCase));

    protected override async Task OnInitializedAsync()
    {
        try
        {
            data = await HttpClient.GetFromJsonAsync<FruitModel[]>("data/fruits.json") ?? [];
        }
        catch (Exception error) when (error is HttpRequestException or JsonException)
        {
            LoadError = "The fruit library could not load. Please reload the page to try again.";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
