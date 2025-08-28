using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Domain.CountryEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace CountryExplorer.WebApi.Tests;

public class CountriesEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CountriesEndpointTests(WebApplicationFactory<Program> factory)
    {
        var mockCountryRepo = new Mock<ICountryRepository>();
        mockCountryRepo.Setup(r => r.GetCountryListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Country>
        {
            new() { CountryName = "South Africa", FlagUrl = "https://flagcdn.com/za.svg" }
        });

        var clientFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => mockCountryRepo.Object);
            });
        });

        _client = clientFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithCountries()
    {
        var resp = await _client.GetAsync("/countries");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

        var json = await resp.Content.ReadAsStringAsync();
        Assert.Contains("South Africa", json);
    }
}
