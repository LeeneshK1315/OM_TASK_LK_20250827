using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CountryExplorer.Infrastructure.HTTP
{
    public sealed class CountryLookupClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public sealed record CountryDto(
            CountryNameDto name,
            CountryFlagsDto flags,
            long? population,
            string[]? capital,
            string? region,
            string[]? language
        );

        public sealed record CountryNameDto(string common);
        public sealed record CountryFlagsDto(string? svg, string? png);

        public CountryLookupClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<CountryDto>> GetAllCountriesAsync(CancellationToken cancellation)
            => await _httpClient.GetFromJsonAsync<List<CountryDto>>("https://restcountries.com/v3.1/independent?status=true&fields=name,flags,population,capital,region,language", JsonOptions, cancellation)
            ?? new();

        public async Task<CountryDto?> GetByCountryNameAsync(string countryName, CancellationToken cancellation)
        {
            var list = await _httpClient.GetFromJsonAsync<List<CountryDto>>($"https://restcountries.com/v3.1/name/{Uri.EscapeDataString(countryName)}?fields=name,flags,population,capital,region", JsonOptions, cancellation);
            return list?.FirstOrDefault();
        }
    }
}
