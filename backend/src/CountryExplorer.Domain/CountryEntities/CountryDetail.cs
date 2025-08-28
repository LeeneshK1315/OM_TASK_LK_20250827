namespace CountryExplorer.Domain.CountryEntities
{
    public class CountryDetails
    {
        public required string CountryName { get; init; }
        public long Population { get; init; }
        public List<string>? Capital { get; init; }
        public string? Regions { get; init; }
        public required string FlagUrl { get; init; }
    }
}
