namespace UI;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync(
        int maxItems = 10,
        CancellationToken cancellationToken = default)
    {
        List<WeatherForecast>? forecasts = null;
        IAsyncEnumerable<WeatherForecast?> results = httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("/weatherforecast", cancellationToken);

        await foreach (var forecast in results)
        {
            if (forecasts?.Count >= maxItems)
            {
                break;
            }

            if (forecast is null) continue;

            forecasts ??= [];
            forecasts.Add(forecast);
        }

        return forecasts?.ToArray() ?? [];
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}