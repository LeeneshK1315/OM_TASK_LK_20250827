using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Domain.CountryEntities;
using CountryExplorer.Infrastructure.HTTP;
using Microsoft.Extensions.Caching.Memory;

namespace CountryExplorer.Infrastructure.Persistence
{
    public sealed class CountriesRepository : ICountryRepository
    {
        private readonly CountryLookupClient _client;
        private readonly IMemoryCache _cache;
        private static readonly string AllCacheKey = "countries:all";
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(15);

        public CountriesRepository(CountryLookupClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<IReadOnlyList<Country>> GetCountryListAsync(CancellationToken cancellation = default)
        {
            if (_cache.TryGetValue(AllCacheKey, out List<Country>? cached)) return cached!;

            var data = await _client.GetAllCountriesAsync(cancellation);
            var mapped = data
                .Where(d => d.name?.common is not null)
                .Select(d => new Country
                {
                    CountryName = d.name.common,
                    FlagUrl = d.flags.svg ?? d.flags.png ?? string.Empty
                })
                .OrderBy(c => c.CountryName)
                .ToList();

            _cache.Set(AllCacheKey, mapped, CacheTtl);
            return mapped;
        }

        public async Task<CountryDetails?> GetCountryByNameAsync(string countryName, CancellationToken cancellation = default)
        {
            var dto = await _client.GetByCountryNameAsync(countryName, cancellation);
            if (dto is null || dto.name?.common is null) return null;

            return new CountryDetails
            {
                CountryName = dto.name.common,
                Population = dto.population ?? 0,
                Capital = dto.capital?.ToList(),
                Regions = dto.region ?? string.Empty,
                FlagUrl = dto.flags.svg ?? dto.flags.png ?? string.Empty
            };
        }
    }
}
