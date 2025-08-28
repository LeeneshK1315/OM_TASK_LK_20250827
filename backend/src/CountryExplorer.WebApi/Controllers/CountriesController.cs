using CountryExplorer.Application.CountryLookupService;
using CountryExplorer.Domain.CountryEntities;
using Microsoft.AspNetCore.Mvc;

namespace CountryExplorer.WebApi.Controllers
{
    [ApiController]
    [Route("countries")]
    public sealed class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService) => _countryService = countryService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Country>>> GetAll(CancellationToken cancellation)
        {
            var countries = await _countryService.GetAllCountriesAsync(cancellation);
            return Ok(countries);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(CountryDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDetails>> GetByName(string name, CancellationToken cancellation)
        {
            var result = await _countryService.GetByCountryNameAsync(name, cancellation);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
