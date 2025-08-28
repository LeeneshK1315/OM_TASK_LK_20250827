using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Application.CountryLookupService;
using CountryExplorer.Domain.CountryEntities;
using Moq;

namespace CountryExplorer.Application.Tests;

public class CountryServiceTests
{
    [Fact]
    public async Task GetAll_ReturnsCountries_FromRepository()
    {
        var mockCountryRepo = new Mock<ICountryRepository>();
        mockCountryRepo.Setup(r => r.GetCountryListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Country>
            {
                new() { CountryName = "Zimbabwe", FlagUrl = "z" },
                new() { CountryName = "Albania", FlagUrl = "a" },
                new() { CountryName = "South Africa", FlagUrl = "za" }
            });

        var service = new CountryService(mockCountryRepo.Object);
        var result = await service.GetAllCountriesAsync();

        Assert.Equal(3, result.Count);
        Assert.Contains(result, c => c.CountryName == "Albania" || c.CountryName == "South Africa");
    }

    [Fact]
    public async Task GetByName_Returns_Null_When_NotFound()
    {
        var mockCountryRepo = new Mock<ICountryRepository>();
        mockCountryRepo.Setup(r => r.GetCountryByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((CountryDetails?)null);
        var service = new CountryService(mockCountryRepo.Object);

        var result = await service.GetByCountryNameAsync("NonExisting");
        Assert.Null(result);
    }
}
