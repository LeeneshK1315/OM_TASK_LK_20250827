using CountryExplorer.Domain.CountryEntities;

namespace CountryExplorer.Application.CountryLookupMain
{
    public interface ICountryRepository
    {
        Task<IReadOnlyList<Country>> GetCountryListAsync(CancellationToken cancellationToken = default);
        Task<CountryDetails?> GetCountryByNameAsync(string countryName, CancellationToken cancellationToken = default);
    }
}
