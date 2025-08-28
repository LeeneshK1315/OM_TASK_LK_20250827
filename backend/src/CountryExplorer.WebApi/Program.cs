using CountryExplorer.Application.CountryLookupMain;
using CountryExplorer.Application.CountryLookupService;
using CountryExplorer.Infrastructure.HTTP;
using CountryExplorer.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<CountryLookupClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<ICountryRepository, CountriesRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Country Explorer Assessment API", Version = "v1" });
});

builder.Services.AddCors(o => o.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.WebHost.UseUrls("https://localhost:7114");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Country Explorer API Assessment v1");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();

public partial class Program { }
