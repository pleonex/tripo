using System.Text.Json;
using PleOps.Tripo.Integrations;

namespace PleOps.Tripo.Traveller;

public static class TravelFactory
{
    public static async Task<Travel> LoadJsonAsync(string path)
    {
        await using var jsonStream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<Travel>(jsonStream).ConfigureAwait(false);
    }

    public static async Task SaveJsonAsync(Travel travel, string path)
    {
        await using var jsonStream = File.OpenWrite(path);
        await JsonSerializer.SerializeAsync(jsonStream, travel).ConfigureAwait(false);
    }
    
    public static Travel LoadJson(string path)
    {
        using var jsonStream = File.OpenRead(path);
        return JsonSerializer.Deserialize<Travel>(jsonStream);
    }

    public static async Task<Travel> LoadWorldeeJsonAsync(string path)
    {
        await using var jsonStream = File.OpenRead(path);
        var worldee = await JsonSerializer.DeserializeAsync<WorldeeTravel>(jsonStream).ConfigureAwait(false);

        return WorldeeMapper.Map(worldee);
    }
}