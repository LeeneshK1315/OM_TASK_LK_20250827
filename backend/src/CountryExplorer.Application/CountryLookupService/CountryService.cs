using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Domain.CountryEntities;

namespace CountryExplorer.Application.CountryLookupService
{
    public interface ICountryService
    {
        Task<IReadOnlyList<Country>> GetAllCountriesAsync(CancellationToken cancellation = default);
        Task<CountryDetails?> GetByCountryNameAsync(string name, CancellationToken cancellation = default);
    }

    public sealed class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Task<IReadOnlyList<Country>> GetAllCountriesAsync(CancellationToken cancellation = default)
            => _countryRepository.GetCountryListAsync(cancellation);

        public Task<CountryDetails?> GetByCountryNameAsync(string name, CancellationToken cancellation = default)
            => _countryRepository.GetCountryByNameAsync(name, cancellation);
    }
}
