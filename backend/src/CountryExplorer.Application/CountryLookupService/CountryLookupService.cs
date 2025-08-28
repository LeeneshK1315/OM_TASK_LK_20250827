using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Domain.CountryEntities;

namespace CountryExplorer.Application.CountryLookupService
{
    public sealed class CountryLookupService(ICountryRepository countryRepository)
    {
        private readonly ICountryRepository _countryRepository = countryRepository;

        public Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellation = default)
        => _countryRepository.GetCountryListAsync(cancellation);

        public Task<CountryDetails?> GetByCountryNameAsync(string countryName, CancellationToken cancellation = default)
        => _countryRepository.GetCountryByNameAsync(countryName, cancellation);
    }
}
