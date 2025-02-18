using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PartsClient.Data;

public static class PartsManager
{
    static readonly string BaseAddress = "URL_GOES_HERE";
    static readonly string Url = $"{BaseAddress}/api/parts";
    static HttpClient client;

    private static async Task<HttpClient> GetClient()
    {
        if (client == null)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_AUTH_KEY");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        return client;
    }

    public static async Task<IEnumerable<Part>> GetAll()
    {
        var httpClient = await GetClient();
        var response = await httpClient.GetFromJsonAsync<IEnumerable<Part>>(Url);
        return response ?? new List<Part>();
    }

    public static async Task<Part> Add(string partName, string supplier, string partType)
    {
        var httpClient = await GetClient();
        var newPart = new Part { PartName = partName, TheSuppliers = supplier, PartType = partType };

        var response = await httpClient.PostAsJsonAsync(Url, newPart);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Part>();
    }

    public static async Task Update(Part part)
    {
        var httpClient = await GetClient();
        var response = await httpClient.PutAsJsonAsync($"{Url}/{part.PartID}", part);
        response.EnsureSuccessStatusCode();
    }

    public static async Task Delete(string partID)
    {
        var httpClient = await GetClient();
        var response = await httpClient.DeleteAsync($"{Url}/{partID}");
        response.EnsureSuccessStatusCode();
    }
}
