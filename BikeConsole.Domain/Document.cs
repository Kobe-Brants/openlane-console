namespace BikeConsole.Domain;

public class Document
{
    public required string DocumentId { get; set; }
    public required string Type { get; set; }
    public required string Url { get; set; }

    public required Bike Bike { get; set; }
}