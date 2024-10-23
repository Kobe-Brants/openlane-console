namespace BikeConsole.Domain;

public class Bike
{
    public required string Id { get; set; }
    public required string BikeContainerId { get; set; }
    public required string Brand { get; set; }
    public required string Type { get; set; }
    public required string Country { get; set; }
    public bool IncludesAllTaxes { get; set; }
    public required string Version { get; set; }
    public required string Category { get; set; }
    public required string AdditionalInformation { get; set; }
    public required string RegistrationNumber { get; set; }
    public DateTime FirstRegistrationDate { get; set; }
    public required List<string> Equipments { get; set; }
    public required string MainImageUrl { get; set; }
    public required List<Document> Documents { get; set; }
    public required List<Tax> Taxes { get; set; }
    public required string Comments { get; set; }

    public required BikeContainer BikeContainer { get; set; }
}